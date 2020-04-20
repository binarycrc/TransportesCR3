/*********************************************************************
 * Copyright 2020 Pablo Ugalde
 * Universidad Estatal A Distancia
 * PRIMER CUATRI-2020 00830 PROGRAMACION AVANZADA
 * 
*********************************************************************/

using Cliente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TransportesCRLib;

namespace Server
{
    public partial class frmServer : Form
    {
        public delegate void ClientCarrier(TcpConnServer tcpConnServer);
        public event ClientCarrier OnClientConnected;
        public event ClientCarrier OnClientDisconnected;
        public delegate void DataRecieved(TcpConnServer tcpConnServer, string data);
        public event DataRecieved OnDataRecieved;

        private TcpListener _tcpListener;
        private Thread _acceptThread;
        private List<TcpConnServer> connectedClients = new List<TcpConnServer>();

        bool serverStarted;
        public frmServer()
        {
            InitializeComponent();
        }
        private void frmServer_Load(object sender, EventArgs e)
        {
            //OnDataRecieved += msgReceived;
            //OnClientConnected += connReceived;
            //OnClientDisconnected += connClosed;

            //listenClients("127.0.0.1", 1982);
        }
        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        public void serverConnect() 
        {
            try
            {
                OnDataRecieved += msgReceived;
                OnClientConnected += connReceived;
                OnClientDisconnected += connClosed;
                listenClients("127.0.0.1", 1982);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            
        }
        public void serverDisconnect()
        {
            try
            {
                //_acceptThread = null;
                //_tcpListener.EndAcceptTcpClient();
                _tcpListener.Stop();
                //OnDataRecieved += null;
                //OnClientConnected += null;
                //OnClientDisconnected += null;
                
            }
            catch (SocketException socketEx)
            {
                MessageBox.Show(socketEx.Message.ToString());
                //if (_acceptThread)
                //    ar.SetAsCompleted(null, false); //exception because listener stopped (disposed), ignore exception
                //else
                //    ar.SetAsCompleted(socketEx, false);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        private void msgReceived(TcpConnServer tcpConnServer, string data)
        {
            var package = new Package(data);
            string command = package.Command;
            if (command == "login")
            {
                string content = package.Content;
                List<string> values = Map.Deserialize(content);

                Invoke(new Action(() => textBox1.Text = values[0]));
                Invoke(new Action(() => textBox2.Text = values[1]));

                var msgPack = new Package("resultado", "OK");
                tcpConnServer.SendPackage(msgPack);
            }
            if (command == "insertar")
            {
                string content = package.Content;
                List<string> values = Map.Deserialize(content);
                //usuariosTableAdapter.Insert(values[0], values[1]);
                var msgPack = new Package("resultado", "Registros en SQL: OK");
                tcpConnServer.SendPackage(msgPack);
            }
        }
        private void connReceived(TcpConnServer tcpConnServer)
        {
            lock (connectedClients)
                if (!connectedClients.Contains(tcpConnServer))
                    connectedClients.Add(tcpConnServer);
            Invoke(new Action(() => label1.Text = string.Format("Clientes: {0}", connectedClients.Count)));
        }
        private void connClosed(TcpConnServer tcpConnServer)
        {
            lock (connectedClients)
                if (connectedClients.Contains(tcpConnServer))
                {
                    int cliIndex = connectedClients.IndexOf(tcpConnServer);
                    connectedClients.RemoveAt(cliIndex);
                }
            Invoke(new Action(() => label1.Text = string.Format("Clientes: {0}", connectedClients.Count)));
        }
        private void listenClients(string ipAddress, int port)
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
                _tcpListener.Start();
                _acceptThread = new Thread(acceptClients);
                _acceptThread.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        private void acceptClients()
        {
            do
            {
                try
                {
                    var tt = _tcpListener.Pending();

                    //while (!_tcpListener.Pending())
                    //{
                    //    Thread.Sleep(1000);
                    //}

                    var conn = _tcpListener.AcceptTcpClient();
                    var srvClient = new TcpConnServer(conn){ReadThread = new Thread(readData)};
                    srvClient.ReadThread.Start(srvClient);

                    if (OnClientConnected != null) { OnClientConnected(srvClient); }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }

            } while (true);
        }
        private void readData(object client)
        {
            var cli = client as TcpConnServer;
            var charBuffer = new List<int>();

            do
            {
                try
                {
                    if (cli == null) { break; }
                    if (cli.StreamReader.EndOfStream) { break; }

                    int charCode = cli.StreamReader.Read();
                    if (charCode == -1) { break; }

                    if (charCode != 0)
                    {
                        charBuffer.Add(charCode);
                        continue;
                    }
                    if (OnDataRecieved != null)
                    {
                        var chars = new char[charBuffer.Count];
                        //Convert all the character codes to their representable characters
                        for (int i = 0; i < charBuffer.Count; i++)
                        {
                            chars[i] = Convert.ToChar(charBuffer[i]);
                        }
                        //Convert the character array to a string
                        var message = new string(chars);

                        //Invoke our event
                        OnDataRecieved(cli, message);
                    }
                    charBuffer.Clear();
                }
                catch (IOException)
                {
                    break;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    break;
                }
            } while (true);

            if (OnClientDisconnected != null) { OnClientDisconnected(cli); }
        }

        private void menuServerStart_Click(object sender, EventArgs e)
        {
            if (serverStarted) { lblStripStatus.Text = "Servidor conectado!"; }
            else
            {
                serverConnect();
                lblStripStatus.Text = "Servidor conectado";
                serverStarted = true;
                menuServerStart.Enabled = false;
                menuServerStop.Enabled = true;
            }
        }

        private void menuServerStop_Click(object sender, EventArgs e)
        {
            if (!serverStarted) { lblStripStatus.Text = "Servidor Desconectado!"; }
            else
            {
                serverDisconnect();
                lblStripStatus.Text = "Servidor Desconectado";
                serverStarted = false;
                menuServerStart.Enabled = true;
                menuServerStop.Enabled = false;
            }
        }

        private void menuNewClient_Click(object sender, EventArgs e)
        {
            if (!serverStarted) { MessageBox.Show("Primero debe iniciar el servidor."); }
            else
            {
                frmClient client = new frmClient();
                client.Show();
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            Application.Exit();
        }

       
    }
}
