using ClienteTcp;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TransportesCRLib;

namespace ServerTcp
{
    public partial class frmServidor : Form
    {
        /// <summary>
        /// Declaracion de los delegados para los objetos
        /// que despliegan datos
        /// </summary>
        /// <param name="mensaje"></param>
        delegate void ModificartxtStatusDelegado(string mensaje);
        delegate void ModificarlblClientesConectadosDelegado(string mensaje);
        ModificartxtStatusDelegado modificartxtStatus;
        ModificarlblClientesConectadosDelegado modificarlblClientesConectados;

        /// <summary>
        /// Declaracion de variables globales
        /// </summary>
        private List<TcpClient> listaClientes;
        private TcpListener tcpListener;
        private Thread subprocesoEscuchaClientes;
        private int cuentaClientes;
        private bool continuar;
        IPAddress local = IPAddress.Parse("127.0.0.1");
        int intPort = 16830;

        /// <summary>
        /// Creamos una instancia de la clase DataLayer
        /// </summary>
        DataLayer datalayer = new DataLayer();

        public frmServidor()
        {
            InitializeComponent();
            /// Inicializamos todos los componentes
            listaClientes = new List<TcpClient>();
            cuentaClientes = 0;
            lblClientesConectados.Text = "Clientes conectados: 0";
            btnIniciarServidor.Enabled = true;
            btnDetenerServidor.Enabled = false;
            btnEnviarMensajeGrupal.Enabled = false;
            btnCliente.Enabled = false;
            txtStatus.Text = string.Empty;
            modificartxtStatus = new ModificartxtStatusDelegado(ModificartxtStatus);
            modificarlblClientesConectados = new ModificarlblClientesConectadosDelegado(ModificarlblClientesConectados);
        }

        /// <summary>
        /// Metodo del delegado para modificar el txtStatus
        /// </summary>
        /// <param name="mensaje"></param>
        private void ModificartxtStatus(string mensaje) 
        {
            txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " +mensaje;
            txtStatus.SelectionStart = txtStatus.Text.Length;
            txtStatus.ScrollToCaret();
        }

        /// <summary>
        /// Metodo del delegado para modificar el label mostrando la cantidad de clientes conectados
        /// </summary>
        /// <param name="mensaje"></param>
        private void ModificarlblClientesConectados(string mensaje)
        {
            lblClientesConectados.Text = mensaje;
        }

        private void btnIniciarServidor_Click(object sender, EventArgs e)
        {
            try
            {
                /// Inicializamos los componentes y variables
                lblClientesConectados.Text = "Clientes conectados: 0";
                cuentaClientes = 0;
                listaClientes.Clear();

                txtStatus.Invoke(modificartxtStatus, new object[] { "Servidor iniciando en (" + local.ToString() + ":" + intPort.ToString() + ") " });

                btnIniciarServidor.Enabled = false;
                btnDetenerServidor.Enabled = true;
                btnCliente.Enabled = true;
                btnEnviarMensajeGrupal.Enabled = true;

                /// Levantamos el servidor TCP\IP
                continuar = true;
                tcpListener = new TcpListener(local, intPort);
                subprocesoEscuchaClientes = new Thread(new ThreadStart(EscuchaClientes));
                subprocesoEscuchaClientes.Start();
                subprocesoEscuchaClientes.IsBackground = true;
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Problem starting server." });
                txtStatus.Invoke(modificartxtStatus, new object[] { ex.ToString() });
            }
        }

        /// <summary>
        /// Metodo relacionado al thread del servidor
        /// </summary>
        private void EscuchaClientes()
        {
            try
            {
                tcpListener.Start();
                txtStatus.Invoke(modificartxtStatus, new object[] { "Server iniciado. escuchando en :" + local.ToString() + ":" + intPort.ToString() + ") " });

                while (continuar)
                {
                    txtStatus.Invoke(modificartxtStatus, new object[] { "Esperando por clientes..." });
                    TcpClient client = tcpListener.AcceptTcpClient();   // se bloquea esperando nuevas conecciones
                    listaClientes.Add(client);
                    cuentaClientes += 1;
                    Thread clientThread = new Thread(new ParameterizedThreadStart(ComunicacionClienteB));
                    clientThread.Start(client);

                    txtStatus.Invoke(modificartxtStatus, new object[] { "Conexion con cliente aceptada..." });
                    lblClientesConectados.Invoke(modificarlblClientesConectados, new object[] { "Clientes conectados: " + cuentaClientes.ToString() });
                }
            }
            catch (SocketException )
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Problem starting server." });
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "**Problemas iniciando el servidor**" });
                txtStatus.Invoke(modificartxtStatus, new object[] { "Error: EscuchaClientes " + ex.ToString() });
            }
            txtStatus.Invoke(modificartxtStatus, new object[] { "**Terminando el listener y thread**" });
            txtStatus.Invoke(modificartxtStatus, new object[] { " " });
        } 

        /// <summary>
        /// Metodo para procesar la comunicacion con los clientes conectados
        /// </summary>
        /// <param name="obj"></param>
        private void ComunicacionClienteB(object obj)
        {  
            TcpClient tcpClient = (TcpClient)obj;
            StreamReader reader = new StreamReader(tcpClient.GetStream());
            StreamWriter servidorStreamWriter = new StreamWriter(tcpClient.GetStream());

            while (continuar)
            {
                try
                {
                    var mensajeTcp = reader.ReadLine();
                    if (mensajeTcp != null) 
                    {
                        MensajeSocket<object> mensajeRecibido = JsonConvert.DeserializeObject<MensajeSocket<object>>(mensajeTcp);
                        SeleccionarMetodo(mensajeRecibido.Mensaje, mensajeTcp, ref servidorStreamWriter);
                    }
                }
                catch (Exception ex)
                {
                    txtStatus.Invoke(modificartxtStatus, new object[] { "Error: ComunicacionClienteB" + ex.Message});
                }
            }
            tcpClient.Close();
        }

        /// <summary>
        /// Metodo para identificar los procesos solicitados por los clientes
        /// </summary>
        /// <param name="accion"></param>
        /// <param name="mensajeTcp"></param>
        /// <param name="servidorStreamWriter"></param>
        private void SeleccionarMetodo(string accion, string mensajeTcp, ref StreamWriter servidorStreamWriter)
        {
            switch (accion)
            {
                case "Conectar":
                    MensajeSocket<string> mensajeConectar = JsonConvert.DeserializeObject<MensajeSocket<string>>(mensajeTcp);
                    ConectarCliente(mensajeConectar.Valor, ref servidorStreamWriter);
                    txtStatus.Invoke(modificartxtStatus, new object[] { "Nuevo cliente conectado "+mensajeConectar.Valor });
                    break;
                case "Desconectar":
                    DesconectarCliente(ref servidorStreamWriter);
                    break;
                case "Login Conductor":
                    MensajeSocket<Conductor> mensajeLoginConductor = JsonConvert.DeserializeObject<MensajeSocket<Conductor>>(mensajeTcp);
                    Conductor conductorLogin = mensajeLoginConductor.Valor;
                    ValidarUsuario(mensajeLoginConductor.Valor, ref servidorStreamWriter);
                    break;
                case "Registro Conductor":
                    MensajeSocket<Conductor> mensajeRegistroConductor = JsonConvert.DeserializeObject<MensajeSocket<Conductor>>(mensajeTcp);
                    Conductor conductorRegistro = mensajeRegistroConductor.Valor;
                    RegistroConductor(mensajeRegistroConductor.Valor, ref servidorStreamWriter);
                    txtStatus.Invoke(modificartxtStatus, new object[] { "Registro de Conductor: " + conductorRegistro.UserName });
                    break;
                case "Registro Viaje":
                    MensajeSocket<Viaje> mensajeRegistroViaje = JsonConvert.DeserializeObject<MensajeSocket<Viaje>>(mensajeTcp);
                    Viaje viajeRegistro = mensajeRegistroViaje.Valor;
                    RegistroViaje(mensajeRegistroViaje.Valor, ref servidorStreamWriter);
                    txtStatus.Invoke(modificartxtStatus, new object[] { "Registro de Viaje: " + viajeRegistro.Id_viaje});
                    break;
                case "Registro Tracking":
                    MensajeSocket<Tracking> mensajeRegistroTracking = JsonConvert.DeserializeObject<MensajeSocket<Tracking>>(mensajeTcp);
                    Tracking trackingRegistro = mensajeRegistroTracking.Valor;
                    RegistroTracking(mensajeRegistroTracking.Valor, ref servidorStreamWriter);
                    txtStatus.Invoke(modificartxtStatus, new object[] { "Registro de Tracking: " + trackingRegistro.Id_viaje });
                    break;
                case "Viaje Activo":
                    MensajeSocket<Conductor> mensajeViajeActivoConductor = JsonConvert.DeserializeObject<MensajeSocket<Conductor>>(mensajeTcp);
                    Conductor conductorViajeActivo = mensajeViajeActivoConductor.Valor;
                    ObtenerViajes(conductorViajeActivo.UserName, ref servidorStreamWriter);
                    break;
                case "Finalizar Viaje":
                    MensajeSocket<Viaje> mensajeFinalizarViaje = JsonConvert.DeserializeObject<MensajeSocket<Viaje>>(mensajeTcp);
                    Viaje viajeFinalizar = mensajeFinalizarViaje.Valor;
                    FinalizarViaje(mensajeFinalizarViaje.Valor, ref servidorStreamWriter);
                    txtStatus.Invoke(modificartxtStatus, new object[] { "Finalizar Viaje: " + viajeFinalizar.Id_viaje });
                    break;
                case "Notificacion de Cliente":
                    MensajeSocket<string> mensajeNotificacion = JsonConvert.DeserializeObject<MensajeSocket<string>>(mensajeTcp);
                    txtStatus.Invoke(modificartxtStatus, new object[] { "/***** Notificación de Cliente: ******/"});
                    txtStatus.Invoke(modificartxtStatus, new object[] { mensajeNotificacion.Valor });
                    break;
                default:  
                    {
                        MensajeSocket<string> mensajeDefault = JsonConvert.DeserializeObject<MensajeSocket<string>>(mensajeTcp);
                        txtStatus.Invoke(modificartxtStatus, new object[] { mensajeDefault.Valor });
                        break;
                    }
            }
        }

        /// <summary>
        /// Metodo para cambiar la cantidad de clientes
        /// </summary>
        /// <param name="servidorStreamWriter"></param>
        private void DesconectarCliente( ref StreamWriter servidorStreamWriter)
        {
            cuentaClientes -= 1;
            lblClientesConectados.Invoke(modificarlblClientesConectados, new object[] { "Clientes conectados: " + cuentaClientes.ToString() });
        }

        /// <summary>
        /// Metodo para cambiar el estado de un viaje activo
        /// </summary>
        /// <param name="viaje"></param>
        /// <param name="servidorStreamWriter"></param>
        private void FinalizarViaje(Viaje viaje, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            string resultadoFinalizarViaje = datalayer.RegistrarViajeFinalizado(viaje.Id_viaje);
            if (resultadoFinalizarViaje == "OKViajeActualizacion")
            {
                resultado.Mensaje = "Viaje finalizado!";
                resultado.Valor = true;
            }
            else
            {
                resultado.Mensaje = resultadoFinalizarViaje;
                resultado.Valor = false;
            }
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(resultado));
            servidorStreamWriter.Flush();
        }

        /// <summary>
        /// Metodo para insertar un registro al historial del viaje
        /// </summary>
        /// <param name="tracking"></param>
        /// <param name="servidorStreamWriter"></param>
        private void RegistroTracking(Tracking tracking, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            string resultadoRegistrarTracking = datalayer.RegistrarTracking(tracking);
            if (resultadoRegistrarTracking == "OKTracking")
            {
                resultado.Mensaje = "Tracking creado!";
                resultado.Valor = true;
            }
            else
            {
                resultado.Mensaje = resultadoRegistrarTracking;
                resultado.Valor = false;
            }
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(resultado));
            servidorStreamWriter.Flush();
        }

        /// <summary>
        /// Metodo para ingresar un viaje 
        /// </summary>
        /// <param name="viaje"></param>
        /// <param name="servidorStreamWriter"></param>
        private void RegistroViaje(Viaje viaje, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            string resultadoRegistrarViaje = datalayer.RegistrarViaje(viaje);
            if (resultadoRegistrarViaje == "OKViaje")
            {
                resultado.Mensaje = "Viaje creados!";
                resultado.Valor = true;
            }
            else
            {
                resultado.Mensaje = resultadoRegistrarViaje;
                resultado.Valor = false;
            }
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(resultado));
            servidorStreamWriter.Flush();
        }

        /// <summary>
        /// Metodo para ingresar un Conductor y Camion
        /// </summary>
        /// <param name="conductor"></param>
        /// <param name="servidorStreamWriter"></param>
        private void RegistroConductor(Conductor conductor, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            string resultadoRegistrarConductor = datalayer.RegistrarConductorCamion(conductor);
            if (resultadoRegistrarConductor== "OKConductor")
            {
                resultado.Mensaje = "Conductor y camion creados!";
                resultado.Valor = true;
            }
            else
            {
                resultado.Mensaje = resultadoRegistrarConductor;
                resultado.Valor = false;
            }
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(resultado));
            servidorStreamWriter.Flush();
        }

        /// <summary>
        /// Metodo para procesar una nueva coneccion de clientes
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="servidorStreamWriter"></param>
        private void ConectarCliente(string valor, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            resultado.Mensaje = "Ingreso del cliente valido " + valor;
            resultado.Valor = true;
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(resultado));
            servidorStreamWriter.Flush();
        }

        /// <summary>
        /// Metodo para consultar por el viaje activo de un conductor
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="servidorStreamWriter"></param>
        private void ObtenerViajes(string userName, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<Viaje> resultado = new MensajeSocket<Viaje>();
            Viaje viajeActivo = datalayer.getViajeActivoConductor(userName);
            if (viajeActivo!=null)
            {
                resultado.Mensaje = "Viaje activo " + viajeActivo.Id_viaje;
                resultado.Valor = viajeActivo;
            }
            else
            {
                resultado.Mensaje = "No tiene viajes activos"; 
                resultado.Valor = null;
            }
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(resultado));
            servidorStreamWriter.Flush();
            txtStatus.Invoke(modificartxtStatus, new object[] { " " + resultado.Mensaje });
        }

        /// <summary>
        /// Metodo para validar un usuario y su estado
        /// </summary>
        /// <param name="conductor"></param>
        /// <param name="servidorStreamWriter"></param>
        private void ValidarUsuario(Conductor conductor, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            string resultadoLogin = datalayer.UsuarioAcceso(conductor);
            if (resultadoLogin== "OKUsuarioAcceso")
            {
                resultado.Mensaje = "Ingreso de conductor valido";
                resultado.Valor = true;
            }
            else 
            {
                resultado.Mensaje = "Conductor no existe o esta denegado";
                resultado.Valor = false;
            }
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(resultado));
            servidorStreamWriter.Flush();
        }

        /// <summary>
        /// Metodo para dejeter el servidor TCP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetenerServidor_Click(object sender, EventArgs e)
        {
            continuar = false;

            try
            {
                foreach (TcpClient client in listaClientes)
                {
                    client.Close();
                }
                listaClientes.Clear();
                tcpListener.Stop();

            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Problem starting server." });
                txtStatus.Invoke(modificartxtStatus, new object[] { ex.ToString() });
            }

            txtStatus.Invoke(modificartxtStatus, new object[] { "Desconectando el servidor, desconectando los clientes." });
            btnIniciarServidor.Enabled = true;
            btnDetenerServidor.Enabled = false;
            btnCliente.Enabled = false;
            btnEnviarMensajeGrupal.Enabled = false;
        }

        /// <summary>
        /// Metodo para enviar el mensaje tipo broadcast
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnviarMensajeGrupal_Click(object sender, EventArgs e)
        {
            try
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Enviando mensaje a clientes: " + txtEnviarMensaje.Text });

                try
                {
                    foreach (TcpClient client in listaClientes)
                    {
                        //if (socket != null)
                        //{
                        //    bufferSend = Encoding.Unicode.GetBytes(txtEnviarMensaje.Text);
                        //    socket.Send(bufferSend);
                        //}
                        //else
                        //{
                        //    if (client.Connected)
                        //    {
                        //        bufferSend = Encoding.Unicode.GetBytes(txtEnviarMensaje.Text);
                        //        NetworkStream nts = client.GetStream();
                        //        if (nts.CanWrite)
                        //        {
                        //            nts.Write(bufferSend, 0, bufferSend.Length);
                        //        }

                        //    }
                        //}

                        //StreamWriter writer = new StreamWriter(client.GetStream());
                        //writer.WriteLine(txtEnviarMensaje.Text);
                        //writer.Flush();
                    }
                }
                catch (Exception)
                {
                }
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Problemas enviando mensaje a los clientes" });
                txtStatus.Invoke(modificartxtStatus, new object[] { ex.ToString() });
            }
        }

        /// <summary>
        /// Metodo para abrir un nuevo form de cliente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCliente_Click(object sender, EventArgs e)
        {
            try
            {
                frmCliente cliente = new frmCliente();
                cliente.Show();
            }
            catch (IOException iex) 
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Problemas de comunicación" });
                txtStatus.Invoke(modificartxtStatus, new object[] { iex.ToString() });
            }
            catch (ThreadAbortException tex) 
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Problemas de comunicación" });
                txtStatus.Invoke(modificartxtStatus, new object[] { tex.ToString() });
            }
            catch (Exception ex) 
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Problemas de comunicación" });
                txtStatus.Invoke(modificartxtStatus, new object[] { ex.ToString() });
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            btnDetenerServidor_Click(sender, e);
            Environment.Exit(0);
            Application.Exit();
        }

        /// <summary>
        /// Metodo para cargar todos los conductores al grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCargarConductor_Click(object sender, EventArgs e)
        {
            try
            {
                gvConductores.DataSource = datalayer.ConsultarConductores();
                gvConductores.Update();
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Error:" });
                txtStatus.Invoke(modificartxtStatus, new object[] { ex.ToString() });
            }
        }

        /// <summary>
        /// Metodo para habilitar el acceso del conductor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValidarConductor_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedIdentificacion;
                if (gvConductores.SelectedCells.Count > 0)
                {
                    int selectedrowindex = gvConductores.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = gvConductores.Rows[selectedrowindex];
                    selectedIdentificacion = Convert.ToString(selectedRow.Cells["Identificacion"].Value);

                    datalayer.ValidarConductor(selectedIdentificacion, "CONDUCTOR");
                    txtStatus.Invoke(modificartxtStatus, new object[] { "Conductor: " + Convert.ToString(selectedRow.Cells["Identificacion"].Value) + " validado." });
                    
                    btnCargarConductor_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Error:" });
                txtStatus.Invoke(modificartxtStatus, new object[] { ex.ToString() });
            }
        }

        /// <summary>
        /// Metodo para denegar el acceso al conductor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDenegarConductor_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedIdentificacion;
                if (gvConductores.SelectedCells.Count > 0)
                {
                    int selectedrowindex = gvConductores.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = gvConductores.Rows[selectedrowindex];
                    selectedIdentificacion = Convert.ToString(selectedRow.Cells["Identificacion"].Value);

                    txtStatus.Invoke(modificartxtStatus, new object[] { "Conductor: " + Convert.ToString(selectedRow.Cells["Identificacion"].Value) + " denegado." });

                    datalayer.ValidarConductor(selectedIdentificacion, "DENEGADO");
                    btnCargarConductor_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Error:" });
                txtStatus.Invoke(modificartxtStatus, new object[] { ex.ToString() });
            }
        }

        private void frmServidor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Metodo para mostrar todos los viajes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViajesTodos_Click(object sender, EventArgs e)
        {
            try
            {
                gvViajes.DataSource = datalayer.ConsultarViajes(false);
                gvViajes.Update();
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Error:" });
                txtStatus.Invoke(modificartxtStatus, new object[] { ex.ToString() });
            }
        }

        /// <summary>
        /// Metodo para mostrar todos los viajes activos 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViajesActivos_Click(object sender, EventArgs e)
        {
            try
            {
                gvViajes.DataSource = datalayer.ConsultarViajes(true);
                gvViajes.Update();
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Error:" });
                txtStatus.Invoke(modificartxtStatus, new object[] { ex.ToString() });
            }
        }

        /// <summary>
        /// Metodo para mostrar los tracking del viaje seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvViajes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string selectedIdViaje;
                if (gvViajes.SelectedCells.Count > 0)
                {
                    int selectedrowindex = gvViajes.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = gvViajes.Rows[selectedrowindex];
                    selectedIdViaje = Convert.ToString(selectedRow.Cells["Id_viaje"].Value);

                    gvViajesTracking.DataSource = datalayer.ConsultarViajesTracking(selectedIdViaje);
                    gvViajesTracking.Update();

                }
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "Error:" });
                txtStatus.Invoke(modificartxtStatus, new object[] { ex.ToString() });
            }
        }
    }
}
