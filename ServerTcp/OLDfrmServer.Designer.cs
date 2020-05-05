namespace ServerTcp
{
    partial class OLDfrmServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.servidorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIniciarServidor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDetenerServidor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSalirServidor = new System.Windows.Forms.ToolStripMenuItem();
            this.mensajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEnviarMensajeServidor = new System.Windows.Forms.ToolStripMenuItem();
            this.clienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNuevoCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConductores = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listClientesConectados = new System.Windows.Forms.ListBox();
            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblConectados = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.servidorToolStripMenuItem,
            this.mensajeToolStripMenuItem,
            this.clienteToolStripMenuItem,
            this.menuConductores});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // servidorToolStripMenuItem
            // 
            this.servidorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuIniciarServidor,
            this.menuDetenerServidor,
            this.toolStripMenuItem1,
            this.menuSalirServidor});
            this.servidorToolStripMenuItem.Name = "servidorToolStripMenuItem";
            this.servidorToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.servidorToolStripMenuItem.Text = "Servidor";
            // 
            // menuIniciarServidor
            // 
            this.menuIniciarServidor.Name = "menuIniciarServidor";
            this.menuIniciarServidor.Size = new System.Drawing.Size(115, 22);
            this.menuIniciarServidor.Text = "Iniciar";
            this.menuIniciarServidor.Click += new System.EventHandler(this.menuIniciarServidor_Click);
            // 
            // menuDetenerServidor
            // 
            this.menuDetenerServidor.Enabled = false;
            this.menuDetenerServidor.Name = "menuDetenerServidor";
            this.menuDetenerServidor.Size = new System.Drawing.Size(115, 22);
            this.menuDetenerServidor.Text = "Detener";
            this.menuDetenerServidor.Click += new System.EventHandler(this.menuDetenerServidor_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(112, 6);
            // 
            // menuSalirServidor
            // 
            this.menuSalirServidor.Name = "menuSalirServidor";
            this.menuSalirServidor.Size = new System.Drawing.Size(115, 22);
            this.menuSalirServidor.Text = "Salir";
            this.menuSalirServidor.Click += new System.EventHandler(this.menuSalirServidor_Click);
            // 
            // mensajeToolStripMenuItem
            // 
            this.mensajeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEnviarMensajeServidor});
            this.mensajeToolStripMenuItem.Name = "mensajeToolStripMenuItem";
            this.mensajeToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.mensajeToolStripMenuItem.Text = "Mensaje";
            // 
            // menuEnviarMensajeServidor
            // 
            this.menuEnviarMensajeServidor.Name = "menuEnviarMensajeServidor";
            this.menuEnviarMensajeServidor.Size = new System.Drawing.Size(106, 22);
            this.menuEnviarMensajeServidor.Text = "Enviar";
            this.menuEnviarMensajeServidor.Click += new System.EventHandler(this.menuEnviarMensajeServidor_Click);
            // 
            // clienteToolStripMenuItem
            // 
            this.clienteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNuevoCliente});
            this.clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            this.clienteToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.clienteToolStripMenuItem.Text = "Cliente";
            // 
            // menuNuevoCliente
            // 
            this.menuNuevoCliente.Enabled = false;
            this.menuNuevoCliente.Name = "menuNuevoCliente";
            this.menuNuevoCliente.Size = new System.Drawing.Size(149, 22);
            this.menuNuevoCliente.Text = "Nuevo Cliente";
            this.menuNuevoCliente.Click += new System.EventHandler(this.menuNuevoCliente_Click);
            // 
            // menuConductores
            // 
            this.menuConductores.Name = "menuConductores";
            this.menuConductores.Size = new System.Drawing.Size(87, 20);
            this.menuConductores.Text = "Conductores";
            this.menuConductores.Click += new System.EventHandler(this.menuConductores_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 461);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 0);
            this.panel1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.listClientesConectados, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtbLogs, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 361);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 100);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // listClientesConectados
            // 
            this.listClientesConectados.FormattingEnabled = true;
            this.listClientesConectados.Location = new System.Drawing.Point(3, 3);
            this.listClientesConectados.Name = "listClientesConectados";
            this.listClientesConectados.Size = new System.Drawing.Size(244, 95);
            this.listClientesConectados.TabIndex = 3;
            // 
            // rtbLogs
            // 
            this.rtbLogs.Location = new System.Drawing.Point(253, 3);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.ReadOnly = true;
            this.rtbLogs.Size = new System.Drawing.Size(531, 96);
            this.rtbLogs.TabIndex = 4;
            this.rtbLogs.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblConectados});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblConectados
            // 
            this.lblConectados.Name = "lblConectados";
            this.lblConectados.Size = new System.Drawing.Size(118, 17);
            this.lblConectados.Text = "toolStripStatusLabel1";
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServidorTcp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmServer_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem servidorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuIniciarServidor;
        private System.Windows.Forms.ToolStripMenuItem menuDetenerServidor;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuSalirServidor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem clienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNuevoCliente;
        private System.Windows.Forms.ToolStripMenuItem mensajeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuEnviarMensajeServidor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listClientesConectados;
        private System.Windows.Forms.RichTextBox rtbLogs;
        private System.Windows.Forms.ToolStripMenuItem menuConductores;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblConectados;
    }
}

