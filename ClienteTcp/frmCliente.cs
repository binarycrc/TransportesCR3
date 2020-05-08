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
        //private BinaryWriter escritor;
        //private BinaryReader lector;
        //private BinaryFormatter bf;

        ModificartxtStatusDelegado modificartxtStatus;
        ModificarlblClientesConectadosDelegado modificarlblClientesConectados;
        private delegate void ModificartxtStatusDelegado(string mensaje);
        private delegate void ModificarlblClientesConectadosDelegado(string mensaje);

        // lblClientesConectados.Invoke(modificarlblClientesConectados, new object[] { "Clientes conectados: " + cuentaClientes.ToString()});
        // txtStatus.Invoke(modificartxtStatus, new object[] { "->Nuevo cliente conectado " });

        public frmCliente()
        {
            InitializeComponent();
            clienteConectado = false;
            tabIngreso.Parent = tabMain; //show
            tabRegistroConductor.Parent = tabTrash; // hide    
            //tabRegistroConductor.Parent = tabMain; //show
            tabIngresoViajes.Parent = tabTrash; // hide    
            //tabIngresoViajes.Parent = tabMain; //show
            tabViajes.Parent = tabTrash; // hide    
            //tabViajes.Parent = tabMain; //show
            //timerCliente.Start();
        }

        private void ModificartxtStatus(string mensaje)
        {
            txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " + mensaje;
            txtStatus.SelectionStart = txtStatus.Text.Length;
            txtStatus.ScrollToCaret();
        }
        private void ModificarlblClientesConectados(string mensaje)
        {
            
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
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Ingreso de Conductor: " + txtUsuario.Text;
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();

                        CargarViajeActivo(conductor);

                    }
                    else
                    {
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Ingreso de Conductor: " + txtUsuario.Text;
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " + resultado.Mensaje;
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();
                    }
                }

                
            }
            catch (IOException iex) {
                lblMensaje.Text = iex.Message;
                clienteConectado = false;
            }
            catch (SocketException sex)
            {
                lblMensaje.Text = sex.Message;
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                clienteConectado = false;
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                //throw;
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
                lblMensaje.Text = sex.Message;
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnRegistroConductor_Click(object sender, EventArgs e)
        {
            string test = "";
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
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Registro de Conductor: " + txtIdentificacion.Text;
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Registro de Usuario: " + txtUserName.Text;
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Registro de Camion: " + txtPlaca.Text;
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();

                        tabIngreso.Parent = tabMain;
                        
                        tabRegistroConductor.Parent = tabTrash;
                        //tabIngresoViajes.Invoke(new MethodInvoker(delegate {
                            
                        //}));

                        //tabIngreso.Invoke(new MethodInvoker(delegate {
                            
                        //}));
                    }
                    else
                    {
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Registro de Conductor: " + txtUsuario.Text;
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " + resultado.Mensaje;
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();
                    }


                }
                else
                {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Verifique los datos, todos los campos son obligatorios.";
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }
            }
            catch (SocketException sex)
            {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Error: "+ sex.Message;
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Error: " + ex.Message;
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
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
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Registro de Viaje: " + txtIdentificacion.Text;
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();

                        tabViajes.Parent = tabMain;
                        tabIngresoViajes.Parent = tabTrash;
                        //tabIngresoViajes.Invoke(new MethodInvoker(delegate {
                        //    tabIngresoViajes.Parent = tabMain;
                        //}));

                        //tabIngreso.Invoke(new MethodInvoker(delegate {
                        //    tabIngreso.Parent = tabTrash;
                        //}));
                    }
                    else
                    {
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Registro de Conductor: " + txtUsuario.Text;
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " + resultado.Mensaje;
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();
                    }


                }
                else
                {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Verifique los datos, todos los campos son obligatorios.";
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }


            }
            catch (SocketException sex)
            {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Error: " + sex.Message;
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Error: " + ex.Message;
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
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
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " + resultado.Mensaje;
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();

                clienteConectado = true;
                btnConectarCliente.Enabled = false;
                btnIngresar.Enabled = true;
                linkRegistrarse.Enabled = true;
                btnRegistroConductor.Enabled = true;


            }
            catch (Exception ex)
            {
                txtStatus.Text += "\r\n" + "Problemas conectando el servidor.";
                txtStatus.Text += "\r\n" + ex.ToString();
            }
        }

        private void EscuchaClientes(object cliente)
        {
            //TcpClient client = (TcpClient)cliente;
            //string input = string.Empty;

            //NetworkStream clienteStream = client.GetStream();
            //escritor = new BinaryWriter(clienteStream);
            //lector = new BinaryReader(clienteStream);
            //BinaryFormatter bf = new BinaryFormatter();
            ////clienteStream.Flush();

            //String accion = string.Empty;

            //try
            //{
            //    escritor.Write("Nuevo Cliente");
            //    escritor.Flush();

            //    while (client.Connected)
            //    {
            //        //input = reader.ReadLine(); // block here until we receive something from the server.
            //        accion = lector.ReadString();
            //        //if (input == null)
            //        if (accion == null)
            //        {
            //            Desconectardesdeservidor();
            //        }
            //        else
            //        {
            //            switch (accion)
            //            {
            //                case "OKUsuarioAcceso":
            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    tabIngresoViajes.Invoke(new MethodInvoker(delegate {
            //                        tabIngresoViajes.Parent = tabMain;
            //                    }));

            //                    tabIngreso.Invoke(new MethodInvoker(delegate {
            //                        tabIngreso.Parent = tabTrash;
            //                    }));
            //                    break;
            //                case "DenegadoUsuarioAcceso":
            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    MessageBox.Show("El conductor no existe o tiene el acceso denegado!");
            //                    break;
            //                case "OKConductor":
            //                    tabIngreso.Invoke(new MethodInvoker(delegate {
            //                        tabIngreso.Parent = tabMain;
            //                    }));
            //                    tabRegistroConductor.Invoke(new MethodInvoker(delegate {
            //                        tabRegistroConductor.Parent = tabTrash;
            //                    }));
            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    MessageBox.Show("Conductor y camion creados!");
            //                    break;

            //                case "ExistenteConductor":
            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    MessageBox.Show("El conductor con la identificacion " + txtIdentificacion.Text + ", ya existe!");
            //                    break;

            //                case "ExistenteUsuario":
            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    MessageBox.Show("El conductor con el Usuario " + txtUsuario.Text + ", ya existe!");
            //                    break;
            //                case "ExistenteCamion":
            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    MessageBox.Show("El camion con la placa " + txtPlaca.Text + ", ya existe!");
            //                    break;
            //                case "OKViaje":
            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    tabIngresoViajes.Invoke(new MethodInvoker(delegate {
            //                        tabIngresoViajes.Parent = tabTrash;
            //                    }));

            //                    tabViajes.Invoke(new MethodInvoker(delegate {
            //                        tabViajes.Parent = tabMain;
            //                    }));
            //                    break;
            //                case "TieneViajeActivo":
            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    MessageBox.Show("El conductor: " + txtUsuario.Text + ", ya tiene un viaje acitvo.");
            //                    break;
            //                case "OKViajeActualizacion":
            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    break;
            //                case "OKViajeFinalizado":
            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    break;
            //                case "ConductorActivo":
            //                    Conductor conductorActivo = (Conductor)(bf.Deserialize(clienteStream));
            //                    Viaje viajeActivo = (Viaje)(bf.Deserialize(clienteStream));
            //                    txtIdentificacion.Invoke(new MethodInvoker(delegate {
            //                        txtIdentificacion.Text = conductorActivo.Identificacion;
            //                    }));
            //                    txtNombre.Invoke(new MethodInvoker(delegate {
            //                        txtNombre.Text = conductorActivo.Nombre;
            //                    }));
            //                    txtPApellido.Invoke(new MethodInvoker(delegate {
            //                        txtPApellido.Text = conductorActivo.PApellido;
            //                    }));
            //                    txtSApellido.Invoke(new MethodInvoker(delegate {
            //                        txtSApellido.Text = conductorActivo.SApellido;
            //                    }));
            //                    txtUserName.Invoke(new MethodInvoker(delegate {
            //                        txtUserName.Text = conductorActivo.UserName;
            //                    }));

            //                    lblGUIDActivo.Invoke(new MethodInvoker(delegate {
            //                        lblGUIDActivo.Text = viajeActivo.Id_viaje;
            //                    }));

            //                    txtStatus.Invoke(new MethodInvoker(delegate {
            //                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
            //                        txtStatus.SelectionStart = txtStatus.Text.Length;
            //                        txtStatus.ScrollToCaret();
            //                    }));
            //                    tabIngreso.Invoke(new MethodInvoker(delegate {
            //                        tabIngreso.Parent = tabTrash;
            //                    }));
            //                    tabViajes.Invoke(new MethodInvoker(delegate {
            //                        tabViajes.Parent = tabMain;
            //                    }));
            //                    break;
            //                default:
            //                    {
            //                        txtStatus.Invoke(new MethodInvoker(delegate {
            //                            txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->"+ accion;
            //                            txtStatus.SelectionStart = txtStatus.Text.Length;
            //                            txtStatus.ScrollToCaret();
            //                        }));
            //                        break;
            //                    }
            //            } // end switch
            //        } // end if/else


            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (client.Connected) 
            //    {
            //        txtStatus.Invoke(new MethodInvoker(delegate {
            //            txtStatus.Text += "\r\n" + "Problemas de comunicacion con el servidor";
            //            txtStatus.Text += "\r\n" + ex.Message;
            //        }));
            //    }
            //}
            //if (client.Connected)
            //{
            //    btnConectarCliente.Invoke(new MethodInvoker(delegate
            //    {
            //        btnConectarCliente.Enabled = true;
            //    }));
            //    txtStatus.Invoke(new MethodInvoker(delegate
            //    {
            //        txtStatus.Text += "\r\n" + string.Empty;
            //    }));
            //}
        }

        private void Desconectardesdeservidor()
        {
            try
            {
                cliente.Close();
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + "Desconectado desde el servidor";
                }));
                btnConectarCliente.Invoke(new MethodInvoker(delegate {
                    btnConectarCliente.Enabled = true;
                }));
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + string.Empty;
                }));
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + "Problemas desconectando el servidor";
                    txtStatus.Text += "\r\n" + ex.Message;
                }));
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + string.Empty;
                }));
            }
            txtStatus.Invoke(new MethodInvoker(delegate {
                txtStatus.Text += "\r\n" + string.Empty;
            }));
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
                        //lblGUIDActivo.Invoke(new MethodInvoker(delegate {lblGUIDActivo.Text = idviaje;}));
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Registro de Tracking: " + lblGUIDActivo.Text.Trim();
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " + txtNuevaUbicacion.Text.Trim();
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();

                        //tabViajes.Parent = tabMain;
                        //tabIngresoViajes.Parent = tabTrash;
                        //tabIngresoViajes.Invoke(new MethodInvoker(delegate {
                        //    tabIngresoViajes.Parent = tabMain;
                        //}));

                        //tabIngreso.Invoke(new MethodInvoker(delegate {
                        //    tabIngreso.Parent = tabTrash;
                        //}));
                    }
                    else
                    {
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Registro de Tracking: " + txtUsuario.Text;
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " + resultado.Mensaje;
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();
                    }


                }
                else
                {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Verifique los datos, todos los campos son obligatorios.";
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }


            }
            catch (SocketException sex)
            {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Error: " + sex.Message;
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Error: " + ex.Message;
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
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
                    //lblGUIDActivo.Invoke(new MethodInvoker(delegate {lblGUIDActivo.Text = idviaje;}));
                    lblGUIDActivo.Text = idviaje;
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Finalizar Viaje: " + txtIdentificacion.Text;
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                    
                    tabIngresoViajes.Parent = tabMain;
                    tabViajes.Parent = tabTrash;
                }
                else
                {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Finalizar Viaje: " + txtUsuario.Text;
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> " + resultado.Mensaje;
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }

            }
            catch (SocketException sex)
            {
                lblMensaje.Text = sex.Message;
                clienteConectado = false;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void frmCliente_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
