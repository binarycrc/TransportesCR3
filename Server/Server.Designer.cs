namespace Server
{
    partial class Server
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
            this.btnServerStart = new System.Windows.Forms.Button();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.btnServerStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnServerStart
            // 
            this.btnServerStart.Location = new System.Drawing.Point(39, 51);
            this.btnServerStart.Name = "btnServerStart";
            this.btnServerStart.Size = new System.Drawing.Size(75, 23);
            this.btnServerStart.TabIndex = 0;
            this.btnServerStart.Text = "Iniciar";
            this.btnServerStart.UseVisualStyleBackColor = true;
            this.btnServerStart.Click += new System.EventHandler(this.btnServerStart_Click);
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.Location = new System.Drawing.Point(36, 130);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(90, 13);
            this.lblServerStatus.TabIndex = 1;
            this.lblServerStatus.Text = "Servidor detenido";
            // 
            // btnServerStop
            // 
            this.btnServerStop.Location = new System.Drawing.Point(39, 92);
            this.btnServerStop.Name = "btnServerStop";
            this.btnServerStop.Size = new System.Drawing.Size(75, 23);
            this.btnServerStop.TabIndex = 2;
            this.btnServerStop.Text = "Detener";
            this.btnServerStop.UseVisualStyleBackColor = true;
            this.btnServerStop.Click += new System.EventHandler(this.btnServerStop_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnServerStop);
            this.Controls.Add(this.lblServerStatus);
            this.Controls.Add(this.btnServerStart);
            this.Name = "Server";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnServerStart;
        private System.Windows.Forms.Label lblServerStatus;
        private System.Windows.Forms.Button btnServerStop;
    }
}

