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
        delegate void ModificartxtStatusDelegado(string mensaje);
        delegate void ModificarlblClientesConectadosDelegado(string mensaje);

        List<TcpClient> listaClientes;
        TcpListener tcpListener;
        Thread subprocesoEscuchaClientes;
        int cuentaClientes;
        bool continuar;
        IPAddress local = IPAddress.Parse("127.0.0.1");
        int intPort = 16830;

        private Socket socket;
        byte[] bufferReceive = new byte[4096];
        byte[] bufferSend = new byte[4096];

        DataLayer datalayer = new DataLayer();

        ModificartxtStatusDelegado modificartxtStatus;
        ModificarlblClientesConectadosDelegado modificarlblClientesConectados;

        // lblClientesConectados.Invoke(modificarlblClientesConectados, new object[] { "Clientes conectados: " + cuentaClientes.ToString()});
        // txtStatus.Invoke(modificartxtStatus, new object[] { "->Nuevo cliente conectado " });
        public frmServidor()
        {
            InitializeComponent();
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
        private void ModificartxtStatus(string mensaje) 
        {
            txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " +mensaje;
            txtStatus.SelectionStart = txtStatus.Text.Length;
            txtStatus.ScrollToCaret();
        }
        private void ModificarlblClientesConectados(string mensaje)
        {
            lblClientesConectados.Text = mensaje;
        }

        private void btnIniciarServidor_Click(object sender, EventArgs e)
        {
            try
            {
                lblClientesConectados.Text = "Clientes conectados: 0";
                cuentaClientes = 0;
                listaClientes.Clear();

                txtStatus.Text += DateTime.Now.ToString("T") + "->Servidor iniciando en (" + local.ToString() +":"+ intPort.ToString() + ") ";
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();

                btnIniciarServidor.Enabled = false;
                btnDetenerServidor.Enabled = true;
                btnCliente.Enabled = true;
                btnEnviarMensajeGrupal.Enabled = true;

                continuar = true;
                tcpListener = new TcpListener(local, intPort);
                subprocesoEscuchaClientes = new Thread(new ThreadStart(EscuchaClientes));
                //subprocesoEscuchaClientes.Name = "Servidor Listener Thread";
                subprocesoEscuchaClientes.Start();
                subprocesoEscuchaClientes.IsBackground = true;


                
            }
            catch (Exception ex)
            {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Problem starting server.";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" +ex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
        }

        
        

        private void EscuchaClientes()
        {
            try
            {
                //continuar = true;
                //tcpListener = new TcpListener(local, intPort);
                tcpListener.Start();
                txtStatus.Invoke(modificartxtStatus, new object[] { "Server iniciado. escuchando en :" + local.ToString() + ":" + intPort.ToString() + ") " });

                while (continuar)
                {
                    txtStatus.Invoke(modificartxtStatus, new object[] { "Esperando por clientes..." });
                    TcpClient client = tcpListener.AcceptTcpClient();   // blocks here until client connects
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
                // swallow this one exception
                // _statusTextBox.InvokeEx(stb => stb.Text += CRLF + "Problem starting the server.");
                // _statusTextBox.InvokeEx(stb => stb.Text += CRLF + se.ToString());
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(modificartxtStatus, new object[] { "**Problemas iniciando el servidor**" });
                txtStatus.Invoke(modificartxtStatus, new object[] { "Error: EscuchaClientes " + ex.ToString() });
            }
            txtStatus.Invoke(modificartxtStatus, new object[] { "**Terminando el listener y thread**" });
            txtStatus.Invoke(modificartxtStatus, new object[] { " " });
        } // end ListenForIncomingConnections() method

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
                    //MensajeSocket<int> mensajeDesconectar = JsonConvert.DeserializeObject<MensajeSocket<int>>(mensajeTcp);
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
                    //txtStatus.Invoke(modificartxtStatus, new object[] { "Viaje Activo: " + conductorViajeActivo.Identificacion });
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
                default:  // default case acts as echo server
                    {
                        MensajeSocket<string> mensajeDefault = JsonConvert.DeserializeObject<MensajeSocket<string>>(mensajeTcp);
                        txtStatus.Invoke(modificartxtStatus, new object[] { mensajeDefault.Valor });
                        break;
                    }
            }
        }

        private void DesconectarCliente( ref StreamWriter servidorStreamWriter)
        {
            //listaClientes.Remove(client);
            //client.Close();
            cuentaClientes -= 1;

            

            lblClientesConectados.Invoke(modificarlblClientesConectados, new object[] { "Clientes conectados: " + cuentaClientes.ToString() });
        }

        private void FinalizarViaje(Viaje viaje, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            string resultadoFinalizarViaje = datalayer.RegistrarViajeFinalizado(viaje.Id_viaje);
            //if (DatosRegistroConductor(conductor))
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

        private void RegistroTracking(Tracking tracking, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            string resultadoRegistrarTracking = datalayer.RegistrarTracking(tracking);
            //if (DatosRegistroConductor(conductor))
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

        private void RegistroViaje(Viaje viaje, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            string resultadoRegistrarViaje = datalayer.RegistrarViaje(viaje);
            //if (DatosRegistroConductor(conductor))
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

        private void RegistroConductor(Conductor conductor, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            string resultadoRegistrarConductor = datalayer.RegistrarConductorCamion(conductor);
            //if (DatosRegistroConductor(conductor))
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

        private void ConectarCliente(string valor, ref StreamWriter servidorStreamWriter)
        {
            MensajeSocket<bool> resultado = new MensajeSocket<bool>();
            resultado.Mensaje = "Ingreso del cliente valido " + valor;
            resultado.Valor = true;
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(resultado));
            servidorStreamWriter.Flush();
        }
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
                resultado.Mensaje = "No tiene viajes activos"; // + viajeActivo.Id_viaje;
                resultado.Valor = null;
            }
            servidorStreamWriter.WriteLine(JsonConvert.SerializeObject(resultado));
            servidorStreamWriter.Flush();
            txtStatus.Invoke(modificartxtStatus, new object[] { " " + resultado.Mensaje });
        }
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
            //txtStatus.Invoke(modificartxtStatus, new object[] { "Ingreso de Conductor: " + conductor.UserName });
            //txtStatus.Invoke(modificartxtStatus, new object[] { "  " + resultado.Mensaje });
        }
        private bool DatosValidarUsuario(Conductor conductor)
        {
            bool resultadoValor = false;
            string resultadoLogin = datalayer.UsuarioAcceso(conductor);
            if(resultadoLogin == "OKUsuarioAcceso")
            {
                resultadoValor = true;
            }
            return resultadoValor;
        }
        private void btnDetenerServidor_Click(object sender, EventArgs e)
        {
            continuar = false;
            //txtStatus.Text = string.Empty;
            txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Desconectando el servidor, desconectando los clientes.";
            txtStatus.SelectionStart = txtStatus.Text.Length;
            txtStatus.ScrollToCaret();

            try
            {
                foreach (TcpClient client in listaClientes)
                {
                    client.Close();
                }
                listaClientes.Clear();
                tcpListener.Stop();

            }
            catch (Exception)
            {
                // Swallow the exception
                //_statusTextBox.InvokeEx(stb => stb.Text += CRLF + "Problem stopping the server, or client connections forcibly closed...");
                //_statusTextBox.InvokeEx(stb => stb.Text += CRLF + ex.ToString());
            }

            btnIniciarServidor.Enabled = true;
            btnDetenerServidor.Enabled = false;
            btnCliente.Enabled = false;
            btnEnviarMensajeGrupal.Enabled = false;
            //txtStatus.Text = string.Empty;
            
        }

        private void btnEnviarMensajeGrupal_Click(object sender, EventArgs e)
        {
            try
            {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Enviando mensaje a clientes: "+ txtEnviarMensaje.Text;
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();

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
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Problemas enviando mensaje a los clientes";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + ex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            try
            {
                frmCliente cliente = new frmCliente();
                cliente.Show();
            }
            catch (IOException iex) 
            {
                txtStatus.Text += DateTime.Now.ToString("T") + "->Problemas de comunicación";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + iex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
            catch (ThreadAbortException tex) 
            {
                txtStatus.Text += DateTime.Now.ToString("T") + "->Problemas con threads";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + tex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
            catch (Exception ex) 
            {
                txtStatus.Text += DateTime.Now.ToString("T") + "->Error:";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + ex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            btnDetenerServidor_Click(sender, e);
            Environment.Exit(0);
            Application.Exit();
        }

        private void btnCargarConductor_Click(object sender, EventArgs e)
        {
            try
            {
                gvConductores.DataSource = datalayer.ConsultarConductores();
                gvConductores.Update();
            }
            catch (Exception ex)
            {
                txtStatus.Text += DateTime.Now.ToString("T") + "->Error:";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + ex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
        }

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
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Conductor: "+ Convert.ToString(selectedRow.Cells["Identificacion"].Value)+" validado.";
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                    btnCargarConductor_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text += DateTime.Now.ToString("T") + "->Error:";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + ex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
        }

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
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Conductor: " + Convert.ToString(selectedRow.Cells["Identificacion"].Value) + " denegado.";
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();

                    datalayer.ValidarConductor(selectedIdentificacion, "DENEGADO");
                    btnCargarConductor_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text += DateTime.Now.ToString("T") + "->Error:";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + ex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
        }

        private void frmServidor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnViajesTodos_Click(object sender, EventArgs e)
        {
            try
            {
                gvViajes.DataSource = datalayer.ConsultarViajes(false);
                gvViajes.Update();
            }
            catch (Exception ex)
            {
                txtStatus.Text += DateTime.Now.ToString("T") + "->Error:";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + ex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
        }

        private void btnViajesActivos_Click(object sender, EventArgs e)
        {
            try
            {
                gvViajes.DataSource = datalayer.ConsultarViajes(true);
                gvViajes.Update();
            }
            catch (Exception ex)
            {
                txtStatus.Text += DateTime.Now.ToString("T") + "->Error:";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + ex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
        }

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
                txtStatus.Text += DateTime.Now.ToString("T") + "->Error:";
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + ex.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }
        }
    }
}
