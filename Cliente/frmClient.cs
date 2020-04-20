/*********************************************************************
 * Copyright 2020 Pablo Ugalde
 * Universidad Estatal A Distancia
 * PRIMER CUATRI-2020 00830 PROGRAMACION AVANZADA
 * 
*********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TransportesCRLib;

namespace Cliente
{
    public partial class frmClient : Form
    {
        public static TcpConnClient tcpConnClient = new TcpConnClient();
        public static string IPADDRESS = "127.0.0.1";
        public const int PORT = 1982;
        public frmClient()
        {
            InitializeComponent();
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            tcpConnClient.OnDataRecieved += msgReceived;

            if (!tcpConnClient.Connect(IPADDRESS, PORT))
            {
                MessageBox.Show("Error conectando con el servidor!");
                return;
            }
        }
        private void msgReceived(string data)
        {
            var package = new Package(data);
            string command = package.Command;
            if (command == "resultado")
            {
                string content = package.Content;

                Invoke(new Action(() => label1.Text = string.Format("Respuesta: {0}", content)));
            }
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Environment.Exit(0);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tcpConnClient.TcpClient.Connected)
            {
                var msgPack = new Package("login", string.Format("{0},{1}", textBox1.Text, textBox2.Text));
                tcpConnClient.SendPackage(msgPack);
            }
        }

        
    }
}
