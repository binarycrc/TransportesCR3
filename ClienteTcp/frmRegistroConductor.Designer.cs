namespace ClienteTcp
{
    partial class frmRegistroConductor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnRegistroConductor = new System.Windows.Forms.Button();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numModelo = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPApellido = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numModelo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtIdentificacion);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblMensaje);
            this.groupBox1.Controls.Add(this.btnSalir);
            this.groupBox1.Controls.Add(this.btnRegistroConductor);
            this.groupBox1.Controls.Add(this.cbMarca);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numModelo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPlaca);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSApellido);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPApellido);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 213);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registro de Conductor";
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.Location = new System.Drawing.Point(6, 32);
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.Size = new System.Drawing.Size(109, 20);
            this.txtIdentificacion.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Identificación";
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
            this.lblMensaje.Location = new System.Drawing.Point(6, 172);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(35, 13);
            this.lblMensaje.TabIndex = 17;
            this.lblMensaje.Text = "label8";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(262, 97);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(123, 33);
            this.btnSalir.TabIndex = 16;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnRegistroConductor
            // 
            this.btnRegistroConductor.Location = new System.Drawing.Point(262, 19);
            this.btnRegistroConductor.Name = "btnRegistroConductor";
            this.btnRegistroConductor.Size = new System.Drawing.Size(123, 33);
            this.btnRegistroConductor.TabIndex = 15;
            this.btnRegistroConductor.Text = "Registrar";
            this.btnRegistroConductor.UseVisualStyleBackColor = true;
            this.btnRegistroConductor.Click += new System.EventHandler(this.btnRegistroConductor_Click);
            // 
            // cbMarca
            // 
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Items.AddRange(new object[] {
            "Acura",
            "Alfa Romeo",
            "AMC",
            "Aro",
            "Asia",
            "Aston Martin",
            "Audi",
            "Austin",
            "Baw",
            "Bentley",
            "Bluebird",
            "BMW",
            "Brilliance",
            "Buick",
            "BYD",
            "Cadillac",
            "Chana",
            "Changan",
            "Chery",
            "Chevrolet",
            "Chrysler",
            "Citroen",
            "Dacia",
            "Daewoo",
            "Daihatsu",
            "Datsun",
            "Dodge/RAM",
            "Donfeng (ZNA)",
            "Eagle",
            "Faw",
            "Ferrari",
            "Fiat",
            "Ford",
            "Foton",
            "Freightliner",
            "Geely",
            "Genesis",
            "Geo",
            "GMC",
            "Gonow",
            "Great Wall",
            "Hafei",
            "Haima",
            "Heibao",
            "Higer",
            "Hino",
            "Honda",
            "Hummer",
            "Hyundai",
            "Infiniti",
            "International",
            "Isuzu",
            "Iveco",
            "JAC",
            "Jaguar",
            "Jeep",
            "Jinbei",
            "JMC",
            "Jonway",
            "Kenworth",
            "Kia",
            "Lada",
            "Lamborghini",
            "Lancia",
            "Land Rover",
            "Lexus",
            "Lifan",
            "Lincoln",
            "Lotus",
            "Mack",
            "Magiruz",
            "Mahindra",
            "Maserati",
            "Mazda",
            "Mercedes Benz",
            "Mercury",
            "MG",
            "Mini",
            "Mitsubishi\t",
            "Nissan",
            "Oldsmobile",
            "Opel",
            "Peterbilt",
            "Peugeot",
            "Plymouth",
            "Polarsun",
            "Pontiac",
            "Porsche",
            "Proton",
            "Rambler",
            "Renault",
            "Reva",
            "Rolls Royce",
            "Rover",
            "Saab",
            "Samsung",
            "Saturn",
            "Scania",
            "Scion",
            "Seat",
            "Skoda",
            "Smart",
            "Soueast",
            "Ssang Yong",
            "Subaru",
            "Suzuki",
            "Tianma",
            "Tiger Truck",
            "Toyota",
            "Volkswagen",
            "Volvo",
            "Western Star",
            "Yugo",
            "Zotye"});
            this.cbMarca.Location = new System.Drawing.Point(130, 149);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(109, 21);
            this.cbMarca.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(133, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Marca Camión:";
            // 
            // numModelo
            // 
            this.numModelo.Location = new System.Drawing.Point(130, 111);
            this.numModelo.Maximum = new decimal(new int[] {
            2021,
            0,
            0,
            0});
            this.numModelo.Minimum = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.numModelo.Name = "numModelo";
            this.numModelo.ReadOnly = true;
            this.numModelo.Size = new System.Drawing.Size(109, 20);
            this.numModelo.TabIndex = 12;
            this.numModelo.Value = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(133, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Modelo Camión:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(130, 71);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(109, 20);
            this.txtPlaca.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(130, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Placa Camión:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(130, 32);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(109, 20);
            this.txtUsuario.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Usuario:";
            // 
            // txtSApellido
            // 
            this.txtSApellido.Location = new System.Drawing.Point(6, 149);
            this.txtSApellido.Name = "txtSApellido";
            this.txtSApellido.Size = new System.Drawing.Size(109, 20);
            this.txtSApellido.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Segundo Apellido:";
            // 
            // txtPApellido
            // 
            this.txtPApellido.Location = new System.Drawing.Point(6, 110);
            this.txtPApellido.Name = "txtPApellido";
            this.txtPApellido.Size = new System.Drawing.Size(109, 20);
            this.txtPApellido.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Primer Apellido:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(6, 71);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(109, 20);
            this.txtNombre.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre:";
            // 
            // frmRegistroConductor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 241);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRegistroConductor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Registro de Conductor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numModelo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnRegistroConductor;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numModelo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPApellido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.TextBox txtIdentificacion;
        private System.Windows.Forms.Label label8;
    }
}