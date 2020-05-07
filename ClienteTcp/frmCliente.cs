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
        bool clienteConectado;
        public IPAddress ipServidor = IPAddress.Parse("127.0.0.1");
        public int intPort = 16830;
        public IPEndPoint serverEndPoint;
        public StreamWriter clienteStreamWriter;
        public StreamReader clienteStreamReader;

        private TcpClient cliente;
        private BinaryWriter escritor;
        private BinaryReader lector;
        private BinaryFormatter bf;

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

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cliente.Connected)
                {
                    //NetworkStream clienteStream = cliente.GetStream();
                    //escritor = new BinaryWriter(clienteStream);
                    ////lector = new BinaryReader(clienteStream);
                    //clienteStream.Flush();
                    //escritor.Write("loginconductor");
                    //escritor.Write(txtUsuario.Text.Trim());

                    Conductor conductor = new Conductor("", "", "", "", "", txtUsuario.Text, "");
                    MensajeSocket<Conductor> mensajeConectar = new MensajeSocket<Conductor> { Mensaje = "loginconductor", Valor = conductor };

                    clienteStreamReader = new StreamReader(cliente.GetStream());
                    clienteStreamWriter = new StreamWriter(cliente.GetStream());
                    clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeConectar));
                    clienteStreamWriter.Flush();
                    string mensaje = clienteStreamReader.ReadLine();
                    MensajeSocket<bool> resultado;
                    resultado = JsonConvert.DeserializeObject<MensajeSocket<bool>>(mensaje);

                    if (resultado.Valor)
                    {
                        txtStatus.Invoke(new MethodInvoker(delegate {
                            txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Ingreso de Conductor";
                            txtStatus.SelectionStart = txtStatus.Text.Length;
                            txtStatus.ScrollToCaret();
                        }));
                        tabIngresoViajes.Invoke(new MethodInvoker(delegate {
                            tabIngresoViajes.Parent = tabMain;
                        }));

                        tabIngreso.Invoke(new MethodInvoker(delegate {
                            tabIngreso.Parent = tabTrash;
                        }));
                    }
                    else
                    {
                        txtStatus.Invoke(new MethodInvoker(delegate {
                            txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Ingreso de Conductor";
                            txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->El conductor no existe o tiene el acceso denegado!";
                            txtStatus.SelectionStart = txtStatus.Text.Length;
                            txtStatus.ScrollToCaret();
                        }));
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {

                cliente.Close();
                this.Close();
            }
            catch (Exception)
            {

                throw;
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
                    NetworkStream clienteStream = cliente.GetStream();
                    escritor = new BinaryWriter(clienteStream);
                    lector = new BinaryReader(clienteStream);
                    clienteStream.Flush();
                    bf = new BinaryFormatter();

                    //existe el usuario
                    Conductor condutor = new Conductor(txtIdentificacion.Text.Trim(), txtNombre.Text.Trim(), txtPApellido.Text.Trim(),
                        txtSApellido.Text.Trim(), "", txtUserName.Text.Trim(), "PENDIENTE");
                    Camion camion = new Camion(txtPlaca.Text.Trim(), numModelo.Value.ToString(), cbMarca.Text.Trim(), "0", "0");
                    escritor.Write("registraconductorcamion");
                    bf.Serialize(clienteStream, condutor);
                    bf.Serialize(clienteStream, camion);

                    txtStatus.Invoke(new MethodInvoker(delegate {
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> registraconductorcamion";
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();
                    }));
                }
                else
                { lblMensaje.Text = "Verifique los datos, todos los campos son obligatorios."; }

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
                    NetworkStream clienteStream = cliente.GetStream();
                    escritor = new BinaryWriter(clienteStream);
                    lector = new BinaryReader(clienteStream);
                    clienteStream.Flush();
                    string idviaje = Guid.NewGuid().ToString();
                    bf = new BinaryFormatter();

                    Viaje viaje = new Viaje(idviaje
                        , txtUsuario.Text.Trim()
                        ,txtLugarInicio.Text.Trim()
                        ,txtLugarFinal.Text.Trim()
                        ,txtDescripcionCarga.Text.Trim()
                        ,txtTiempoEstimado.Text.Trim()
                        ,"ACTIVO");
                    escritor.Write("registraviaje");
                    bf.Serialize(clienteStream, viaje);
                    lblGUIDActivo.Invoke(new MethodInvoker(delegate {
                        lblGUIDActivo.Text = idviaje;
                    }));
                    txtStatus.Invoke(new MethodInvoker(delegate {
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> registraviaje";
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();
                    }));

                   
                }
                else
                { lblMensaje.Text = "Verifique los datos, todos los campos son obligatorios."; }

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

        private void btnConectarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                cliente = new TcpClient();
                serverEndPoint = new IPEndPoint(ipServidor, intPort);
                cliente.Connect(serverEndPoint);
                Conductor conductor = new Conductor("", "", "", "", "", txtUsuario.Text, "");
                MensajeSocket<Conductor> mensajeConectar = new MensajeSocket<Conductor> { Mensaje = "Conectar", Valor = conductor };

                clienteStreamReader = new StreamReader(cliente.GetStream());
                clienteStreamWriter = new StreamWriter(cliente.GetStream());
                clienteStreamWriter.WriteLine(JsonConvert.SerializeObject(mensajeConectar));
                clienteStreamWriter.Flush();

                //TcpClient clienteListener = new TcpClient(ipServidor.ToString(), intPort);
                //Thread thread = new Thread(EscuchaClientes);
                //thread.IsBackground = true;
                //thread.Start(clienteListener);

                btnConectarCliente.Enabled = false;
                btnIngresar.Enabled = true;
                linkRegistrarse.Enabled = true;
                
                
            }
            catch (Exception ex)
            {
                txtStatus.Text += "\r\n" + "Problemas conectando el servidor.";
                txtStatus.Text += "\r\n" + ex.ToString();
            }
        }

        private void EscuchaClientes(object cliente)
        {
            TcpClient client = (TcpClient)cliente;
            string input = string.Empty;

            NetworkStream clienteStream = client.GetStream();
            escritor = new BinaryWriter(clienteStream);
            lector = new BinaryReader(clienteStream);
            BinaryFormatter bf = new BinaryFormatter();
            //clienteStream.Flush();

            String accion = string.Empty;

            try
            {
                escritor.Write("Nuevo Cliente");
                escritor.Flush();

                while (client.Connected)
                {
                    //input = reader.ReadLine(); // block here until we receive something from the server.
                    accion = lector.ReadString();
                    //if (input == null)
                    if (accion == null)
                    {
                        Desconectardesdeservidor();
                    }
                    else
                    {
                        switch (accion)
                        {
                            case "OKUsuarioAcceso":
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                tabIngresoViajes.Invoke(new MethodInvoker(delegate {
                                    tabIngresoViajes.Parent = tabMain;
                                }));

                                tabIngreso.Invoke(new MethodInvoker(delegate {
                                    tabIngreso.Parent = tabTrash;
                                }));
                                break;
                            case "DenegadoUsuarioAcceso":
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                MessageBox.Show("El conductor no existe o tiene el acceso denegado!");
                                break;
                            case "OKConductor":
                                tabIngreso.Invoke(new MethodInvoker(delegate {
                                    tabIngreso.Parent = tabMain;
                                }));
                                tabRegistroConductor.Invoke(new MethodInvoker(delegate {
                                    tabRegistroConductor.Parent = tabTrash;
                                }));
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                MessageBox.Show("Conductor y camion creados!");
                                break;

                            case "ExistenteConductor":
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                MessageBox.Show("El conductor con la identificacion " + txtIdentificacion.Text + ", ya existe!");
                                break;

                            case "ExistenteUsuario":
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                MessageBox.Show("El conductor con el Usuario " + txtUsuario.Text + ", ya existe!");
                                break;
                            case "ExistenteCamion":
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                MessageBox.Show("El camion con la placa " + txtPlaca.Text + ", ya existe!");
                                break;
                            case "OKViaje":
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                tabIngresoViajes.Invoke(new MethodInvoker(delegate {
                                    tabIngresoViajes.Parent = tabTrash;
                                }));

                                tabViajes.Invoke(new MethodInvoker(delegate {
                                    tabViajes.Parent = tabMain;
                                }));
                                break;
                            case "TieneViajeActivo":
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                MessageBox.Show("El conductor: " + txtUsuario.Text + ", ya tiene un viaje acitvo.");
                                break;
                            case "OKViajeActualizacion":
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                break;
                            case "OKViajeFinalizado":
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                break;
                            case "ConductorActivo":
                                Conductor conductorActivo = (Conductor)(bf.Deserialize(clienteStream));
                                Viaje viajeActivo = (Viaje)(bf.Deserialize(clienteStream));
                                txtIdentificacion.Invoke(new MethodInvoker(delegate {
                                    txtIdentificacion.Text = conductorActivo.Identificacion;
                                }));
                                txtNombre.Invoke(new MethodInvoker(delegate {
                                    txtNombre.Text = conductorActivo.Nombre;
                                }));
                                txtPApellido.Invoke(new MethodInvoker(delegate {
                                    txtPApellido.Text = conductorActivo.PApellido;
                                }));
                                txtSApellido.Invoke(new MethodInvoker(delegate {
                                    txtSApellido.Text = conductorActivo.SApellido;
                                }));
                                txtUserName.Invoke(new MethodInvoker(delegate {
                                    txtUserName.Text = conductorActivo.UserName;
                                }));

                                lblGUIDActivo.Invoke(new MethodInvoker(delegate {
                                    lblGUIDActivo.Text = viajeActivo.Id_viaje;
                                }));

                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                tabIngreso.Invoke(new MethodInvoker(delegate {
                                    tabIngreso.Parent = tabTrash;
                                }));
                                tabViajes.Invoke(new MethodInvoker(delegate {
                                    tabViajes.Parent = tabMain;
                                }));
                                break;
                            default:
                                {
                                    txtStatus.Invoke(new MethodInvoker(delegate {
                                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->"+ accion;
                                        txtStatus.SelectionStart = txtStatus.Text.Length;
                                        txtStatus.ScrollToCaret();
                                    }));
                                    break;
                                }
                        } // end switch
                    } // end if/else


                }
            }
            catch (Exception ex)
            {
                if (client.Connected) 
                {
                    txtStatus.Invoke(new MethodInvoker(delegate {
                        txtStatus.Text += "\r\n" + "Problemas de comunicacion con el servidor";
                        txtStatus.Text += "\r\n" + ex.Message;
                    }));
                }
            }
            if (client.Connected)
            {
                btnConectarCliente.Invoke(new MethodInvoker(delegate
                {
                    btnConectarCliente.Enabled = true;
                }));
                txtStatus.Invoke(new MethodInvoker(delegate
                {
                    txtStatus.Text += "\r\n" + string.Empty;
                }));
            }
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
                    NetworkStream clienteStream = cliente.GetStream();
                    escritor = new BinaryWriter(clienteStream);
                    lector = new BinaryReader(clienteStream);
                    clienteStream.Flush();
                    bf = new BinaryFormatter();
                    Tracking tracking = new Tracking(lblGUIDActivo.Text
                        , txtNuevaUbicacion.Text.Trim()
                        , txtObservaciones.Text.Trim());

                    escritor.Write("registraviajeactualizacion");
                    bf.Serialize(clienteStream, tracking);

                    txtStatus.Invoke(new MethodInvoker(delegate {
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> registraviajeactualizacion";
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();
                    }));


                }
                else
                { lblMensaje.Text = "Verifique los datos, todos los campos son obligatorios."; }

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

        private void btnFinalizarViaje_Click(object sender, EventArgs e)
        {
            try
            {
                NetworkStream clienteStream = cliente.GetStream();
                escritor = new BinaryWriter(clienteStream);
                lector = new BinaryReader(clienteStream);
                clienteStream.Flush();
                bf = new BinaryFormatter();
                
                escritor.Write("registraviajefinalizado");
                bf.Serialize(clienteStream, lblGUIDActivo.Text.Trim());

                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> registraviajefinalizado";
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }));
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

        
    }
}
