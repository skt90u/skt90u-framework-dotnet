using System;
using System.Drawing;
using System.Windows.Forms;

namespace JUtil.WinForm
{
    class XDataGridView : DataGridView
    {
        int TotalCheckBoxes = 0;
        int TotalCheckedCheckBoxes = 0;
        CheckBox HeaderCheckBox = null;
        bool IsHeaderCheckBoxClicked = false;

        public XDataGridView()
        {
            AddHeaderCheckBox();
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            // reset after accept a new data source
            TotalCheckBoxes = RowCount;
            TotalCheckedCheckBoxes = 0;

            base.OnDataSourceChanged(e);
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            // reset after "dynamically" add a new row 
            TotalCheckBoxes = RowCount;
            TotalCheckedCheckBoxes = 0;

            base.OnRowsAdded(e);
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            TotalCheckBoxes = RowCount;
            TotalCheckedCheckBoxes = 0;
            foreach (DataGridViewRow row in Rows)
            {
                DataGridViewCheckBoxCell checkBox = (DataGridViewCheckBoxCell)row.Cells[0];
                if ((bool)(checkBox.Value) == true)
                {
                    TotalCheckedCheckBoxes++;
                }
            }

            HeaderCheckBox.Checked = (TotalCheckBoxes > 0) ? (TotalCheckBoxes == TotalCheckedCheckBoxes) : false;

            base.OnRowsRemoved(e);
        }

        private void AddHeaderCheckBox()
        {
            HeaderCheckBox = new CheckBox();

            HeaderCheckBox.Size = new Size(15, 15);

            //Add the CheckBox into the DataGridView
            Controls.Add(HeaderCheckBox);

            HeaderCheckBox.KeyUp += new KeyEventHandler(HeaderCheckBox_KeyUp);
            HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);

            base.OnCellPainting(e);
        }

        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            HeaderCheckBox.Location = oPoint;
        }

        void HeaderCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                HeaderCheckBoxClick((CheckBox)sender);
        }

        void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);
        }

        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            IsHeaderCheckBoxClicked = true;

            foreach (DataGridViewRow Row in Rows)
                ((DataGridViewCheckBoxCell)Row.Cells["chkBxSelect"]).Value = HCheckBox.Checked;

            RefreshEdit();

            TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;

            IsHeaderCheckBoxClicked = false;
        }

        protected override void OnCurrentCellDirtyStateChanged(EventArgs e)
        {
            if (CurrentCell is DataGridViewCheckBoxCell)
                CommitEdit(DataGridViewDataErrorContexts.Commit);

            base.OnCurrentCellDirtyStateChanged(e);
        }
        
        protected override void OnCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            
            if (!IsHeaderCheckBoxClicked)
            {
                DataGridView dgv = this;

                // if you override any native event of a control,
                // you must take care any situation that happen after
                // InitializeComponent function of Form happen will
                // change control state.
                // 
                // in this case, e.RowIndex will be -1 after InitializeComponent finish
                // if you call dgv[e.ColumnIndex, e.RowIndex]
                // you'll get a OutOfRangeException
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                    RowCheckBoxClick((DataGridViewCheckBoxCell)dgv[e.ColumnIndex, e.RowIndex]);
            }

            base.OnCellValueChanged(e);
        }

        

        private void RowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxes < TotalCheckBoxes)
                    TotalCheckedCheckBoxes++;
                else if (TotalCheckedCheckBoxes > 0)
                    TotalCheckedCheckBoxes--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxes < TotalCheckBoxes)
                    HeaderCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxes == TotalCheckBoxes)
                    HeaderCheckBox.Checked = true;
            }
        }


    } // end of XDataGridView
}
