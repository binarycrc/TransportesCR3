using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ClienteTcp
{
    public partial class frmCliente : Form
    {
        bool clienteConectado;
        TcpClient cliente;
        public frmCliente()
        {
            InitializeComponent();
            lblEstado.ForeColor = Color.Red;
            btnDesconectar.Enabled = false;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdentificador.Text))
            {
                try
                {
                    IPAddress ipServidor = IPAddress.Parse("127.0.0.1");
                    cliente = new TcpClient();
                    IPEndPoint serverEndPoint = new IPEndPoint(ipServidor, 30000);

                    cliente.Connect(serverEndPoint);
                    NetworkStream clienteStream = cliente.GetStream();
                    ASCIIEncoding encoder = new ASCIIEncoding();

                    //aqui envio los datos segun mi estructura y separador
                    byte[] buffer = encoder.GetBytes(txtIdentificador.Text);

                    clienteStream.Write(buffer, 0, buffer.Length);
                    clienteStream.Flush();

                    clienteStream = cliente.GetStream();
                    byte[] message = new byte[4096];
                    int byteLeidos;
                    byteLeidos = clienteStream.Read(message, 0, 4096);
                    encoder = new ASCIIEncoding();
                    System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, 4096));
                    string lastMessage = encoder.GetString(message, 0, byteLeidos);
                    //Mensaje recibido desde el servidor en el cliente
                    MessageBox.Show(lastMessage);

                    lblEstado.Text = "Conectado al server";
                    lblEstado.ForeColor = Color.Green;
                    clienteConectado = true;
                    btnConectar.Enabled = false;
                    btnDesconectar.Enabled = true;
                    txtIdentificador.ReadOnly = true;
                }
                catch (SocketException se) 
                {
                    MessageBox.Show(se.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar el identificador del cliente");
            }

        }
    }
}
