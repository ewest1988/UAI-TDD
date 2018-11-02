namespace UI
{
    partial class cambiarContraseña
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
            this.Button2 = new System.Windows.Forms.Button();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(208, 137);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(75, 23);
            this.Button2.TabIndex = 15;
            this.Button2.Text = "Cancelar";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new System.Drawing.Point(184, 94);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.PasswordChar = '*';
            this.TextBox3.Size = new System.Drawing.Size(139, 20);
            this.TextBox3.TabIndex = 14;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(184, 77);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(133, 13);
            this.Label3.TabIndex = 13;
            this.Label3.Text = "Repita Contraseña Nueva:";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(184, 45);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.PasswordChar = '*';
            this.TextBox2.Size = new System.Drawing.Size(139, 20);
            this.TextBox2.TabIndex = 12;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(184, 28);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(99, 13);
            this.Label2.TabIndex = 11;
            this.Label2.Text = "Contraseña Nueva:";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(12, 45);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.PasswordChar = '*';
            this.TextBox1.Size = new System.Drawing.Size(139, 20);
            this.TextBox1.TabIndex = 10;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 28);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(97, 13);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Contraseña Actual:";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(43, 137);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 8;
            this.Button1.Text = "Guardar";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // cambiarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 178);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.TextBox3);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Button1);
            this.Name = "cambiarContraseña";
            this.Text = "cambiarContraseña";
            this.Load += new System.EventHandler(this.cambiarContraseña_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.TextBox TextBox3;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button Button1;
    }
}