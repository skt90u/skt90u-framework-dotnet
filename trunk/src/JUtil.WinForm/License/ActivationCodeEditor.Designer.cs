namespace JUtil.WinForm
{
    partial class ActivationCodeEditor
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
            this.titleActivationCode = new System.Windows.Forms.Label();
            this.tbActivationCode = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.line = new System.Windows.Forms.GroupBox();
            this.gb = new System.Windows.Forms.GroupBox();
            this.titleSoftName = new System.Windows.Forms.Label();
            this.titleCertificationCode = new System.Windows.Forms.Label();
            this.lbContact = new System.Windows.Forms.Label();
            this.titlePhone = new System.Windows.Forms.Label();
            this.lbPhone = new System.Windows.Forms.Label();
            this.tbSoftwareName = new System.Windows.Forms.TextBox();
            this.tbCertificationCode = new System.Windows.Forms.TextBox();
            this.gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleActivationCode
            // 
            this.titleActivationCode.AutoSize = true;
            this.titleActivationCode.Location = new System.Drawing.Point(22, 194);
            this.titleActivationCode.Name = "titleActivationCode";
            this.titleActivationCode.Size = new System.Drawing.Size(77, 12);
            this.titleActivationCode.TabIndex = 4;
            this.titleActivationCode.Text = "請輸入啟用碼";
            // 
            // tbActivationCode
            // 
            this.tbActivationCode.Location = new System.Drawing.Point(21, 219);
            this.tbActivationCode.Name = "tbActivationCode";
            this.tbActivationCode.Size = new System.Drawing.Size(361, 22);
            this.tbActivationCode.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(224, 276);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(119, 276);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 43;
            this.btnOK.Text = "確定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // line
            // 
            this.line.Location = new System.Drawing.Point(12, 257);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(386, 2);
            this.line.TabIndex = 42;
            this.line.TabStop = false;
            // 
            // gb
            // 
            this.gb.Controls.Add(this.tbCertificationCode);
            this.gb.Controls.Add(this.tbSoftwareName);
            this.gb.Controls.Add(this.titleSoftName);
            this.gb.Controls.Add(this.titleCertificationCode);
            this.gb.Location = new System.Drawing.Point(16, 79);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(366, 100);
            this.gb.TabIndex = 45;
            this.gb.TabStop = false;
            this.gb.Text = "產品啟用資訊";
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
            // lbContact
            // 
            this.lbContact.AutoSize = true;
            this.lbContact.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbContact.Location = new System.Drawing.Point(22, 18);
            this.lbContact.Name = "lbContact";
            this.lbContact.Size = new System.Drawing.Size(304, 12);
            this.lbContact.TabIndex = 46;
            this.lbContact.Text = "請撥打以下電話，並告知產品啟用資訊，以啟用產品";
            // 
            // titlePhone
            // 
            this.titlePhone.AutoSize = true;
            this.titlePhone.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.titlePhone.Location = new System.Drawing.Point(21, 44);
            this.titlePhone.Name = "titlePhone";
            this.titlePhone.Size = new System.Drawing.Size(57, 12);
            this.titlePhone.TabIndex = 47;
            this.titlePhone.Text = "連絡電話";
            // 
            // lbPhone
            // 
            this.lbPhone.AutoSize = true;
            this.lbPhone.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbPhone.ForeColor = System.Drawing.Color.Red;
            this.lbPhone.Location = new System.Drawing.Point(82, 45);
            this.lbPhone.Name = "lbPhone";
            this.lbPhone.Size = new System.Drawing.Size(85, 12);
            this.lbPhone.TabIndex = 48;
            this.lbPhone.Text = "0921-859-698";
            // 
            // tbSoftwareName
            // 
            this.tbSoftwareName.Location = new System.Drawing.Point(94, 25);
            this.tbSoftwareName.Name = "tbSoftwareName";
            this.tbSoftwareName.Size = new System.Drawing.Size(256, 22);
            this.tbSoftwareName.TabIndex = 50;
            // 
            // tbCertificationCode
            // 
            this.tbCertificationCode.Location = new System.Drawing.Point(94, 65);
            this.tbCertificationCode.Name = "tbCertificationCode";
            this.tbCertificationCode.Size = new System.Drawing.Size(256, 22);
            this.tbCertificationCode.TabIndex = 51;
            // 
            // ActivationCodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 315);
            this.Controls.Add(this.lbPhone);
            this.Controls.Add(this.titlePhone);
            this.Controls.Add(this.lbContact);
            this.Controls.Add(this.gb);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.line);
            this.Controls.Add(this.tbActivationCode);
            this.Controls.Add(this.titleActivationCode);
            this.Name = "ActivationCodeEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "產品金鑰啟用";
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleActivationCode;
        private System.Windows.Forms.TextBox tbActivationCode;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox line;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.Label titleSoftName;
        private System.Windows.Forms.Label titleCertificationCode;
        private System.Windows.Forms.Label lbContact;
        private System.Windows.Forms.Label titlePhone;
        private System.Windows.Forms.Label lbPhone;
        private System.Windows.Forms.TextBox tbCertificationCode;
        private System.Windows.Forms.TextBox tbSoftwareName;
    }
}