using ClienteTcp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TransportesCRLib;

namespace ServerTcp
{
    public partial class OLDfrmServer : Form
    {
        private List<TcpClient> _client_list;
        private TcpListener _tcpListener;
        private Thread _acceptThread;
        private int _client_count;
        private bool _keep_going;

        Thread subprocesoEscuchaClientes;
        bool servidorIniciado;
        ModificarListBoxDelegate modificarListBoxClientes;
        ModificarRichTextBoxDelegate modificarrtbLogs;
        string lastMessage;
        IPAddress local = IPAddress.Parse("127.0.0.1");
        int intPort = 16830;
        DataLayer datalayer = new DataLayer();


        private delegate void ModificarListBoxDelegate(string texto);
        private delegate void ModificarRichTextBoxDelegate(string strColor, string texto);

        public OLDfrmServer()
        {
            InitializeComponent();
            //lblEstado.ForeColor = Color.Red;
            //menuDetenerServidor.Enabled = false;
            modificarListBoxClientes = new ModificarListBoxDelegate(ModificarListBox);
            modificarrtbLogs = new ModificarRichTextBoxDelegate(ModificarrtbLogs);

            _client_list = new List<TcpClient>();
            _client_count = 0;
            lblConectados.Text = "0";
            menuIniciarServidor.Enabled = true;
            menuDetenerServidor.Enabled = false;

        }


        private void ModificarrtbLogs(string strColor, string texto)
        {
            rtbLogs.SelectionColor = ColorTranslator.FromHtml(strColor);
            rtbLogs.SelectedText = texto;
            rtbLogs.SelectionStart = rtbLogs.Text.Length;
            // scroll it automatically
            rtbLogs.ScrollToCaret();
        }

        private void ModificarListBox(string texto)
        {
            listClientesConectados.Items.Add(texto);
        }
        

        private void ComunicacionCliente(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;
            NetworkStream clienteStream = tcpClient.GetStream();
            BinaryWriter escritor = new BinaryWriter(clienteStream);
            BinaryReader lector = new BinaryReader(clienteStream);
            BinaryFormatter bf = new BinaryFormatter();
            clienteStream.Flush();

            String accion;
            while (servidorIniciado)
            {

                try
                {
                    accion = lector.ReadString();
                    switch (accion)
                    {
                        case "loginconductor":
                            String Dsc_user_name = lector.ReadString();
                            String Dsc_password = lector.ReadString();
                            //string resultadoLogin = datalayer.LoginAlumno(Dsc_user_name, Dsc_password);
                            //escritor.Write(resultadoLogin);
                            break;

                        case "registraconductorcamion":
                            Conductor conductor = (Conductor)(bf.Deserialize(clienteStream));
                            Camion camion = (Camion)(bf.Deserialize(clienteStream));
                            string resultadoRegistrarConductorCamion = datalayer.RegistrarConductorCamion(conductor, camion);
                            escritor.Write(resultadoRegistrarConductorCamion);
                            break;

                        default:
                            Console.WriteLine("Nothing");
                            break;
                    }
                }
                catch (IOException iex) { MessageBox.Show(iex.Message); }
                catch (ThreadAbortException tex) { MessageBox.Show(tex.Message); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                listClientesConectados.Invoke(modificarListBoxClientes, new object[] { lastMessage });
            }
            tcpClient.Close();
        }
        private void menuIniciarServidor_Click(object sender, EventArgs e)
        {
            try
            {
                if (_tcpListener == null)
                {
                    _tcpListener = new TcpListener(local, intPort);
                    Thread.Sleep(500);
                }

                subprocesoEscuchaClientes = new Thread(new ThreadStart(EscuchaClientes));
                subprocesoEscuchaClientes.IsBackground = true;
                subprocesoEscuchaClientes.Start();

                servidorIniciado = true;

                rtbLogs.Invoke(modificarrtbLogs, new object[] { "Blue", Environment.NewLine + string.Format("[{0}] *{1}* : Escuchando en ({1},{2}).", DateTime.Now.ToString("T"), "Servidor", local, intPort) });

                menuIniciarServidor.Enabled = false;
                menuDetenerServidor.Enabled = true;
                menuNuevoCliente.Enabled = true;
            }
            catch (IOException iex) { MessageBox.Show(iex.Message); }
            catch (ThreadAbortException tex) { MessageBox.Show(tex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            //EscucharClientes();
        }
        private void EscuchaClientes()
        {
            try
            {
                _tcpListener.Start();
                while (servidorIniciado)
                {
                    TcpClient client = _tcpListener.AcceptTcpClient();
                    Thread clientThread = new Thread(new ParameterizedThreadStart(ComunicacionCliente));
                    clientThread.Start(client);
                }
            }
            catch (IOException iex) { MessageBox.Show(iex.Message); }
            catch (ThreadAbortException tex) { MessageBox.Show(tex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void menuDetenerServidor_Click(object sender, EventArgs e)
        {
            try
            {
                rtbLogs.Invoke(modificarrtbLogs, new object[] { "Red", Environment.NewLine + string.Format("[{0}] *{1}* : Servidor detenido.", DateTime.Now.ToString("T"), "Servidor") });
                servidorIniciado = false;
                //tcpListener.Stop();
                Thread.Sleep(500);
                subprocesoEscuchaClientes.IsBackground = false;
                //subprocesoEscuchaClientes.Abort();

                menuIniciarServidor.Enabled = true;
                menuDetenerServidor.Enabled = false;
                menuNuevoCliente.Enabled = false;
            }
            catch (IOException iex) { MessageBox.Show(iex.Message); }
            catch (ThreadAbortException tex) { MessageBox.Show(tex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void menuNuevoCliente_Click(object sender, EventArgs e)
        {
            try
            {
                frmCliente cliente = new frmCliente();
                cliente.Show();
            }
            catch (IOException iex) { MessageBox.Show(iex.Message); }
            catch (ThreadAbortException tex) { MessageBox.Show(tex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void menuSalirServidor_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
                Application.Exit();
            }
            catch (Exception){throw;}
        }

        private void menuEnviarMensajeServidor_Click(object sender, EventArgs e)
        {
            try
            {
                frmConductores form1 = new frmConductores();
                form1.MdiParent = this;
                form1.Show();
            }
            catch (IOException iex) { MessageBox.Show(iex.Message); }
            catch (ThreadAbortException tex) { MessageBox.Show(tex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void menuConductores_Click(object sender, EventArgs e)
        {

        }
        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }


    }
}
