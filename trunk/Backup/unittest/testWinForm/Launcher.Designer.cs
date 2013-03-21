namespace testWinForm
{
    partial class Launcher
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBAD = new System.Windows.Forms.Button();
            this.btnGOOD = new System.Windows.Forms.Button();
            this.btnShowProgress = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBAD
            // 
            this.btnBAD.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBAD.Location = new System.Drawing.Point(0, 0);
            this.btnBAD.Name = "btnBAD";
            this.btnBAD.Size = new System.Drawing.Size(292, 23);
            this.btnBAD.TabIndex = 0;
            this.btnBAD.Text = "Cross Thread BAD Example";
            this.btnBAD.UseVisualStyleBackColor = true;
            this.btnBAD.Click += new System.EventHandler(this.btnBAD_Click);
            // 
            // btnGOOD
            // 
            this.btnGOOD.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGOOD.Location = new System.Drawing.Point(0, 23);
            this.btnGOOD.Name = "btnGOOD";
            this.btnGOOD.Size = new System.Drawing.Size(292, 23);
            this.btnGOOD.TabIndex = 1;
            this.btnGOOD.Text = "Handle Cross Thread GOOD Example";
            this.btnGOOD.UseVisualStyleBackColor = true;
            this.btnGOOD.Click += new System.EventHandler(this.btnGOOD_Click);
            // 
            // btnShowProgress
            // 
            this.btnShowProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnShowProgress.Location = new System.Drawing.Point(0, 46);
            this.btnShowProgress.Name = "btnShowProgress";
            this.btnShowProgress.Size = new System.Drawing.Size(292, 23);
            this.btnShowProgress.TabIndex = 2;
            this.btnShowProgress.Text = "Showing Progress";
            this.btnShowProgress.UseVisualStyleBackColor = true;
            this.btnShowProgress.Click += new System.EventHandler(this.btnShowProgress_Click);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.btnShowProgress);
            this.Controls.Add(this.btnGOOD);
            this.Controls.Add(this.btnBAD);
            this.Name = "Launcher";
            this.Text = "Launcher";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBAD;
        private System.Windows.Forms.Button btnGOOD;
        private System.Windows.Forms.Button btnShowProgress;
    }
}

