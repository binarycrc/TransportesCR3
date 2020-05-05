namespace ServerTcp
{
    partial class frmConductores
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
            this.btnConsultarConductor = new System.Windows.Forms.Button();
            this.gvConductores = new System.Windows.Forms.DataGridView();
            this.btnValidarConductor = new System.Windows.Forms.Button();
            this.btnDenegar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvConductores)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConsultarConductor
            // 
            this.btnConsultarConductor.Location = new System.Drawing.Point(12, 12);
            this.btnConsultarConductor.Name = "btnConsultarConductor";
            this.btnConsultarConductor.Size = new System.Drawing.Size(151, 49);
            this.btnConsultarConductor.TabIndex = 0;
            this.btnConsultarConductor.Text = "Consultar Todos";
            this.btnConsultarConductor.UseVisualStyleBackColor = true;
            this.btnConsultarConductor.Click += new System.EventHandler(this.btnConsultarConductor_Click);
            // 
            // gvConductores
            // 
            this.gvConductores.AllowUserToAddRows = false;
            this.gvConductores.AllowUserToDeleteRows = false;
            this.gvConductores.AllowUserToResizeRows = false;
            this.gvConductores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvConductores.Location = new System.Drawing.Point(12, 70);
            this.gvConductores.Name = "gvConductores";
            this.gvConductores.Size = new System.Drawing.Size(493, 148);
            this.gvConductores.TabIndex = 1;
            // 
            // btnValidarConductor
            // 
            this.btnValidarConductor.Location = new System.Drawing.Point(182, 12);
            this.btnValidarConductor.Name = "btnValidarConductor";
            this.btnValidarConductor.Size = new System.Drawing.Size(151, 49);
            this.btnValidarConductor.TabIndex = 2;
            this.btnValidarConductor.Text = "Validar";
            this.btnValidarConductor.UseVisualStyleBackColor = true;
            this.btnValidarConductor.Click += new System.EventHandler(this.btnValidarConductor_Click);
            // 
            // btnDenegar
            // 
            this.btnDenegar.Location = new System.Drawing.Point(354, 12);
            this.btnDenegar.Name = "btnDenegar";
            this.btnDenegar.Size = new System.Drawing.Size(151, 49);
            this.btnDenegar.TabIndex = 3;
            this.btnDenegar.Text = "Denegar";
            this.btnDenegar.UseVisualStyleBackColor = true;
            this.btnDenegar.Click += new System.EventHandler(this.btnDenegar_Click);
            // 
            // frmConductores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 232);
            this.Controls.Add(this.btnDenegar);
            this.Controls.Add(this.btnValidarConductor);
            this.Controls.Add(this.gvConductores);
            this.Controls.Add(this.btnConsultarConductor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConductores";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Conductores";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.gvConductores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConsultarConductor;
        private System.Windows.Forms.DataGridView gvConductores;
        private System.Windows.Forms.Button btnValidarConductor;
        private System.Windows.Forms.Button btnDenegar;
    }
}