namespace JUtil.WinForm
{
    partial class ActivationCodeGenerator
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
            this.tbActivationCode = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.line = new System.Windows.Forms.GroupBox();
            this.gb = new System.Windows.Forms.GroupBox();
            this.tbCertificationCode = new System.Windows.Forms.TextBox();
            this.comboBox_SoftwareName = new System.Windows.Forms.ComboBox();
            this.titleSoftName = new System.Windows.Forms.Label();
            this.titleCertificationCode = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbActivationCode
            // 
            this.tbActivationCode.Location = new System.Drawing.Point(16, 128);
            this.tbActivationCode.Name = "tbActivationCode";
            this.tbActivationCode.Size = new System.Drawing.Size(361, 22);
            this.tbActivationCode.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(114, 187);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 43;
            this.btnOK.Text = "產製(&C)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // line
            // 
            this.line.Location = new System.Drawing.Point(9, 168);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(386, 2);
            this.line.TabIndex = 42;
            this.line.TabStop = false;
            // 
            // gb
            // 
            this.gb.Controls.Add(this.tbCertificationCode);
            this.gb.Controls.Add(this.comboBox_SoftwareName);
            this.gb.Controls.Add(this.titleSoftName);
            this.gb.Controls.Add(this.titleCertificationCode);
            this.gb.Location = new System.Drawing.Point(13, 11);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(366, 100);
            this.gb.TabIndex = 45;
            this.gb.TabStop = false;
            this.gb.Text = "產品啟用資訊";
            // 
            // tbCertificationCode
            // 
            this.tbCertificationCode.Location = new System.Drawing.Point(95, 68);
            this.tbCertificationCode.Name = "tbCertificationCode";
            this.tbCertificationCode.Size = new System.Drawing.Size(263, 22);
            this.tbCertificationCode.TabIndex = 51;
            // 
            // comboBox_SoftwareName
            // 
            this.comboBox_SoftwareName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SoftwareName.FormattingEnabled = true;
            this.comboBox_SoftwareName.Location = new System.Drawing.Point(95, 27);
            this.comboBox_SoftwareName.Name = "comboBox_SoftwareName";
            this.comboBox_SoftwareName.Size = new System.Drawing.Size(263, 20);
            this.comboBox_SoftwareName.TabIndex = 50;
            // 
            // titleSoftName
            // 
            this.titleSoftName.AutoSize = true;
            this.titleSoftName.Location = new System.Drawing.Point(22, 30);
            this.titleSoftName.Name = "titleSoftName";
            this.titleSoftName.Size = new System.Drawing.Size(53, 12);
            this.titleSoftName.TabIndex = 46;
            this.titleSoftName.Text = "產品名稱";
            // 
            // titleCertificationCode
            // 
            this.titleCertificationCode.AutoSize = true;
            this.titleCertificationCode.Location = new System.Drawing.Point(22, 72);
            this.titleCertificationCode.Name = "titleCertificationCode";
            this.titleCertificationCode.Size = new System.Drawing.Size(41, 12);
            this.titleCertificationCode.TabIndex = 48;
            this.titleCertificationCode.Text = "驗證碼";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(223, 187);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 46;
            this.btnExit.Text = "離開(&E)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ActivationCodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 222);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.gb);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.line);
            this.Controls.Add(this.tbActivationCode);
            this.Name = "ActivationCodeGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "啟用碼產生器";
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbActivationCode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox line;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.Label titleSoftName;
        private System.Windows.Forms.Label titleCertificationCode;
        private System.Windows.Forms.TextBox tbCertificationCode;
        private System.Windows.Forms.ComboBox comboBox_SoftwareName;
        private System.Windows.Forms.Button btnExit;
    }
}