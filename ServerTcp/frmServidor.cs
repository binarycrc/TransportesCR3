using ClienteTcp;
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

namespace ServerTcp
{
    public partial class frmServidor : Form
    {
        private List<TcpClient> listaClientes;
        private TcpListener tcpListener;
        private int cuentaClientes;
        private bool continuar;
        IPAddress local = IPAddress.Parse("127.0.0.1");
        int intPort = 16830;
        DataLayer datalayer = new DataLayer();

        private BinaryWriter escritor;
        private BinaryReader lector;
        private BinaryFormatter bf;
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


                Thread thread = new Thread(EscuchaClientes);
                thread.Name = "Servidor Listener Thread";
                thread.IsBackground = true;
                thread.Start();
                btnIniciarServidor.Enabled = false;
                btnDetenerServidor.Enabled = true;
                btnCliente.Enabled = true;
                btnEnviarMensajeGrupal.Enabled = true;
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
                continuar = true;
                tcpListener = new TcpListener(local, intPort);
                tcpListener.Start();
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n"+ DateTime.Now.ToString("T") + "-> Server iniciado. escuchando en :" + local.ToString() + ":" + intPort.ToString() + ") ";
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }));



                while (continuar)
                {
                    txtStatus.Invoke(new MethodInvoker(delegate {
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Esperando por clientes...";
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();
                    }));

                    TcpClient client = tcpListener.AcceptTcpClient();   // blocks here until client connects
                    txtStatus.Invoke(new MethodInvoker(delegate {
                        txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Conexion con cliente aceptada...";
                        txtStatus.SelectionStart = txtStatus.Text.Length;
                        txtStatus.ScrollToCaret();
                    }));

                    Thread thread = new Thread(ComunicacionCliente);
                    thread.IsBackground = true;
                    thread.Start(client);
                }
            }
            catch (SocketException se)
            {
                // swallow this one exception
                // _statusTextBox.InvokeEx(stb => stb.Text += CRLF + "Problem starting the server.");
                // _statusTextBox.InvokeEx(stb => stb.Text += CRLF + se.ToString());
            }
            catch (Exception ex)
            {
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->**Problemas iniciando el servidor**";
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }));
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->" +ex.ToString();
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }));
            }
            txtStatus.Invoke(new MethodInvoker(delegate {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->**Terminando el listener y thread**";
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }));
            txtStatus.Invoke(new MethodInvoker(delegate {
                txtStatus.Text += "\r\n" + String.Empty;
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }));
        } // end ListenForIncomingConnections() method

        private void ComunicacionCliente(object obj)
        {
            TcpClient client = (TcpClient)obj;
            listaClientes.Add(client);
            cuentaClientes += 1;
            lblClientesConectados.Invoke(new MethodInvoker(delegate {
                lblClientesConectados.Text = "Clientes conectados: " + cuentaClientes.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }));

            string input = string.Empty;

            try
            {
                NetworkStream clienteStream = client.GetStream();
                escritor = new BinaryWriter(clienteStream);
                lector = new BinaryReader(clienteStream);
                bf = new BinaryFormatter();
                clienteStream.Flush();

                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                String accion;
                while (client.Connected)
                {
                    //input = reader.ReadLine(); // blocks here until something is received from client
                    accion = lector.ReadString();
                    //switch (input)
                    switch (accion)
                    {
                        //TODO: Add appropriate cases for commands
                        //OKUsuarioAcceso
                        //DenegadoUsuarioAcceso
                        case "OKUsuarioAcceso":
                            txtStatus.Invoke(new MethodInvoker(delegate {
                                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Cliente " + client.GetHashCode() + ": " + accion;
                                txtStatus.SelectionStart = txtStatus.Text.Length;
                                txtStatus.ScrollToCaret();
                            }));
                            //escritor.Write(resultadoLogin);
                            break;
                        case "DenegadoUsuarioAcceso":
                            txtStatus.Invoke(new MethodInvoker(delegate {
                                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Cliente " + client.GetHashCode() + ": " + accion;
                                txtStatus.SelectionStart = txtStatus.Text.Length;
                                txtStatus.ScrollToCaret();
                            }));
                            //escritor.Write(resultadoLogin);
                            break;
                        case "loginconductor":
                            txtStatus.Invoke(new MethodInvoker(delegate {
                                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Cliente " + client.GetHashCode() + ": " + accion;
                                txtStatus.SelectionStart = txtStatus.Text.Length;
                                txtStatus.ScrollToCaret();
                            }));
                            String userName = lector.ReadString();
                            string resultadoLogin = datalayer.UsuarioAcceso(userName);
                            escritor.Write(resultadoLogin);
                            break;

                        case "registraconductorcamion":
                            txtStatus.Invoke(new MethodInvoker(delegate {
                                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Cliente " + client.GetHashCode() + ": " + accion;
                                txtStatus.SelectionStart = txtStatus.Text.Length;
                                txtStatus.ScrollToCaret();
                            }));
                            Conductor conductor = (Conductor)(bf.Deserialize(clienteStream));
                            Camion camion = (Camion)(bf.Deserialize(clienteStream));
                            string resultadoRegistrarConductorCamion = datalayer.RegistrarConductorCamion(conductor, camion);
                            escritor.Write(resultadoRegistrarConductorCamion);
                            break;
                        case "registraviaje":
                            txtStatus.Invoke(new MethodInvoker(delegate {
                                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Cliente " + client.GetHashCode() + ": " + accion;
                                txtStatus.SelectionStart = txtStatus.Text.Length;
                                txtStatus.ScrollToCaret();
                            }));
                            Viaje viaje = (Viaje)(bf.Deserialize(clienteStream));
                            string resultadoRegistrarViaje = datalayer.RegistrarViaje(viaje);
                            escritor.Write(resultadoRegistrarViaje);
                            break;
                        case "registraviajeactualizacion":
                            txtStatus.Invoke(new MethodInvoker(delegate {
                                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Cliente " + client.GetHashCode() + ": " + accion;
                                txtStatus.SelectionStart = txtStatus.Text.Length;
                                txtStatus.ScrollToCaret();
                            }));
                            Tracking tracking = (Tracking)(bf.Deserialize(clienteStream));
                            string resultadoRegistrarViajeActualizacion = datalayer.RegistrarViajeActualizacion(tracking);
                            escritor.Write(resultadoRegistrarViajeActualizacion);
                            break;
                        case "registraviajefinalizado":
                            txtStatus.Invoke(new MethodInvoker(delegate {
                                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Cliente " + client.GetHashCode() + ": " + accion;
                                txtStatus.SelectionStart = txtStatus.Text.Length;
                                txtStatus.ScrollToCaret();
                            }));
                            string Id_viaje = (string)(bf.Deserialize(clienteStream));
                            string resultadoRegistrarViajeFinalizado = datalayer.RegistrarViajeFinalizado(Id_viaje);
                            escritor.Write(resultadoRegistrarViajeFinalizado);
                            break;

                        //

                        default:  // default case acts as echo server
                            {
                                txtStatus.Invoke(new MethodInvoker(delegate {
                                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Cliente " + client.GetHashCode() + ": " + accion;
                                    txtStatus.SelectionStart = txtStatus.Text.Length;
                                    txtStatus.ScrollToCaret();
                                }));
                                escritor.Write("Respuesta servidor: " + "->Cliente " + client.GetHashCode());
                                escritor.Flush();
                                //writer.WriteLine("Respuesta servidor: " + "->Cliente " + client.GetHashCode());
                                //writer.Flush();

                                break;
                            }
                    }
                }

            }
            catch (SocketException se)
            {
                // Swallow this exception
                // _statusTextBox.InvokeEx(stb => stb.Text += CRLF + "Problem processing client requests.");
                // _statusTextBox.InvokeEx(stb => stb.Text =se.ToString());

            }
            catch (Exception ex)
            {
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Problemas con la solicitud del cliente " + input;
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }));
            }

            listaClientes.Remove(client);
            cuentaClientes -= 1;
            lblClientesConectados.Invoke(new MethodInvoker(delegate {
                lblClientesConectados.Text = "Clientes conectados: " + cuentaClientes.ToString();
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }));
            txtStatus.Invoke(new MethodInvoker(delegate {
                txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Finalizado la solicitud del cliente " + client.GetHashCode() ;
                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();
            }));

            if (cuentaClientes == 0)
            {
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->No hay clientes conectados";
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }));
            }
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
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "-> Enviando mensaje a clientes: "+ txtEnviarMensaje.Text;
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }));
                foreach (TcpClient client in listaClientes)
                {
                    NetworkStream clienteStream = client.GetStream();
                    escritor = new BinaryWriter(clienteStream);
                    clienteStream.Flush();
                    escritor.Write(txtEnviarMensaje.Text);
                    escritor.Flush();
                    //StreamWriter writer = new StreamWriter(client.GetStream());
                    //writer.WriteLine(txtEnviarMensaje.Text);
                    //writer.Flush();
                    //_clientCommandTextBox.Text = string.Empty; 
                }

            }
            catch (Exception ex)
            {
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->Problemas enviando mensaje a los clientes";
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }));
                txtStatus.Invoke(new MethodInvoker(delegate {
                    txtStatus.Text += "\r\n" + DateTime.Now.ToString("T") + "->"+ex.ToString();
                    txtStatus.SelectionStart = txtStatus.Text.Length;
                    txtStatus.ScrollToCaret();
                }));
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
    }
}
