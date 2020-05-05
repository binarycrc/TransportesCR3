namespace ClienteTcp
{
    partial class frmViaje
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
            this.btnIngresoViaje = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLugarInicio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLugarFinal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTiempoEstimado = new System.Windows.Forms.TextBox();
            this.gbIngresoViaje = new System.Windows.Forms.GroupBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gbViajePendiente = new System.Windows.Forms.GroupBox();
            this.txtNuevaUbicacion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.gbIngresoViaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbViajePendiente.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnIngresoViaje
            // 
            this.btnIngresoViaje.Location = new System.Drawing.Point(190, 15);
            this.btnIngresoViaje.Name = "btnIngresoViaje";
            this.btnIngresoViaje.Size = new System.Drawing.Size(106, 41);
            this.btnIngresoViaje.TabIndex = 0;
            this.btnIngresoViaje.Text = "Registro";
            this.btnIngresoViaje.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(190, 72);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(106, 39);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lugar de Inicio de viaje";
            // 
            // txtLugarInicio
            // 
            this.txtLugarInicio.Location = new System.Drawing.Point(6, 36);
            this.txtLugarInicio.Name = "txtLugarInicio";
            this.txtLugarInicio.Size = new System.Drawing.Size(178, 20);
            this.txtLugarInicio.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Lugar de finalización del viaje";
            // 
            // txtLugarFinal
            // 
            this.txtLugarFinal.Location = new System.Drawing.Point(6, 75);
            this.txtLugarFinal.Name = "txtLugarFinal";
            this.txtLugarFinal.Size = new System.Drawing.Size(178, 20);
            this.txtLugarFinal.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Descripción de la carga a transportar";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 114);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(178, 20);
            this.textBox2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tiempo estimado del viaje";
            // 
            // txtTiempoEstimado
            // 
            this.txtTiempoEstimado.Location = new System.Drawing.Point(6, 153);
            this.txtTiempoEstimado.Name = "txtTiempoEstimado";
            this.txtTiempoEstimado.Size = new System.Drawing.Size(178, 20);
            this.txtTiempoEstimado.TabIndex = 9;
            // 
            // gbIngresoViaje
            // 
            this.gbIngresoViaje.Controls.Add(this.txtTiempoEstimado);
            this.gbIngresoViaje.Controls.Add(this.label4);
            this.gbIngresoViaje.Controls.Add(this.textBox2);
            this.gbIngresoViaje.Controls.Add(this.label3);
            this.gbIngresoViaje.Controls.Add(this.txtLugarFinal);
            this.gbIngresoViaje.Controls.Add(this.label2);
            this.gbIngresoViaje.Controls.Add(this.txtLugarInicio);
            this.gbIngresoViaje.Controls.Add(this.label1);
            this.gbIngresoViaje.Controls.Add(this.btnSalir);
            this.gbIngresoViaje.Controls.Add(this.btnIngresoViaje);
            this.gbIngresoViaje.Location = new System.Drawing.Point(12, 12);
            this.gbIngresoViaje.Name = "gbIngresoViaje";
            this.gbIngresoViaje.Size = new System.Drawing.Size(350, 283);
            this.gbIngresoViaje.TabIndex = 0;
            this.gbIngresoViaje.TabStop = false;
            this.gbIngresoViaje.Text = "Ingreso de Viajes";
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
            this.lblMensaje.Location = new System.Drawing.Point(19, 298);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(35, 13);
            this.lblMensaje.TabIndex = 19;
            this.lblMensaje.Text = "label8";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 83);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(484, 108);
            this.dataGridView1.TabIndex = 0;
            // 
            // gbViajePendiente
            // 
            this.gbViajePendiente.Controls.Add(this.button2);
            this.gbViajePendiente.Controls.Add(this.txtObservaciones);
            this.gbViajePendiente.Controls.Add(this.label7);
            this.gbViajePendiente.Controls.Add(this.label6);
            this.gbViajePendiente.Controls.Add(this.txtNuevaUbicacion);
            this.gbViajePendiente.Controls.Add(this.label5);
            this.gbViajePendiente.Controls.Add(this.button1);
            this.gbViajePendiente.Controls.Add(this.dataGridView1);
            this.gbViajePendiente.Location = new System.Drawing.Point(414, 48);
            this.gbViajePendiente.Name = "gbViajePendiente";
            this.gbViajePendiente.Size = new System.Drawing.Size(614, 254);
            this.gbViajePendiente.TabIndex = 1;
            this.gbViajePendiente.TabStop = false;
            this.gbViajePendiente.Text = "Viaje Pendiente";
            // 
            // txtNuevaUbicacion
            // 
            this.txtNuevaUbicacion.Location = new System.Drawing.Point(6, 57);
            this.txtNuevaUbicacion.Name = "txtNuevaUbicacion";
            this.txtNuevaUbicacion.Size = new System.Drawing.Size(178, 20);
            this.txtNuevaUbicacion.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Nueva Ubicación";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(384, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 41);
            this.button1.TabIndex = 4;
            this.button1.Text = "Registro";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Nueva Ubicación";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(189, 55);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(178, 20);
            this.txtObservaciones.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Observaciones";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(496, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 39);
            this.button2.TabIndex = 10;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // frmViaje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 498);
            this.Controls.Add(this.gbViajePendiente);
            this.Controls.Add(this.gbIngresoViaje);
            this.Controls.Add(this.lblMensaje);
            this.Name = "frmViaje";
            this.Text = "Viaje";
            this.gbIngresoViaje.ResumeLayout(false);
            this.gbIngresoViaje.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbViajePendiente.ResumeLayout(false);
            this.gbViajePendiente.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIngresoViaje;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLugarInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLugarFinal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTiempoEstimado;
        private System.Windows.Forms.GroupBox gbIngresoViaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox gbViajePendiente;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNuevaUbicacion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
    }
}