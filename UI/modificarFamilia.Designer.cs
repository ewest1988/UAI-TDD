namespace UI
{
    partial class modificarFamilia
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
            this.Label1 = new System.Windows.Forms.Label();
            this.ListBox2 = new System.Windows.Forms.ListBox();
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Label1.Location = new System.Drawing.Point(219, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(151, 24);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Modificar Familia";
            // 
            // ListBox2
            // 
            this.ListBox2.FormattingEnabled = true;
            this.ListBox2.Location = new System.Drawing.Point(327, 71);
            this.ListBox2.Name = "ListBox2";
            this.ListBox2.Size = new System.Drawing.Size(239, 251);
            this.ListBox2.TabIndex = 8;
            // 
            // ListBox1
            // 
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.Location = new System.Drawing.Point(13, 71);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(239, 251);
            this.ListBox1.TabIndex = 7;
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(71, 378);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(120, 23);
            this.Button2.TabIndex = 6;
            this.Button2.Text = "Desvincular Permiso";
            this.Button2.UseVisualStyleBackColor = true;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(391, 378);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(120, 23);
            this.Button1.TabIndex = 5;
            this.Button1.Text = "Vincular Permiso";
            this.Button1.UseVisualStyleBackColor = true;
            // 
            // modificarFamilia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 423);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.ListBox2);
            this.Controls.Add(this.ListBox1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Name = "modificarFamilia";
            this.Text = "modificarFamilia";
            this.Load += new System.EventHandler(this.modificarFamilia_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ListBox ListBox2;
        internal System.Windows.Forms.ListBox ListBox1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
    }
}