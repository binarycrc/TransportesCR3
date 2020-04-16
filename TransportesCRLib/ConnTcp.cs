using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TransportesCRLib
{
    public class ConnTcp
    {
        public TcpClient TcpClient;
        public StreamReader StreamReader;
        public StreamWriter StreamWriter;
        public Thread ReadThread;

        public delegate void DataCarrier(string data);
        public event DataCarrier OnDataRecieved;

        public delegate void DisconnectNotify();
        public event DisconnectNotify OnDisconnect;

        public delegate void ErrorCarrier(Exception e);
        public event ErrorCarrier OnError;

        public ConnTcp(TcpClient client)
        {
            var ns = client.GetStream();
            StreamReader = new StreamReader(ns);
            StreamWriter = new StreamWriter(ns);
            TcpClient = client;
        }
        private void writeMsj(string mensaje)
        {
            try
            {
                StreamWriter.Write(mensaje + "\0");
                StreamWriter.Flush();
            }
            catch (Exception e)
            {
                if (OnError != null)
                    OnError(e);
            }
        }
        public void SendPackage(Package package)
        {
            writeMsj(package);
        }
    }
}
