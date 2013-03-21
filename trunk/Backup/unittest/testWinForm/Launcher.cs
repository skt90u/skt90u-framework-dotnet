using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testWinForm
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            InitializeComponent();
        }

        private void btnBAD_Click(object sender, EventArgs e)
        {
            BackgroundWorkerBadExample form = new BackgroundWorkerBadExample();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);
        }

        private void btnGOOD_Click(object sender, EventArgs e)
        {

        }

        private void btnShowProgress_Click(object sender, EventArgs e)
        {

        }

    }
}
