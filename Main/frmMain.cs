using Cliente;
using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class frmMain : Form
    {
        bool serverStarted;
        public frmMain()
        {
            InitializeComponent();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void menuServerStart_Click_1(object sender, EventArgs e)
        {
            if (serverStarted) { lblStripStatus.Text = "Servidor conectado!"; }
            else
            {
                frmServer server = new frmServer();
                server.serverConnect();
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
                frmServer server = new frmServer();
                server.serverDisconnect();
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
    }
}
