namespace JUtil.WinForm
{
    partial class LicenseEditor
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
            this.lbSoftwareName = new System.Windows.Forms.Label();
            this.tbSerialNumber = new System.Windows.Forms.TextBox();
            this.lbSerialNumber = new System.Windows.Forms.Label();
            this.tbCpuId = new System.Windows.Forms.TextBox();
            this.lbCpuId = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.line = new System.Windows.Forms.GroupBox();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.lbComment = new System.Windows.Forms.Label();
            this.comboBox_SoftwareName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbSoftwareName
            // 
            this.lbSoftwareName.AutoSize = true;
            this.lbSoftwareName.Location = new System.Drawing.Point(27, 56);
            this.lbSoftwareName.Name = "lbSoftwareName";
            this.lbSoftwareName.Size = new System.Drawing.Size(73, 12);
            this.lbSoftwareName.TabIndex = 2;
            this.lbSoftwareName.Text = "SoftwareName";
            // 
            // tbSerialNumber
            // 
            this.tbSerialNumber.Location = new System.Drawing.Point(108, 89);
            this.tbSerialNumber.Name = "tbSerialNumber";
            this.tbSerialNumber.Size = new System.Drawing.Size(252, 22);
            this.tbSerialNumber.TabIndex = 5;
            // 
            // lbSerialNumber
            // 
            this.lbSerialNumber.AutoSize = true;
            this.lbSerialNumber.Location = new System.Drawing.Point(27, 94);
            this.lbSerialNumber.Name = "lbSerialNumber";
            this.lbSerialNumber.Size = new System.Drawing.Size(69, 12);
            this.lbSerialNumber.TabIndex = 4;
            this.lbSerialNumber.Text = "SerialNumber";
            // 
            // tbCpuId
            // 
            this.tbCpuId.Location = new System.Drawing.Point(108, 127);
            this.tbCpuId.Name = "tbCpuId";
            this.tbCpuId.Size = new System.Drawing.Size(252, 22);
            this.tbCpuId.TabIndex = 7;
            // 
            // lbCpuId
            // 
            this.lbCpuId.AutoSize = true;
            this.lbCpuId.Location = new System.Drawing.Point(27, 132);
            this.lbCpuId.Name = "lbCpuId";
            this.lbCpuId.Size = new System.Drawing.Size(35, 12);
            this.lbCpuId.TabIndex = 6;
            this.lbCpuId.Text = "CpuId";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(102, 188);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "確定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(220, 188);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // line
            // 
            this.line.Location = new System.Drawing.Point(29, 167);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(339, 2);
            this.line.TabIndex = 8;
            this.line.TabStop = false;
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(108, 14);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(252, 22);
            this.tbComment.TabIndex = 1;
            // 
            // lbComment
            // 
            this.lbComment.AutoSize = true;
            this.lbComment.Location = new System.Drawing.Point(27, 19);
            this.lbComment.Name = "lbComment";
            this.lbComment.Size = new System.Drawing.Size(51, 12);
            this.lbComment.TabIndex = 0;
            this.lbComment.Text = "Comment";
            // 
            // comboBox_SoftwareName
            // 
            this.comboBox_SoftwareName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SoftwareName.FormattingEnabled = true;
            this.comboBox_SoftwareName.Location = new System.Drawing.Point(110, 52);
            this.comboBox_SoftwareName.Name = "comboBox_SoftwareName";
            this.comboBox_SoftwareName.Size = new System.Drawing.Size(250, 20);
            this.comboBox_SoftwareName.TabIndex = 12;
            this.comboBox_SoftwareName.SelectedIndexChanged += new System.EventHandler(this.comboBox_SoftwareName_SelectedIndexChanged);
            // 
            // LicenseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(397, 237);
            this.Controls.Add(this.comboBox_SoftwareName);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.lbComment);
            this.Controls.Add(this.line);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbCpuId);
            this.Controls.Add(this.lbCpuId);
            this.Controls.Add(this.tbSerialNumber);
            this.Controls.Add(this.lbSerialNumber);
            this.Controls.Add(this.lbSoftwareName);
            this.Name = "LicenseEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LicenseEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSoftwareName;
        private System.Windows.Forms.TextBox tbSerialNumber;
        private System.Windows.Forms.Label lbSerialNumber;
        private System.Windows.Forms.TextBox tbCpuId;
        private System.Windows.Forms.Label lbCpuId;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox line;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label lbComment;
        private System.Windows.Forms.ComboBox comboBox_SoftwareName;
    }
}