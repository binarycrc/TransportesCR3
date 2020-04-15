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
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        TcpListener tcplistener;
        Thread processClientListener;
        bool serverStarted;
        public Server()
        {
            InitializeComponent();
            lblServerStatus.ForeColor = Color.Red;
            btnServerStop.Enabled = false;
        }

        private void btnServerStart_Click(object sender, EventArgs e)
        {
            IPAddress localIPAddress = IPAddress.Parse("127.0.0.1");
            tcplistener = new TcpListener(localIPAddress, 30000);
            processClientListener = new Thread(new ThreadStart(ClientListener) );
            processClientListener.Start();
            processClientListener.IsBackground = true;

            serverStarted = true;
            lblServerStatus.Text = "Escuchando clientes en: " + localIPAddress.ToString();
            lblServerStatus.ForeColor = Color.Green;
            btnServerStart.Enabled = false;
            btnServerStop.Enabled = true;

        }
        private void btnServerStop_Click(object sender, EventArgs e)
        {
            serverStarted = false;
            lblServerStatus.Text = "Servidor detenido";
            lblServerStatus.ForeColor = Color.Red;
            btnServerStart.Enabled = true;
            btnServerStop.Enabled = false;
        }
        private void ClientListener()
        {
            tcplistener.Start();
            while (serverStarted)
            {
                TcpClient client = tcplistener.AcceptTcpClient();
                Thread clientThread = new Thread(new ParameterizedThreadStart(clientComunication));
                clientThread.Start(client);

            }
            //throw new NotImplementedException();
        }

        private void clientComunication(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;
            NetworkStream clientStream = tcpClient.GetStream();
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = new byte[4096];
            int byteReaded;

            while (serverStarted)
            {
                byteReaded = 0;
                try
                {
                    byteReaded = clientStream.Read(buffer, 0, buffer.Length);
                }
                catch (Exception)
                {
                    break;
                }
                if (byteReaded == 0) { break; }
                string message = encoder.GetString(buffer, 0, buffer.Length);
                MessageBox.Show(string.Format("El cliente {0} se ha conectado", message),"Cliente conectado",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            tcpClient.Close();
            //throw new NotImplementedException();
        }
    }
}
