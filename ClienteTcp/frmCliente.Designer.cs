namespace ClienteTcp
{
    partial class frmCliente
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabIngreso = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkRegistrarse = new System.Windows.Forms.LinkLabel();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabRegistroConductor = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRegistroConductor = new System.Windows.Forms.Button();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numModelo = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPApellido = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabIngresoViajes = new System.Windows.Forms.TabPage();
            this.gbIngresoViaje = new System.Windows.Forms.GroupBox();
            this.txtTiempoEstimado = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcionCarga = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLugarFinal = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLugarInicio = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnIngresoViaje = new System.Windows.Forms.Button();
            this.tabViajes = new System.Windows.Forms.TabPage();
            this.gbViajePendiente = new System.Windows.Forms.GroupBox();
            this.btnFinalizarViaje = new System.Windows.Forms.Button();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtNuevaUbicacion = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnActualizarViaje = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnConectarCliente = new System.Windows.Forms.Button();
            this.tabTrash = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblGUIDActivo = new System.Windows.Forms.Label();
            this.tabMain.SuspendLayout();
            this.tabIngreso.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabRegistroConductor.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numModelo)).BeginInit();
            this.tabIngresoViajes.SuspendLayout();
            this.gbIngresoViaje.SuspendLayout();
            this.tabViajes.SuspendLayout();
            this.gbViajePendiente.SuspendLayout();
            this.tabTrash.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabIngreso);
            this.tabMain.Controls.Add(this.tabRegistroConductor);
            this.tabMain.Controls.Add(this.tabIngresoViajes);
            this.tabMain.Controls.Add(this.tabViajes);
            this.tabMain.Location = new System.Drawing.Point(12, 3);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(530, 231);
            this.tabMain.TabIndex = 1;
            // 
            // tabIngreso
            // 
            this.tabIngreso.Controls.Add(this.groupBox1);
            this.tabIngreso.Location = new System.Drawing.Point(4, 22);
            this.tabIngreso.Name = "tabIngreso";
            this.tabIngreso.Padding = new System.Windows.Forms.Padding(3);
            this.tabIngreso.Size = new System.Drawing.Size(522, 205);
            this.tabIngreso.TabIndex = 0;
            this.tabIngreso.Text = "Ingreso";
            this.tabIngreso.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkRegistrarse);
            this.groupBox1.Controls.Add(this.btnIngresar);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 132);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // linkRegistrarse
            // 
            this.linkRegistrarse.AutoSize = true;
            this.linkRegistrarse.Enabled = false;
            this.linkRegistrarse.Location = new System.Drawing.Point(47, 100);
            this.linkRegistrarse.Name = "linkRegistrarse";
            this.linkRegistrarse.Size = new System.Drawing.Size(60, 13);
            this.linkRegistrarse.TabIndex = 4;
            this.linkRegistrarse.TabStop = true;
            this.linkRegistrarse.Text = "Registrarse";
            this.linkRegistrarse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRegistrarse_LinkClicked);
            // 
            // btnIngresar
            // 
            this.btnIngresar.Enabled = false;
            this.btnIngresar.Location = new System.Drawing.Point(31, 63);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(100, 23);
            this.btnIngresar.TabIndex = 2;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = true;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(7, 37);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(156, 20);
            this.txtUsuario.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // tabRegistroConductor
            // 
            this.tabRegistroConductor.Controls.Add(this.groupBox2);
            this.tabRegistroConductor.Location = new System.Drawing.Point(4, 22);
            this.tabRegistroConductor.Name = "tabRegistroConductor";
            this.tabRegistroConductor.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegistroConductor.Size = new System.Drawing.Size(522, 205);
            this.tabRegistroConductor.TabIndex = 1;
            this.tabRegistroConductor.Text = "Registro de Conductor";
            this.tabRegistroConductor.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtIdentificacion);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnRegistroConductor);
            this.groupBox2.Controls.Add(this.cbMarca);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.numModelo);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtPlaca);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtUserName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtSApellido);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtPApellido);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtNombre);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 185);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Registro de Conductor";
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.Location = new System.Drawing.Point(6, 32);
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.Size = new System.Drawing.Size(109, 20);
            this.txtIdentificacion.TabIndex = 19;
            this.txtIdentificacion.Text = "1";
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
            this.cbMarca.Text = "1";
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
            this.txtPlaca.Text = "1";
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
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(130, 32);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(109, 20);
            this.txtUserName.TabIndex = 8;
            this.txtUserName.Text = "1";
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
            this.txtSApellido.Text = "1";
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
            this.txtPApellido.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Primer Apellido:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(6, 71);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(109, 20);
            this.txtNombre.TabIndex = 2;
            this.txtNombre.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Nombre:";
            // 
            // tabIngresoViajes
            // 
            this.tabIngresoViajes.Controls.Add(this.gbIngresoViaje);
            this.tabIngresoViajes.Location = new System.Drawing.Point(4, 22);
            this.tabIngresoViajes.Name = "tabIngresoViajes";
            this.tabIngresoViajes.Padding = new System.Windows.Forms.Padding(3);
            this.tabIngresoViajes.Size = new System.Drawing.Size(522, 205);
            this.tabIngresoViajes.TabIndex = 2;
            this.tabIngresoViajes.Text = "Ingreso de Viajes";
            this.tabIngresoViajes.UseVisualStyleBackColor = true;
            // 
            // gbIngresoViaje
            // 
            this.gbIngresoViaje.Controls.Add(this.txtTiempoEstimado);
            this.gbIngresoViaje.Controls.Add(this.label2);
            this.gbIngresoViaje.Controls.Add(this.txtDescripcionCarga);
            this.gbIngresoViaje.Controls.Add(this.label11);
            this.gbIngresoViaje.Controls.Add(this.txtLugarFinal);
            this.gbIngresoViaje.Controls.Add(this.label12);
            this.gbIngresoViaje.Controls.Add(this.txtLugarInicio);
            this.gbIngresoViaje.Controls.Add(this.label13);
            this.gbIngresoViaje.Controls.Add(this.btnIngresoViaje);
            this.gbIngresoViaje.Location = new System.Drawing.Point(6, 6);
            this.gbIngresoViaje.Name = "gbIngresoViaje";
            this.gbIngresoViaje.Size = new System.Drawing.Size(350, 185);
            this.gbIngresoViaje.TabIndex = 1;
            this.gbIngresoViaje.TabStop = false;
            this.gbIngresoViaje.Text = "Ingreso de Viajes";
            // 
            // txtTiempoEstimado
            // 
            this.txtTiempoEstimado.Location = new System.Drawing.Point(6, 153);
            this.txtTiempoEstimado.Name = "txtTiempoEstimado";
            this.txtTiempoEstimado.Size = new System.Drawing.Size(178, 20);
            this.txtTiempoEstimado.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tiempo estimado del viaje";
            // 
            // txtDescripcionCarga
            // 
            this.txtDescripcionCarga.Location = new System.Drawing.Point(6, 114);
            this.txtDescripcionCarga.Name = "txtDescripcionCarga";
            this.txtDescripcionCarga.Size = new System.Drawing.Size(178, 20);
            this.txtDescripcionCarga.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(181, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Descripción de la carga a transportar";
            // 
            // txtLugarFinal
            // 
            this.txtLugarFinal.Location = new System.Drawing.Point(6, 75);
            this.txtLugarFinal.Name = "txtLugarFinal";
            this.txtLugarFinal.Size = new System.Drawing.Size(178, 20);
            this.txtLugarFinal.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Lugar de finalización del viaje";
            // 
            // txtLugarInicio
            // 
            this.txtLugarInicio.Location = new System.Drawing.Point(6, 36);
            this.txtLugarInicio.Name = "txtLugarInicio";
            this.txtLugarInicio.Size = new System.Drawing.Size(178, 20);
            this.txtLugarInicio.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(117, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Lugar de Inicio de viaje";
            // 
            // btnIngresoViaje
            // 
            this.btnIngresoViaje.Location = new System.Drawing.Point(190, 15);
            this.btnIngresoViaje.Name = "btnIngresoViaje";
            this.btnIngresoViaje.Size = new System.Drawing.Size(106, 41);
            this.btnIngresoViaje.TabIndex = 0;
            this.btnIngresoViaje.Text = "Registro";
            this.btnIngresoViaje.UseVisualStyleBackColor = true;
            this.btnIngresoViaje.Click += new System.EventHandler(this.btnIngresoViaje_Click);
            // 
            // tabViajes
            // 
            this.tabViajes.Controls.Add(this.gbViajePendiente);
            this.tabViajes.Location = new System.Drawing.Point(4, 22);
            this.tabViajes.Name = "tabViajes";
            this.tabViajes.Padding = new System.Windows.Forms.Padding(3);
            this.tabViajes.Size = new System.Drawing.Size(522, 205);
            this.tabViajes.TabIndex = 3;
            this.tabViajes.Text = "Viajes";
            this.tabViajes.UseVisualStyleBackColor = true;
            // 
            // gbViajePendiente
            // 
            this.gbViajePendiente.Controls.Add(this.lblGUIDActivo);
            this.gbViajePendiente.Controls.Add(this.btnFinalizarViaje);
            this.gbViajePendiente.Controls.Add(this.txtObservaciones);
            this.gbViajePendiente.Controls.Add(this.label14);
            this.gbViajePendiente.Controls.Add(this.label15);
            this.gbViajePendiente.Controls.Add(this.txtNuevaUbicacion);
            this.gbViajePendiente.Controls.Add(this.label16);
            this.gbViajePendiente.Controls.Add(this.btnActualizarViaje);
            this.gbViajePendiente.Location = new System.Drawing.Point(6, 5);
            this.gbViajePendiente.Name = "gbViajePendiente";
            this.gbViajePendiente.Size = new System.Drawing.Size(503, 198);
            this.gbViajePendiente.TabIndex = 2;
            this.gbViajePendiente.TabStop = false;
            // 
            // btnFinalizarViaje
            // 
            this.btnFinalizarViaje.Location = new System.Drawing.Point(384, 48);
            this.btnFinalizarViaje.Name = "btnFinalizarViaje";
            this.btnFinalizarViaje.Size = new System.Drawing.Size(106, 32);
            this.btnFinalizarViaje.TabIndex = 10;
            this.btnFinalizarViaje.Text = "Finalizar Viaje";
            this.btnFinalizarViaje.UseVisualStyleBackColor = true;
            this.btnFinalizarViaje.Click += new System.EventHandler(this.btnFinalizarViaje_Click);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(193, 90);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(178, 20);
            this.txtObservaciones.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(190, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "Observaciones";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(7, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 16);
            this.label15.TabIndex = 7;
            this.label15.Text = "Viaje Activo:";
            // 
            // txtNuevaUbicacion
            // 
            this.txtNuevaUbicacion.Location = new System.Drawing.Point(6, 90);
            this.txtNuevaUbicacion.Name = "txtNuevaUbicacion";
            this.txtNuevaUbicacion.Size = new System.Drawing.Size(178, 20);
            this.txtNuevaUbicacion.TabIndex = 6;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(90, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Nueva Ubicación";
            // 
            // btnActualizarViaje
            // 
            this.btnActualizarViaje.Location = new System.Drawing.Point(384, 10);
            this.btnActualizarViaje.Name = "btnActualizarViaje";
            this.btnActualizarViaje.Size = new System.Drawing.Size(106, 32);
            this.btnActualizarViaje.TabIndex = 4;
            this.btnActualizarViaje.Text = "Registro";
            this.btnActualizarViaje.UseVisualStyleBackColor = true;
            this.btnActualizarViaje.Click += new System.EventHandler(this.btnActualizarViaje_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
            this.lblMensaje.Location = new System.Drawing.Point(19, 242);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(35, 13);
            this.lblMensaje.TabIndex = 19;
            this.lblMensaje.Text = "label8";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(450, 237);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 20;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtStatus.Location = new System.Drawing.Point(0, 280);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(561, 164);
            this.txtStatus.TabIndex = 21;
            // 
            // btnConectarCliente
            // 
            this.btnConectarCliente.Location = new System.Drawing.Point(318, 237);
            this.btnConectarCliente.Name = "btnConectarCliente";
            this.btnConectarCliente.Size = new System.Drawing.Size(111, 23);
            this.btnConectarCliente.TabIndex = 22;
            this.btnConectarCliente.Text = "Conectar Cliente";
            this.btnConectarCliente.UseVisualStyleBackColor = true;
            this.btnConectarCliente.Click += new System.EventHandler(this.btnConectarCliente_Click);
            // 
            // tabTrash
            // 
            this.tabTrash.Controls.Add(this.tabPage1);
            this.tabTrash.Controls.Add(this.tabPage2);
            this.tabTrash.Location = new System.Drawing.Point(0, 278);
            this.tabTrash.Name = "tabTrash";
            this.tabTrash.SelectedIndex = 0;
            this.tabTrash.Size = new System.Drawing.Size(113, 56);
            this.tabTrash.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(105, 30);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblGUIDActivo
            // 
            this.lblGUIDActivo.AutoSize = true;
            this.lblGUIDActivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGUIDActivo.Location = new System.Drawing.Point(7, 40);
            this.lblGUIDActivo.Name = "lblGUIDActivo";
            this.lblGUIDActivo.Size = new System.Drawing.Size(127, 16);
            this.lblGUIDActivo.TabIndex = 11;
            this.lblGUIDActivo.Text = "Nueva Ubicación";
            // 
            // frmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 444);
            this.Controls.Add(this.btnConectarCliente);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.tabTrash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cliente";
            this.tabMain.ResumeLayout(false);
            this.tabIngreso.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabRegistroConductor.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numModelo)).EndInit();
            this.tabIngresoViajes.ResumeLayout(false);
            this.gbIngresoViaje.ResumeLayout(false);
            this.gbIngresoViaje.PerformLayout();
            this.tabViajes.ResumeLayout(false);
            this.gbViajePendiente.ResumeLayout(false);
            this.gbViajePendiente.PerformLayout();
            this.tabTrash.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabIngreso;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkRegistrarse;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabRegistroConductor;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtIdentificacion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnRegistroConductor;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numModelo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPApellido;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabIngresoViajes;
        private System.Windows.Forms.GroupBox gbIngresoViaje;
        private System.Windows.Forms.TextBox txtTiempoEstimado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescripcionCarga;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLugarFinal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLugarInicio;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnIngresoViaje;
        private System.Windows.Forms.TabPage tabViajes;
        private System.Windows.Forms.GroupBox gbViajePendiente;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNuevaUbicacion;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnActualizarViaje;
        private System.Windows.Forms.Button btnFinalizarViaje;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnConectarCliente;
        private System.Windows.Forms.TabControl tabTrash;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblGUIDActivo;
    }
}

