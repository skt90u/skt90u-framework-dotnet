using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// Extension CompositeControl
    /// </summary>
    public abstract class CompositeControlExtender
        : CompositeControl
    {
        /// <summary>
        /// Generic GetPropertyValue
        /// </summary>
        protected V GetPropertyValue<V>(string propertyName, V nullValue)
        {
            if (ViewState[propertyName] == null)
            {
                return nullValue;
            }
            return (V)ViewState[propertyName];
        }

        /// <summary>
        /// Generic SetPropertyValue
        /// </summary>
        protected void SetPropertyValue<V>(string propertyName, V value)
        {
            ViewState[propertyName] = value;
        }
    }
}
