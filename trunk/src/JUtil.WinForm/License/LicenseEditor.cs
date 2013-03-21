using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JUtil;
using JUtil.WinForm.Extensions;

namespace JUtil.WinForm
{
    public partial class LicenseEditor : Form
    {
        enum EditorMode
        {
            Insert,
            Edit
        }
        private EditorMode mode;

        public LicenseEditor()
        {
            InitializeComponent();

            initUI(null);
        }

        public LicenseEditor(License license)
        {
            InitializeComponent();

            initUI(license);
        }

        private void initSoftwareName()
        {
            List<ExtComboBox.ListControlItem> list = new List<ExtComboBox.ListControlItem>();
            foreach(string softwareName in Product.GetSoftwareNameList())
                list.Add(new ExtComboBox.ListControlItem{Text = softwareName, Value = softwareName});

            comboBox_SoftwareName.InitComboBox(list);
        }

        private void initUI(License license)
        {
            mode = license == null ? EditorMode.Insert : EditorMode.Edit;

            initSoftwareName();

            switch(mode)
            {
                case EditorMode.Insert:
                    {
                        comboBox_SoftwareName.Enabled = true;
                        tbSerialNumber.ReadOnly = true;
                        tbCpuId.ReadOnly = true;

                        Comment = string.Empty;
                        SoftwareName = Product.GetDefaultSoftwareName();
                        CpuId = string.Empty;
                    }
                    break;

                case EditorMode.Edit:
                    {
                        comboBox_SoftwareName.Enabled = false;
                        tbSerialNumber.ReadOnly = true;
                        tbCpuId.ReadOnly = false;

                        Comment = license.comment;
                        SoftwareName = license.softwareName;
                        SerialNumber = license.serialNumber;
                        CpuId = license.cpuId;
                    }
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CheckInput();

                License license = GetLicense();

                switch (mode)
                {
                    case EditorMode.Insert:
                        {
                            LicenseManagement.Add(license);
                        }
                        break;

                    case EditorMode.Edit:
                        {
                            LicenseManagement.Update(license);
                        }
                        break;
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception err)
            {
                Log.ReportError(err.Message, Text + "失敗");
            }
        }

        private void CheckInput()
        {
            if(Comment.Length==0)
                throw new ArgumentNullException("Comment");

            if (SoftwareName.Length == 0)
                throw new ArgumentNullException("SoftwareName");

            if (SerialNumber.Length == 0)
                throw new ArgumentNullException("SerialNumber");
        }

        private License GetLicense()
        {
            License l = new License();

            l.comment = Comment;
            l.softwareName = SoftwareName;
            l.serialNumber = SerialNumber;
            l.cpuId = CpuId;

            return l;
        }

        private void comboBox_SoftwareName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerialNumber = JUtil.SerialNumber.Generate(SoftwareName);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private string Comment
        {
            get { return tbComment.Text.Trim(); }
            set { tbComment.Text = value.Trim(); }
        }

        private string SoftwareName
        {
            get
            {
                object o = comboBox_SoftwareName.GetNonEnumValue();
                return o != null ? (string) o : string.Empty;
            }

            set
            {
                comboBox_SoftwareName.SetNonEnumValue(value);
            }
        }

        private string SerialNumber
        {
            get { return tbSerialNumber.Text.Trim(); }
            set { tbSerialNumber.Text = value.Trim(); }
        }

        private string CpuId
        {
            get { return tbCpuId.Text.Trim(); }
            set { tbCpuId.Text = value.Trim(); }
        }

        
    }
}
