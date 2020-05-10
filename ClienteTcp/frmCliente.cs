using Newtonsoft.Json;
using System;
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

namespace ClienteTcp
{
    public partial class frmCliente : Form
    {
        public static bool clienteConectado;
        private static IPAddress ipServidor = IPAddress.Parse("127.0.0.1");
        private static int intPort = 16830;
        private static IPEndPoint serverEndPoint;
        private static StreamWriter clienteStreamWriter;
        private static StreamReader clienteStreamReader;

        private TcpClient cliente;

        private delegate void ModificartxtStatusDelegado(string mensaje);
        ModificartxtStatusDelegado modificartxtStatus;

        public frmCliente()
        {
            InitializeComponent();
            clienteConectado = false;
            tabIngreso.Parent = tabMain; 
            tabRegistroConductor.Parent = tabTrash; 
            tabIngresoViajes.Parent = tabTrash; 
            tabViajes.Parent = tabTrash; 
            modificartxtStatus = new ModificartxtStatusDelegado(ModificartxtStatus);
        }

        private void ModificartxtStatus(string mensaje)
        {
            txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " + mensaje;
            txtStatus.SelectionStart = txtStatus.Text.Length;
            txtStatus.ScrollToCaret();
        }
        
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cliente.Connected)
                {
                    Conductor conductor = new Conductor("", "", "", "", "", txtUsuario.Text, "","","","");
                    MensajeSocket<Conductor> mensajeIngresar = new MensajeSocket<Conductor> { Mensaje = "Login Conductor", Valor = conductor };

                    clienteStreamReader = new StreamReader(cliente.GetStream());
                    clienteStreamWriter = new StreamWriter(cliente.GetStream());
                    clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeIngresar));
                    clienteStreamWriter.Flush();
                    string mensaje = clienteStreamReader.ReadLine();
                    MensajeSocket<bool> resultado;
                    resultado = JsonConvert.DeserializeObject<MensajeSocket<bool>>(mensaje);

                    if (resultado.Valor)
                    {
                        modificartxtStatus("Ingreso de Conductor: " + txtUsuario.Text);

                        CargarViajeActivo(conductor);
                    }
                    else
                    {
                        modificartxtStatus("Ingreso de Conductor: " + txtUsuario.Text);
                        modificartxtStatus(resultado.Mensaje);
                    }
                }                
            }
            catch (IOException iex) {
                modificartxtStatus("Problema ingreando usuario.");
                modificartxtStatus(iex.ToString());
                clienteConectado = false;
            }
            catch (SocketException sex)
            {
                modificartxtStatus("Problema ingreando usuario.");
                modificartxtStatus(sex.ToString());
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                modificartxtStatus("Problema ingreando usuario.");
                modificartxtStatus(ex.ToString());
            }
        }

        private void CargarViajeActivo(Conductor conductor)
        {
            try
            {
                MensajeSocket<Conductor> mensajeViajeActivo = new MensajeSocket<Conductor> { Mensaje = "Viaje Activo", Valor = conductor };
                clienteStreamReader = new StreamReader(cliente.GetStream());
                clienteStreamWriter = new StreamWriter(cliente.GetStream());
                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeViajeActivo));
                clienteStreamWriter.Flush();
                string mensajeActivo = clienteStreamReader.ReadLine();
                MensajeSocket<Viaje> resultadoViajeActivo;
                resultadoViajeActivo = JsonConvert.DeserializeObject<MensajeSocket<Viaje>>(mensajeActivo);
                if (resultadoViajeActivo.Valor != null) 
                {
                    tabViajes.Parent = tabMain;
                    tabIngreso.Parent = tabTrash;
                    lblGUIDActivo.Text = resultadoViajeActivo.Valor.Id_viaje;
                }
                else
                {
                    tabIngresoViajes.Parent = tabMain;
                    tabIngreso.Parent = tabTrash;
                }
            }
            catch (Exception ex)
            {
                modificartxtStatus("Problema cargar viaje activo.");
                modificartxtStatus(ex.ToString());
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                if (cliente!=null) 
                {
                    MensajeSocket<int> mensajeIngresar = new MensajeSocket<int> { Mensaje = "Desconectar", Valor = cliente.GetHashCode() };

                    clienteStreamReader = new StreamReader(cliente.GetStream());
                    clienteStreamWriter = new StreamWriter(cliente.GetStream());
                    clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeIngresar));
                    clienteStreamWriter.Flush();
                    cliente.Close();
                }
                                
                this.Close();
            }
            catch (Exception ex)
            {
                modificartxtStatus("Problema al salir.");
                modificartxtStatus(ex.ToString());
            }

        }
        private void linkRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (cliente.Connected)
                {
                    clienteConectado = true;
                    //tabIngreso.Parent = tabMain; //show
                    tabIngreso.Parent = null; // hide    
                    //tabRegistroConductor.Parent = null; // hide    
                    tabRegistroConductor.Parent = tabMain; //show
                    tabIngresoViajes.Parent = null; // hide    
                    //tabIngresoViajes.Parent = tabMain; //show
                    tabViajes.Parent = null; // hide    
                    //tabViajes.Parent = tabMain; //show
                }
                else
                {
                    clienteConectado = false;
                }
                btnRegistroConductor.Enabled = clienteConectado;

            }
            catch (SocketException sex)
            {
                modificartxtStatus("Error:");
                modificartxtStatus(sex.ToString());
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                modificartxtStatus("Error:");
                modificartxtStatus(ex.ToString());
            }
        }
        private void btnRegistroConductor_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtIdentificacion.Text.Trim()) || !string.IsNullOrWhiteSpace(txtIdentificacion.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtNombre.Text.Trim()) || !string.IsNullOrWhiteSpace(txtNombre.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtPApellido.Text.Trim()) || !string.IsNullOrWhiteSpace(txtPApellido.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtSApellido.Text.Trim()) || !string.IsNullOrWhiteSpace(txtSApellido.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtUserName.Text.Trim()) || !string.IsNullOrWhiteSpace(txtUserName.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtPlaca.Text.Trim()) || !string.IsNullOrWhiteSpace(txtPlaca.Text.Trim()) ||
                    !string.IsNullOrEmpty(cbMarca.Text.Trim()) || !string.IsNullOrWhiteSpace(cbMarca.Text.Trim())
                    )
                {
                    Conductor conductor = new Conductor(txtIdentificacion.Text.Trim(), txtNombre.Text.Trim(), txtPApellido.Text.Trim(),
                                txtSApellido.Text.Trim(), "", txtUserName.Text.Trim(), "PENDIENTE", 
                                txtPlaca.Text.Trim(), numModelo.Value.ToString(), cbMarca.Text.Trim());
                    
                    MensajeSocket <Conductor> mensajeRegistroConductor = new MensajeSocket<Conductor> { Mensaje = "Registro Conductor", Valor = conductor };

                    clienteStreamReader = new StreamReader(cliente.GetStream());
                    clienteStreamWriter = new StreamWriter(cliente.GetStream());
                    clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeRegistroConductor));
                    clienteStreamWriter.Flush();
                    string mensaje = clienteStreamReader.ReadLine();
                    MensajeSocket<bool> resultado;
                    resultado = JsonConvert.DeserializeObject<MensajeSocket<bool>>(mensaje);

                    if (resultado.Valor)
                    {
                        modificartxtStatus("Registro de Conductor: " + txtIdentificacion.Text);
                        modificartxtStatus("Registro de Usuario: " + txtUserName.Text);
                        modificartxtStatus("Registro de Camion: " + txtPlaca.Text);

                        tabIngreso.Parent = tabMain;
                        tabRegistroConductor.Parent = tabTrash;
                    }
                    else
                    {
                        modificartxtStatus("Registro de Conductor: " + txtIdentificacion.Text);
                        modificartxtStatus(resultado.Mensaje);
                    }
                }
                else
                {
                    modificartxtStatus("Verifique los datos, todos los campos son obligatorios.");
                }
            }
            catch (SocketException sex)
            {
                modificartxtStatus("Problema registro de conductor.");
                modificartxtStatus(sex.ToString());
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                modificartxtStatus("Problema registro del conductor.");
                modificartxtStatus(ex.ToString());
            }
        }
        private void btnIngresoViaje_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLugarInicio.Text.Trim()) || !string.IsNullOrWhiteSpace(txtLugarInicio.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtLugarFinal.Text.Trim()) || !string.IsNullOrWhiteSpace(txtLugarFinal.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtDescripcionCarga.Text.Trim()) || !string.IsNullOrWhiteSpace(txtDescripcionCarga.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtTiempoEstimado.Text.Trim()) || !string.IsNullOrWhiteSpace(txtTiempoEstimado.Text.Trim()) 
                    )
                {
                    string idviaje = Guid.NewGuid().ToString();
                    Viaje viaje = new Viaje(idviaje
                        , txtIdentificacion.Text.Trim()
                        , txtLugarInicio.Text.Trim()
                        , txtLugarFinal.Text.Trim()
                        , txtDescripcionCarga.Text.Trim()
                        , txtTiempoEstimado.Text.Trim()
                        , "ACTIVO");
                    MensajeSocket<Viaje> mensajeRegistroConductor = new MensajeSocket<Viaje> { Mensaje = "Registro Viaje", Valor = viaje };

                    clienteStreamReader = new StreamReader(cliente.GetStream());
                    clienteStreamWriter = new StreamWriter(cliente.GetStream());
                    clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeRegistroConductor));
                    clienteStreamWriter.Flush();
                    string mensaje = clienteStreamReader.ReadLine();
                    MensajeSocket<bool> resultado;
                    resultado = JsonConvert.DeserializeObject<MensajeSocket<bool>>(mensaje);

                    if (resultado.Valor)
                    {
                        //lblGUIDActivo.Invoke(new MethodInvoker(delegate {lblGUIDActivo.Text = idviaje;}));
                        lblGUIDActivo.Text = idviaje;
                        
                        modificartxtStatus("Registro de Viaje: " + txtIdentificacion.Text);

                        tabViajes.Parent = tabMain;
                        tabIngresoViajes.Parent = tabTrash;
                    }
                    else
                    {
                        modificartxtStatus("Registro de Conductor: " + txtUsuario.Text);
                    }
                }
                else
                {
                    modificartxtStatus("Verifique los datos, todos los campos son obligatorios.");
                }
            }
            catch (SocketException sex)
            {
                modificartxtStatus("Error: ");
                modificartxtStatus(sex.ToString());
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                modificartxtStatus("Error: ");
                modificartxtStatus(ex.ToString());
            }
        }
        private void btnConectarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                serverEndPoint = new IPEndPoint(ipServidor, intPort);
                cliente = new TcpClient();
                cliente.Connect(serverEndPoint);

                MensajeSocket<string> mensajeConectar = new MensajeSocket<string> { Mensaje = "Conectar", Valor = Convert.ToString(cliente.GetHashCode()) };

                clienteStreamReader = new StreamReader(cliente.GetStream());
                clienteStreamWriter = new StreamWriter(cliente.GetStream());
                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeConectar));
                clienteStreamWriter.Flush();
                string mensaje = clienteStreamReader.ReadLine();
                MensajeSocket<bool> resultado;
                resultado = JsonConvert.DeserializeObject<MensajeSocket<bool>>(mensaje);
                modificartxtStatus(resultado.Mensaje);

                clienteConectado = true;
                btnConectarCliente.Enabled = false;
                btnIngresar.Enabled = true;
                linkRegistrarse.Enabled = true;
                btnRegistroConductor.Enabled = true;
            }
            catch (Exception ex)
            {
                modificartxtStatus("Problemas conectando el servidor.");
                modificartxtStatus(ex.ToString());
            }
        }
       
    
        private void btnActualizarViaje_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNuevaUbicacion.Text.Trim()) || !string.IsNullOrWhiteSpace(txtNuevaUbicacion.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtObservaciones.Text.Trim()) || !string.IsNullOrWhiteSpace(txtObservaciones.Text.Trim()) 
                    )
                {
                    Tracking tracking = new Tracking(lblGUIDActivo.Text.Trim(), txtNuevaUbicacion.Text.Trim(), txtObservaciones.Text.Trim());
                    MensajeSocket<Tracking> mensajeRegistroTracking = new MensajeSocket<Tracking> { Mensaje = "Registro Tracking", Valor = tracking };

                    clienteStreamReader = new StreamReader(cliente.GetStream());
                    clienteStreamWriter = new StreamWriter(cliente.GetStream());
                    clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeRegistroTracking));
                    clienteStreamWriter.Flush();
                    string mensaje = clienteStreamReader.ReadLine();
                    MensajeSocket<bool> resultado;
                    resultado = JsonConvert.DeserializeObject<MensajeSocket<bool>>(mensaje);

                    if (resultado.Valor)
                    {
                        modificartxtStatus("Registro de Tracking: " + lblGUIDActivo.Text.Trim());
                    }
                    else
                    {
                        modificartxtStatus("Registro de Tracking: " + txtUsuario.Text);
                    }
                }
                else
                {
                    modificartxtStatus("Verifique los datos, todos los campos son obligatorios.");
                }


            }
            catch (SocketException sex)
            {
                modificartxtStatus("Error");
                modificartxtStatus(sex.ToString());
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                modificartxtStatus("Error");
                modificartxtStatus(ex.ToString());
            }
        }

        private void btnFinalizarViaje_Click(object sender, EventArgs e)
        {
            try
            {
                string idviaje = Guid.NewGuid().ToString();
                Viaje viaje = new Viaje(idviaje
                    , txtUsuario.Text.Trim()
                    , txtLugarInicio.Text.Trim()
                    , txtLugarFinal.Text.Trim()
                    , txtDescripcionCarga.Text.Trim()
                    , txtTiempoEstimado.Text.Trim()
                    , "FINALIZADO");
                MensajeSocket<Viaje> mensajeRegistroConductor = new MensajeSocket<Viaje> { Mensaje = "Finalizar Viaje", Valor = viaje };

                clienteStreamReader = new StreamReader(cliente.GetStream());
                clienteStreamWriter = new StreamWriter(cliente.GetStream());
                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeRegistroConductor));
                clienteStreamWriter.Flush();
                string mensaje = clienteStreamReader.ReadLine();
                MensajeSocket<bool> resultado;
                resultado = JsonConvert.DeserializeObject<MensajeSocket<bool>>(mensaje);

                if (resultado.Valor)
                {
                    lblGUIDActivo.Text = idviaje;
                    modificartxtStatus("Finalizar Viaje: " + txtIdentificacion.Text);
                    
                    tabIngresoViajes.Parent = tabMain;
                    tabViajes.Parent = tabTrash;
                }
                else
                {
                    modificartxtStatus("Finalizar Viaje: " + txtUsuario.Text);
                    modificartxtStatus(resultado.Mensaje);
                }

            }
            catch (SocketException sex)
            {
                modificartxtStatus("Error:");
                modificartxtStatus(sex.ToString());
            }
            catch (Exception ex)
            {
                modificartxtStatus("Error:");
                modificartxtStatus(ex.ToString());
            }
        }

        private void frmCliente_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnEnviarMensaje_Click(object sender, EventArgs e)
        {
            try
            {
                MensajeSocket<string> mensajeNoticacion = new MensajeSocket<string> { 
                    Mensaje = "Notificacion de Cliente"
                    , Valor = " Usuario: " + txtUsuario.Text + " - " +txtEnviarMensaje.Text.Trim() };

                clienteStreamReader = new StreamReader(cliente.GetStream());
                clienteStreamWriter = new StreamWriter(cliente.GetStream());
                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeNoticacion));
                clienteStreamWriter.Flush();
            }
            catch (Exception ex)
            {
                modificartxtStatus("Error:");
                modificartxtStatus(ex.ToString());
            }
        }
    }
}
