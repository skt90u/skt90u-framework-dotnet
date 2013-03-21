using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JUtil.WinForm
{
    public partial class ActivationCodeEditor : Form
    {
        private string softwareName;
        private string encodingStr;

        public ActivationCodeEditor(string softwareName, string encodingStr)
        {
            InitializeComponent();

            this.softwareName = softwareName;
            this.encodingStr = encodingStr;

            tbSoftwareName.Text = softwareName.Trim().ToUpper();
            tbCertificationCode.Text = encodingStr;

            tbActivationCode.KeyPress += new KeyPressEventHandler(OnKeyPress);

            tbSoftwareName.ReadOnly = true;
            tbCertificationCode.ReadOnly = true;
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || 
                Char.IsDigit(e.KeyChar)  ||
                (e.KeyChar == '\b')      ||
                (e.KeyChar == '-')       ||
                Char.IsControl(e.KeyChar))
            {
                if (Char.IsLower(e.KeyChar))
                    e.KeyChar = (char)(e.KeyChar - 32);

                if (e.KeyChar == 22)
                {
                    string text = Clipboard.GetText().Trim().ToUpper();

                    TextBox tb = (sender as TextBox);

                    tb.Text = text;

                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void CheckInput()
        {
            if (ActivationCodeInput.Length == 0)
                throw new Exception("尚未輸入啟用碼");

            ActivationCode.Check(softwareName, ActivationCodeInput);
        }

        public string ActivationCodeInput
        {
            get { return tbActivationCode.Text.Trim().ToUpper(); }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CheckInput();

                DialogResult = DialogResult.OK;
            }
            catch (Exception err)
            {
                Log.ReportError(err.Message, Text + "失敗");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
