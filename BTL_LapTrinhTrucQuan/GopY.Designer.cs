namespace BTL_LapTrinhTrucQuan
{
    partial class GopY
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
            this.btnGUI_GY = new System.Windows.Forms.Button();
            this.txtnd = new System.Windows.Forms.TextBox();
            this.txttn = new System.Windows.Forms.GroupBox();
            this.rdbkhl = new System.Windows.Forms.RadioButton();
            this.rdbhl1 = new System.Windows.Forms.RadioButton();
            this.rdbhl = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtphangopy = new System.Windows.Forms.TextBox();
            this.txttn.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGUI_GY
            // 
            this.btnGUI_GY.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnGUI_GY.ForeColor = System.Drawing.SystemColors.Control;
            this.btnGUI_GY.Location = new System.Drawing.Point(124, 345);
            this.btnGUI_GY.Margin = new System.Windows.Forms.Padding(2);
            this.btnGUI_GY.Name = "btnGUI_GY";
            this.btnGUI_GY.Size = new System.Drawing.Size(80, 30);
            this.btnGUI_GY.TabIndex = 20;
            this.btnGUI_GY.Text = "Gửi";
            this.btnGUI_GY.UseVisualStyleBackColor = false;
            this.btnGUI_GY.Click += new System.EventHandler(this.btnGUI_GY_Click);
            // 
            // txtnd
            // 
            this.txtnd.BackColor = System.Drawing.SystemColors.Info;
            this.txtnd.Location = new System.Drawing.Point(31, 60);
            this.txtnd.Margin = new System.Windows.Forms.Padding(2);
            this.txtnd.Multiline = true;
            this.txtnd.Name = "txtnd";
            this.txtnd.Size = new System.Drawing.Size(263, 105);
            this.txtnd.TabIndex = 19;
            // 
            // txttn
            // 
            this.txttn.Controls.Add(this.rdbkhl);
            this.txttn.Controls.Add(this.rdbhl1);
            this.txttn.Controls.Add(this.rdbhl);
            this.txttn.Location = new System.Drawing.Point(31, 169);
            this.txttn.Margin = new System.Windows.Forms.Padding(2);
            this.txttn.Name = "txttn";
            this.txttn.Padding = new System.Windows.Forms.Padding(2);
            this.txttn.Size = new System.Drawing.Size(263, 152);
            this.txttn.TabIndex = 18;
            this.txttn.TabStop = false;
            this.txttn.Text = "Mức độ trải nghiệm";
            // 
            // rdbkhl
            // 
            this.rdbkhl.AutoSize = true;
            this.rdbkhl.Location = new System.Drawing.Point(27, 107);
            this.rdbkhl.Margin = new System.Windows.Forms.Padding(2);
            this.rdbkhl.Name = "rdbkhl";
            this.rdbkhl.Size = new System.Drawing.Size(96, 17);
            this.rdbkhl.TabIndex = 2;
            this.rdbkhl.TabStop = true;
            this.rdbkhl.Text = "Không hài lòng";
            this.rdbkhl.UseVisualStyleBackColor = true;
            // 
            // rdbhl1
            // 
            this.rdbhl1.AutoSize = true;
            this.rdbhl1.Location = new System.Drawing.Point(27, 70);
            this.rdbhl1.Margin = new System.Windows.Forms.Padding(2);
            this.rdbhl1.Name = "rdbhl1";
            this.rdbhl1.Size = new System.Drawing.Size(111, 17);
            this.rdbhl1.TabIndex = 1;
            this.rdbhl1.TabStop = true;
            this.rdbhl1.Text = "Hài lòng một phần";
            this.rdbhl1.UseVisualStyleBackColor = true;
            // 
            // rdbhl
            // 
            this.rdbhl.AutoSize = true;
            this.rdbhl.Location = new System.Drawing.Point(27, 33);
            this.rdbhl.Margin = new System.Windows.Forms.Padding(2);
            this.rdbhl.Name = "rdbhl";
            this.rdbhl.Size = new System.Drawing.Size(64, 17);
            this.rdbhl.TabIndex = 0;
            this.rdbhl.TabStop = true;
            this.rdbhl.Text = "Hài lòng";
            this.rdbhl.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 35);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Nội dung góp ý";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Bạn muốn góp ý cho phần :";
            // 
            // txtphangopy
            // 
            this.txtphangopy.Location = new System.Drawing.Point(171, 6);
            this.txtphangopy.Name = "txtphangopy";
            this.txtphangopy.Size = new System.Drawing.Size(123, 20);
            this.txtphangopy.TabIndex = 21;
            // 
            // GopY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(320, 386);
            this.Controls.Add(this.txtphangopy);
            this.Controls.Add(this.btnGUI_GY);
            this.Controls.Add(this.txtnd);
            this.Controls.Add(this.txttn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "GopY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GopY";
            this.txttn.ResumeLayout(false);
            this.txttn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGUI_GY;
        private System.Windows.Forms.TextBox txtnd;
        private System.Windows.Forms.GroupBox txttn;
        private System.Windows.Forms.RadioButton rdbkhl;
        private System.Windows.Forms.RadioButton rdbhl1;
        private System.Windows.Forms.RadioButton rdbhl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtphangopy;
    }
}