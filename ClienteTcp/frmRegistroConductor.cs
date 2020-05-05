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
using System.Windows.Forms;
using TransportesCRLib;

namespace ClienteTcp
{
    public partial class frmRegistroConductor : Form
    {
        IPAddress ipServidor = IPAddress.Parse("127.0.0.1");
        int intPort = 30000;

        TcpClient cliente;
        BinaryWriter escritor;
        BinaryReader lector;
        public frmRegistroConductor()
        {
            InitializeComponent();
            try
            {
                cliente = new TcpClient();
                IPEndPoint serverEndPoint = new IPEndPoint(ipServidor, intPort);

                cliente.Connect(serverEndPoint);
            }
            catch (SocketException sex)
            {
                lblMensaje.Text = sex.Message;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                throw;
            }
            finally { lblMensaje.Text = "Cliente Conectado!"; }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnRegistroConductor_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdentificacion.Text.Trim()) || string.IsNullOrWhiteSpace(txtIdentificacion.Text.Trim()) ||
                    string.IsNullOrEmpty(txtNombre.Text.Trim()) || string.IsNullOrWhiteSpace(txtNombre.Text.Trim()) ||
                    string.IsNullOrEmpty(txtPApellido.Text.Trim()) || string.IsNullOrWhiteSpace(txtPApellido.Text.Trim()) ||
                    string.IsNullOrEmpty(txtSApellido.Text.Trim()) || string.IsNullOrWhiteSpace(txtSApellido.Text.Trim()) ||
                    string.IsNullOrEmpty(txtUsuario.Text.Trim()) || string.IsNullOrWhiteSpace(txtUsuario.Text.Trim()) ||
                    string.IsNullOrEmpty(txtPlaca.Text.Trim()) || string.IsNullOrWhiteSpace(txtPlaca.Text.Trim()) ||
                    string.IsNullOrEmpty(cbMarca.Text.Trim()) || string.IsNullOrWhiteSpace(cbMarca.Text.Trim()) 
                    )
                {
                    NetworkStream clienteStream = cliente.GetStream();
                    escritor = new BinaryWriter(clienteStream);
                    lector = new BinaryReader(clienteStream);
                    clienteStream.Flush();
                    BinaryFormatter binaryFormatter = new BinaryFormatter();

                    //existe el usuario
                    Conductor condutor = new Conductor(txtIdentificacion.Text.Trim(), txtNombre.Text.Trim(), txtPApellido.Text.Trim(),
                        txtSApellido.Text.Trim(),"", txtUsuario.Text.Trim(),"PENDIENTE");
                    Camion camion = new Camion(txtPlaca.Text.Trim(), numModelo.Value.ToString(), cbMarca.Text.Trim(), "0", "0");
                    escritor.Write("registraconductorcamion");
                    binaryFormatter.Serialize(clienteStream, condutor);
                    binaryFormatter.Serialize(clienteStream, camion);
                    String respuesta = lector.ReadString();
                    switch (respuesta)
                    {
                        case "OK":
                            MessageBox.Show("Conductor y camion creados!");
                            break;

                        case "ExistenteConductor":
                            MessageBox.Show("El conductor con la identificacion " + txtIdentificacion.Text + ", ya existe!");
                            break;

                        case "ExistenteUsuario":
                            MessageBox.Show("El conductor con el Usuario " + txtUsuario.Text + ", ya existe!");
                            break;
                        case "ExistenteCamion":
                            MessageBox.Show("El camion con la placa " + txtPlaca.Text + ", ya existe!");
                            break;

                        default:
                            MessageBox.Show(respuesta);
                            break;
                    }
                    //existe el camion
                }
                else
                { lblMensaje.Text = "Verifique los datos, todos los campos son obligatorios."; }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
