using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JUtil.WinForm.Extensions;

namespace JUtil.WinForm
{
    public partial class ActivationCodeGenerator : Form
    {
        public ActivationCodeGenerator()
        {
            InitializeComponent();

            initSoftwareName();

            SoftwareName = Product.GetDefaultSoftwareName();

            tbActivationCode.ReadOnly = true;

            tbCertificationCode.KeyPress += new KeyPressEventHandler(OnKeyPress);
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) ||
                Char.IsDigit(e.KeyChar) ||
                (e.KeyChar == '\b') ||
                (e.KeyChar == '-') ||
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

        private void initSoftwareName()
        {
            List<ExtComboBox.ListControlItem> list = new List<ExtComboBox.ListControlItem>();
            foreach (string softwareName in Product.GetSoftwareNameList())
                list.Add(new ExtComboBox.ListControlItem { Text = softwareName, Value = softwareName });

            comboBox_SoftwareName.InitComboBox(list);
        }

        private string SoftwareName
        {
            get
            {
                object o = comboBox_SoftwareName.GetNonEnumValue();
                return o != null ? (string)o : string.Empty;
            }

            set
            {
                comboBox_SoftwareName.SetNonEnumValue(value);
            }
        }

        private string CertificationCodeInput
        {
            get { return tbCertificationCode.Text.Trim(); }
            set { tbCertificationCode.Text = value.Trim(); }
        }

        private string ActivationCodeInput
        {
            get { return tbActivationCode.Text.Trim(); }
            set { tbActivationCode.Text = value.Trim(); }
        }

        private void CheckInput()
        {
            if (SoftwareName.Length == 0)
                throw new ArgumentNullException("SoftwareName");

            if (CertificationCodeInput.Length == 0)
                throw new ArgumentNullException("CertificationCode");

            // 伺服器判斷驗證碼是否正確
            CertificationCode.Check(SoftwareName, CertificationCodeInput);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CheckInput();

                ActivationCodeInput = ActivationCode.Generate(SoftwareName, CertificationCodeInput);
            }
            catch (Exception err)
            {
                Log.ReportError(err.Message, Text + "失敗");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
