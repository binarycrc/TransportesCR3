using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ServerTcp
{
    public partial class frmServer : Form
    {
        TcpListener tcpListener;
        Thread subprocesoEscuchaClientes;
        bool servidorIniciado;
        ModificarListBoxDelegate modificarListBoxClientes;
        string lastMessage;
        int clientesConectados;

        public frmServer()
        {
            InitializeComponent();
            lblEstado.ForeColor = Color.Red;
            btnDetener.Enabled = false;
            modificarListBoxClientes = new ModificarListBoxDelegate(ModificarListBox);
        }

        private void ModificarListBox(string texto)
        {
            listClientesConectados.Items.Add(texto);
        }

        private delegate void ModificarListBoxDelegate(string texto);

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            IPAddress local = IPAddress.Parse("127.0.0.1");
            tcpListener = new TcpListener(local, 30000);
            subprocesoEscuchaClientes = new Thread(new ThreadStart(EscuchaClientes));
            subprocesoEscuchaClientes.Start();
            subprocesoEscuchaClientes.IsBackground = true;

            servidorIniciado = true;
            lblEstado.Text = "Escuchando clientes en  ... (127.0.0.1,30000)";
            lblEstado.ForeColor = Color.Green;
            btnIniciar.Enabled = false;
            btnDetener.Enabled = true;

        }

        private void EscuchaClientes()
        {
            try
            {
                tcpListener.Start();
                while (servidorIniciado)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    lock (this) {
                        clientesConectados++;
                    }
                    Thread clientThread = new Thread(new ParameterizedThreadStart(ComunicacionCliente));
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComunicacionCliente(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;
            NetworkStream clienteStream = tcpClient.GetStream();
            ASCIIEncoding encoder = new ASCIIEncoding();

            byte[] buffer = encoder.GetBytes("Connected");
            clienteStream.Write(buffer, 0, buffer.Length);
            clienteStream.Flush();

            byte[] message = new byte[4096];
            int byteleidos;

            while (servidorIniciado)
            {
                byteleidos = 0;
                try
                {
                    byteleidos = clienteStream.Read(message, 0, message.Length);
                }
                catch (Exception)
                {
                    break;
                }
                if (byteleidos == 0) 
                { 
                    lock (this)
                    {
                        clientesConectados--;
                        //Conectados.Text = clientesConectados.ToString();
                    }
                    break; 
                }
                encoder = new ASCIIEncoding();
                string mensaje = encoder.GetString(message, 0, byteleidos);
                System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, byteleidos));
                lastMessage = encoder.GetString(message, 0, byteleidos);
                //MessageBox.Show(string.Format("el cliente {0} se ha conectado", mensaje, "Cliente conectado", MessageBoxButtons.OK, MessageBoxIcon.Information));
                listClientesConectados.Invoke(modificarListBoxClientes, new object[] { lastMessage });
            }
            tcpClient.Close();
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            servidorIniciado = false;
            tcpListener.Stop();
            lblEstado.Text = "Escuchando clientes en  ... (127.0.0.1,30000)";
            lblEstado.ForeColor = Color.Red;
            btnIniciar.Enabled = true;
            btnDetener.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.MdiParent = this;
            form1.Show();
        }
    }
}
