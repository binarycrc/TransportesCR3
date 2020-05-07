namespace ServerTcp
{
    partial class frmServidor
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
            this.components = new System.ComponentModel.Container();
            this.btnViajesTodos = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCliente = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEnviarMensajeGrupal = new System.Windows.Forms.Button();
            this.txtEnviarMensaje = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnIniciarServidor = new System.Windows.Forms.Button();
            this.btnDetenerServidor = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabViajes = new System.Windows.Forms.TabPage();
            this.btnViajesActivos = new System.Windows.Forms.Button();
            this.btnValidarConductor = new System.Windows.Forms.Button();
            this.gvConductores = new System.Windows.Forms.DataGridView();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.tabValidaConductor = new System.Windows.Forms.TabPage();
            this.btnCargarConductor = new System.Windows.Forms.Button();
            this.btnDenegarConductor = new System.Windows.Forms.Button();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.lblClientesConectados = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.timerCliente = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabViajes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvConductores)).BeginInit();
            this.tabValidaConductor.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnViajesTodos
            // 
            this.btnViajesTodos.Location = new System.Drawing.Point(220, 383);
            this.btnViajesTodos.Name = "btnViajesTodos";
            this.btnViajesTodos.Size = new System.Drawing.Size(143, 35);
            this.btnViajesTodos.TabIndex = 8;
            this.btnViajesTodos.Text = "Todos los Viajes";
            this.btnViajesTodos.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(452, 150);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnCliente
            // 
            this.btnCliente.Location = new System.Drawing.Point(226, 382);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(103, 35);
            this.btnCliente.TabIndex = 8;
            this.btnCliente.Text = "Abrir Cliente";
            this.btnCliente.UseVisualStyleBackColor = true;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEnviarMensajeGrupal);
            this.groupBox1.Controls.Add(this.txtEnviarMensaje);
            this.groupBox1.Location = new System.Drawing.Point(3, 281);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 95);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // btnEnviarMensajeGrupal
            // 
            this.btnEnviarMensajeGrupal.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEnviarMensajeGrupal.Location = new System.Drawing.Point(297, 16);
            this.btnEnviarMensajeGrupal.Name = "btnEnviarMensajeGrupal";
            this.btnEnviarMensajeGrupal.Size = new System.Drawing.Size(143, 76);
            this.btnEnviarMensajeGrupal.TabIndex = 8;
            this.btnEnviarMensajeGrupal.Text = "Enviar Mensaje";
            this.btnEnviarMensajeGrupal.UseVisualStyleBackColor = true;
            this.btnEnviarMensajeGrupal.Click += new System.EventHandler(this.btnEnviarMensajeGrupal_Click);
            // 
            // txtEnviarMensaje
            // 
            this.txtEnviarMensaje.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtEnviarMensaje.Location = new System.Drawing.Point(3, 16);
            this.txtEnviarMensaje.Multiline = true;
            this.txtEnviarMensaje.Name = "txtEnviarMensaje";
            this.txtEnviarMensaje.Size = new System.Drawing.Size(292, 76);
            this.txtEnviarMensaje.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Historial";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(3, 205);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(452, 150);
            this.dataGridView2.TabIndex = 9;
            // 
            // btnIniciarServidor
            // 
            this.btnIniciarServidor.Location = new System.Drawing.Point(117, 382);
            this.btnIniciarServidor.Name = "btnIniciarServidor";
            this.btnIniciarServidor.Size = new System.Drawing.Size(103, 35);
            this.btnIniciarServidor.TabIndex = 4;
            this.btnIniciarServidor.Text = "Iniciar Servidor";
            this.btnIniciarServidor.UseVisualStyleBackColor = true;
            this.btnIniciarServidor.Click += new System.EventHandler(this.btnIniciarServidor_Click);
            // 
            // btnDetenerServidor
            // 
            this.btnDetenerServidor.Location = new System.Drawing.Point(6, 382);
            this.btnDetenerServidor.Name = "btnDetenerServidor";
            this.btnDetenerServidor.Size = new System.Drawing.Size(103, 35);
            this.btnDetenerServidor.TabIndex = 3;
            this.btnDetenerServidor.Text = "Detener Servidor";
            this.btnDetenerServidor.UseVisualStyleBackColor = true;
            this.btnDetenerServidor.Click += new System.EventHandler(this.btnDetenerServidor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Seleccione un viaje";
            // 
            // tabViajes
            // 
            this.tabViajes.Controls.Add(this.label2);
            this.tabViajes.Controls.Add(this.label1);
            this.tabViajes.Controls.Add(this.dataGridView2);
            this.tabViajes.Controls.Add(this.btnViajesTodos);
            this.tabViajes.Controls.Add(this.btnViajesActivos);
            this.tabViajes.Controls.Add(this.dataGridView1);
            this.tabViajes.Location = new System.Drawing.Point(4, 22);
            this.tabViajes.Name = "tabViajes";
            this.tabViajes.Padding = new System.Windows.Forms.Padding(3);
            this.tabViajes.Size = new System.Drawing.Size(461, 429);
            this.tabViajes.TabIndex = 2;
            this.tabViajes.Text = "Viajes";
            this.tabViajes.UseVisualStyleBackColor = true;
            // 
            // btnViajesActivos
            // 
            this.btnViajesActivos.Location = new System.Drawing.Point(71, 383);
            this.btnViajesActivos.Name = "btnViajesActivos";
            this.btnViajesActivos.Size = new System.Drawing.Size(143, 35);
            this.btnViajesActivos.TabIndex = 7;
            this.btnViajesActivos.Text = "Viajes Activos";
            this.btnViajesActivos.UseVisualStyleBackColor = true;
            // 
            // btnValidarConductor
            // 
            this.btnValidarConductor.Location = new System.Drawing.Point(234, 159);
            this.btnValidarConductor.Name = "btnValidarConductor";
            this.btnValidarConductor.Size = new System.Drawing.Size(108, 35);
            this.btnValidarConductor.TabIndex = 5;
            this.btnValidarConductor.Text = "Validar Conductor";
            this.btnValidarConductor.UseVisualStyleBackColor = true;
            this.btnValidarConductor.Click += new System.EventHandler(this.btnValidarConductor_Click);
            // 
            // gvConductores
            // 
            this.gvConductores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvConductores.Dock = System.Windows.Forms.DockStyle.Top;
            this.gvConductores.Location = new System.Drawing.Point(3, 3);
            this.gvConductores.Name = "gvConductores";
            this.gvConductores.Size = new System.Drawing.Size(455, 150);
            this.gvConductores.TabIndex = 0;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(3, 3);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(443, 259);
            this.txtStatus.TabIndex = 1;
            // 
            // tabValidaConductor
            // 
            this.tabValidaConductor.Controls.Add(this.btnCargarConductor);
            this.tabValidaConductor.Controls.Add(this.btnDenegarConductor);
            this.tabValidaConductor.Controls.Add(this.btnValidarConductor);
            this.tabValidaConductor.Controls.Add(this.gvConductores);
            this.tabValidaConductor.Location = new System.Drawing.Point(4, 22);
            this.tabValidaConductor.Name = "tabValidaConductor";
            this.tabValidaConductor.Padding = new System.Windows.Forms.Padding(3);
            this.tabValidaConductor.Size = new System.Drawing.Size(461, 429);
            this.tabValidaConductor.TabIndex = 1;
            this.tabValidaConductor.Text = "Validar Conductor";
            this.tabValidaConductor.UseVisualStyleBackColor = true;
            // 
            // btnCargarConductor
            // 
            this.btnCargarConductor.Location = new System.Drawing.Point(3, 159);
            this.btnCargarConductor.Name = "btnCargarConductor";
            this.btnCargarConductor.Size = new System.Drawing.Size(108, 35);
            this.btnCargarConductor.TabIndex = 7;
            this.btnCargarConductor.Text = "Ver Conductores";
            this.btnCargarConductor.UseVisualStyleBackColor = true;
            this.btnCargarConductor.Click += new System.EventHandler(this.btnCargarConductor_Click);
            // 
            // btnDenegarConductor
            // 
            this.btnDenegarConductor.Location = new System.Drawing.Point(350, 159);
            this.btnDenegarConductor.Name = "btnDenegarConductor";
            this.btnDenegarConductor.Size = new System.Drawing.Size(108, 35);
            this.btnDenegarConductor.TabIndex = 6;
            this.btnDenegarConductor.Text = "Denegar Conductor";
            this.btnDenegarConductor.UseVisualStyleBackColor = true;
            this.btnDenegarConductor.Click += new System.EventHandler(this.btnDenegarConductor_Click);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.lblClientesConectados);
            this.tabMain.Controls.Add(this.btnCliente);
            this.tabMain.Controls.Add(this.groupBox1);
            this.tabMain.Controls.Add(this.btnIniciarServidor);
            this.tabMain.Controls.Add(this.btnDetenerServidor);
            this.tabMain.Controls.Add(this.txtStatus);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(461, 429);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Servidor";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // lblClientesConectados
            // 
            this.lblClientesConectados.AutoSize = true;
            this.lblClientesConectados.Location = new System.Drawing.Point(9, 265);
            this.lblClientesConectados.Name = "lblClientesConectados";
            this.lblClientesConectados.Size = new System.Drawing.Size(115, 13);
            this.lblClientesConectados.TabIndex = 9;
            this.lblClientesConectados.Text = "Clientes conectados: 0";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabValidaConductor);
            this.tabControl1.Controls.Add(this.tabViajes);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(469, 455);
            this.tabControl1.TabIndex = 1;
            // 
            // frmServidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 455);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmServidor";
            this.Text = "Servidor";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabViajes.ResumeLayout(false);
            this.tabViajes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvConductores)).EndInit();
            this.tabValidaConductor.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnViajesTodos;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEnviarMensajeGrupal;
        private System.Windows.Forms.TextBox txtEnviarMensaje;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnIniciarServidor;
        private System.Windows.Forms.Button btnDetenerServidor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabViajes;
        private System.Windows.Forms.Button btnViajesActivos;
        private System.Windows.Forms.Button btnValidarConductor;
        private System.Windows.Forms.DataGridView gvConductores;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TabPage tabValidaConductor;
        private System.Windows.Forms.Button btnDenegarConductor;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label lblClientesConectados;
        private System.Windows.Forms.Button btnCargarConductor;
        private System.Windows.Forms.Timer timerCliente;
    }
}