using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// support decimal checked
    /// </summary>
    public class XCheckBox : CheckBox
    {
        /// <summary>
        /// decimal checked convertion
        /// </summary>
        public virtual decimal DecimalChecked
        {
            get
            {
                decimal value = Checked ? 1 : 0;
                return value;
            }

            set
            {
                bool bChecked = (int)value != 0;
                Checked = bChecked;
            }
        }
        
    }
}
