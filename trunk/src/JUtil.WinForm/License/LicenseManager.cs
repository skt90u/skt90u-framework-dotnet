using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JUtil;

namespace JUtil.WinForm
{
    public partial class LicenseManager : Form
    {


        public LicenseManager()
        {
            try
            {
                BackupLicense();

                InitializeComponent();

                InitListView();

                RefreshListItems();
            }
            catch (Exception e)
            {
                Log.ReportError(e.Message, "LicenseManager");
            }
        }

        protected void InitListView()
        {
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listView.MultiSelect = true;
            listView.View = System.Windows.Forms.View.Details;

            listView.Columns.Add("", 30);
            listView.Columns.Add("COMMENT", 200);
            listView.Columns.Add("SOFTWARENAME", 150);
            listView.Columns.Add("SERIALNUMBER", 150);
            listView.Columns.Add("CPUID", 200);
        }

        public void RefreshListItems()
        {
            try
            {
                listView.Items.Clear();

                List<License> list = LicenseManagement.Select();

                for (int i = 0; i < list.Count; i++)
                {
                    License l = list[i];

                    ListViewItem listViewItem = new ListViewItem(new string[]
                    {
                        String.Format("{0:00}", i + 1),
                        l.comment,
                        l.softwareName,
                        l.serialNumber,
                        l.cpuId
                    });

                    listViewItem.Tag = l;

                    listView.Items.Add(listViewItem);
                }
            }
            catch (Exception e)
            {
                Log.ReportError(e.Message, "無法更新資料");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LicenseEditor editor = new LicenseEditor();

            if (DialogResult.OK == editor.ShowDialog())
                RefreshListItems();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(SelectedIndex != -1)
            {
                LicenseEditor editor = new LicenseEditor(SelectedItemTag as License);

                if (DialogResult.OK == editor.ShowDialog())
                    RefreshListItems();    
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (SelectedItemTags.Count != 0)
            {
                if (MessageBox.Show("是否刪除資料?", "系統提示", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                foreach(License license in SelectedItemTags)
                    LicenseManagement.Delete(license);

                RefreshListItems();
            }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            if (SelectedItemTags.Count != 0)
            {
                LicenseSender dlg = new LicenseSender(SelectedItemTags);
                dlg.ShowDialog();
            }
        }

        protected int SelectedIndex
        {
            get { return listView.SelectedIndices.Count == 0 ? -1 : listView.SelectedIndices[0]; }
            set
            {
                if (value != -1)
                {
                    listView.Items[value].Selected = true;

                    //listView.Items[value].BackColor = SystemColors.Highlight;
                    //listView.Items[value].ForeColor = Color.White;

                    // http://blog.csdn.net/minsenwu/article/details/7386936
                    //listView.FocusedItem = listView.Items[value];
                    //listView.FocusedItem.BackColor = SystemColors.Highlight;
                    //listView.FocusedItem.ForeColor = Color.White;
                }
            }
        }

        protected object SelectedItemTag
        {
            get
            {
                int index = SelectedIndex;
                return index == -1 ? null : listView.Items[index].Tag;
            }
        }

        protected List<License> SelectedItemTags
        {
            get
            {
                List<License> list = new List<License>();

                ListView.SelectedListViewItemCollection SelectedItems = listView.SelectedItems;

                foreach (ListViewItem item in SelectedItems)
                {
                    list.Add(item.Tag as License);
                }

                return list;
            }
        }

        private void BackupLicense()
        {
            string fileName = JUtil.Path.File.GetFileName(LicenseManagement.DbPath);

            string backupName = string.Format("{0}-{1}", DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss"), fileName);

            string backupDirectory = @"C:\LicenseDbBackup\";
            JUtil.Path.Directory.MakeSureExists(backupDirectory);
            string backupPath = JUtil.Path.File.GetAbsolutePath(backupDirectory, backupName);

            JUtil.Path.File.Copy(LicenseManagement.DbPath, backupPath);
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            ActivationCodeGenerator dlg = new ActivationCodeGenerator();
            
            dlg.ShowDialog();

            RefreshListItems();
        }
    }
}
