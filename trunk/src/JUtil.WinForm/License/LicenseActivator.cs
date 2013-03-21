using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JUtil;
using JUtil.Extensions;
using JUtil.LicenseProxy;

namespace JUtil.WinForm
{
    public partial class LicenseActivator : Form
    {
        public static bool Run(string softwareName)
        {
            bool pass = true;

            try
            {
                License.Check(softwareName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "系統提示", MessageBoxButtons.OK);

                try
                {
                    LicenseActivator dlg = new LicenseActivator(softwareName);

                    pass = DialogResult.OK == dlg.ShowDialog();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "系統提示", MessageBoxButtons.OK);

                    pass = false;
                }
            }

            return pass;
        }

        private string softwareName;

        public LicenseActivator(string softwareName)
        {
            InitializeComponent();

            this.textBox1.KeyPress += new KeyPressEventHandler(OnKeyPress);
            this.textBox2.KeyPress += new KeyPressEventHandler(OnKeyPress);

            this.softwareName = softwareName;
        }

        /*
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            // http://stackoverflow.com/questions/3446233/hook-on-default-paste-event-of-winforms-textbox-control

            // http://www.dreamincode.net/forums/topic/76762-blocking-right-mouse-to-disallow-pasting-into-a-textbox/
            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show("Right-click is not allowed", "No Right-click");
                return;
            }
        }
        */

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsDigit(e.KeyChar))
            {
                if (Char.IsLower(e.KeyChar))
                {
                    e.KeyChar = (char)(e.KeyChar - 32);
                }

                TextBox tb = (sender as TextBox);

                if (tb.Text.Length < 5)
                {
                    tb.Text = tb.Text + e.KeyChar;

                    tb.SelectionStart = tb.Text.Length;

                    if (tb.Text.Length == 5)
                    {
                        SendKeys.Send("{TAB}");
                    }
                }

                e.Handled = true;
            }
            else if (e.KeyChar == '\b')
            {
                
            }
            else if(Char.IsControl(e.KeyChar))
            {
                // COPY 
                if(e.KeyChar == 22)
                {
                    string licenseKey = Clipboard.GetText().Trim().ToUpper();

                    TextBox tb = (sender as TextBox);
                    if(tb.Name == textBox1.Name)
                    {
                        PasteLicenseKey(licenseKey);
                        e.Handled = true;    
                    }
                    else
                    {
                        Clipboard.SetText(licenseKey);
                    }
                }
            }
            else
            {
                e.Handled = true;    
            }
        }

        private void CheckInput()
        {
            if (textBox1.Text.Length != 5 ||
               textBox2.Text.Length != 5 )
                throw new Exception("序號必須為每五個一組，總共兩組");
        }

        private string SerialNumberInput
        {
            get
            {
                string text = textBox1.Text + "-" +
                              textBox2.Text;
                return text;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CheckInput();

                // Client Side
                // 判斷序號是否正確
                SerialNumber.Check(softwareName, SerialNumberInput);

                // Client Side
                // 如果正確，就產生驗證碼，並將驗證碼上傳至伺服器
                string encodingStr = CertificationCode.Generate(SerialNumberInput);

                string activationCode = string.Empty;

                /*
                if (LicenseClient.CanConnect)
                {
                    LicenseClient client = new LicenseClient();

                    try
                    {
                        client.StartCon();

                        activationCode = client.GetActivationCode(softwareName, encodingStr);
                    }
                    finally
                    {
                        client.EndCon();    
                    }
                }
                else
                {
                    ActivationCodeEditor dlg = new ActivationCodeEditor(softwareName, encodingStr);

                    if (DialogResult.OK == dlg.ShowDialog())
                        activationCode = dlg.ActivationCodeInput;
                }
                */

                LicenseClient client = new LicenseClient();
                try
                {
                    bool connected = true;

                    try
                    {
                        client.StartCon();
                    }
                    catch (Exception)
                    {
                        connected = false;
                    }

                    if (connected)
                    {
                        activationCode = client.GetActivationCode(softwareName, encodingStr);    
                    }
                    else
                    {
                        ActivationCodeEditor dlg = new ActivationCodeEditor(softwareName, encodingStr);

                        if (DialogResult.OK == dlg.ShowDialog())
                            activationCode = dlg.ActivationCodeInput;    
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "系統提示", MessageBoxButtons.OK);
                }
                finally
                {
                    client.EndCon();
                }

                // Client Side
                // 判斷啟用碼是否正確，如果正確，就產生License檔
                if (false == JUtil.Extensions.ExtString.StrIsNullOrEmpty(activationCode))
                {
                    ActivationCode.Check(softwareName, activationCode);

                    License.Generate(softwareName, activationCode);

                    MessageBox.Show(Text + "完成", "系統提示", MessageBoxButtons.OK);

                    DialogResult = DialogResult.OK;    
                }
            }
            catch (Exception err)
            {
                Log.ReportError(err.Message, Text + "失敗");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Text + "失敗", "系統提示", MessageBoxButtons.OK);

            DialogResult = DialogResult.Cancel;
        }

        private void PasteLicenseKey(string licenseKey)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;

            string[] arrLicenseKey = licenseKey.Split(new char[] { ' ', '-' });

            int total = arrLicenseKey.Length;

            if (total > 0) textBox1.Text = arrLicenseKey[0].Trim();
            if (total > 1) textBox2.Text = arrLicenseKey[1].Trim();
        }

        
    }
}
