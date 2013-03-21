using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// Extension TextArea
    /// </summary>
    public class TextArea : System.Web.UI.WebControls.TextBox
    {
        /// <summary>
        /// OnPreRender Event
        /// </summary>
        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);

            TextMode = TextBoxMode.MultiLine;

            Type type = this.GetType();

            ClientScriptManager.RegisterEmbeddedCSS(type, "JUtil.Web.WebControls.TextBox.XTextArea.jquery.textareaCounter.css");

            ClientScriptManager.RegisterEmbeddedJs(type, "JUtil.Web.WebControls.jQuery.jquery.1.4.4.js");

            List<string> lstWebResource = new List<string>();
            lstWebResource.Add("JUtil.Web.WebControls.TextBox.XTextArea.jquery.textareaCounter.js");
            lstWebResource.Add("JUtil.Web.WebControls.TextBox.XTextArea.jquery.elastic.source.js");
            ClientScriptManager.RegisterCompositeScript(lstWebResource);

            string scripts = GenApplicationLoadScript();
            ClientScriptManager.RegisterClientApplicationLoadScript(this, scripts);
        }

        private string GenApplicationLoadScript()
        {
            StringBuilder scripts = new StringBuilder();
            // --------------------------------------------------
            // textareaCounter
            // --------------------------------------------------
            scripts.Append("var options = {");
            scripts.Append("'maxCharacterSize':  " + maxCharacterSize + ",");
            scripts.Append("'originalStyle'   : '" + originalStyle + "',");
            scripts.Append("'warningStyle'    : '" + warningStyle + "',");
            scripts.Append("'warningNumber'   :  " + warningNumber + ",");
            scripts.Append("'displayFormat'   : '" + displayFormat + "'");
            scripts.Append("};");
            scripts.Append("$('#" + ClientID + "').textareaCount(options);");
            // --------------------------------------------------
            // elastic
            // --------------------------------------------------
            scripts.Append("$('#" + ClientID + "').elastic();");
            return scripts.ToString();
        }

        #region "displayFormat"
        /// <summary>
        /// display format
        /// </summary>
        public string displayFormat
        {
            get { return GetPropertyValue<string>("displayFormat", "目前字數: #input, 剩餘字數: #left, 容許字數: #max"); }
            set { SetPropertyValue<string>("displayFormat", value); }
        }
        #endregion
        #region "maxCharacterSize"
        private string maxCharacterSize
        {
            get
            {
                if (MaxLength.Equals(0))
                {
                    return "-1";
                }
                return MaxLength.ToString();
            }
        }
        #endregion
        #region "warningNumber"
        /// <summary>
        /// warning number
        /// </summary>
        public string warningNumber
        {
            get { return GetPropertyValue<string>("warningNumber", "0"); }
            set { SetPropertyValue<string>("warningNumber", value); }
        }
        #endregion
        #region "warningStyle"
        /// <summary>
        /// warning style
        /// </summary>
        public string warningStyle
        {
            get { return GetPropertyValue<string>("warningStyle", "warningTextareaInfo"); }
            set { SetPropertyValue<string>("warningStyle", value); }
        }
        #endregion
        #region "originalStyle"
        /// <summary>
        /// original style
        /// </summary>
        public string originalStyle
        {
            get { return GetPropertyValue<string>("originalStyle", "originalTextareaInfo"); }
            set { SetPropertyValue<string>("originalStyle", value); }
        }
        #endregion

        /// <summary>
        /// get property
        /// </summary>
        protected V GetPropertyValue<V>(string propertyName, V nullValue)
        {
            object propertyValue = ViewState[propertyName];
            if (propertyValue == null)
            {
                return nullValue;
            }
            return (V)propertyValue;
        }

        /// <summary>
        /// set property
        /// </summary>
        protected void SetPropertyValue<V>(string propertyName, V value)
        {
            ViewState[propertyName] = value;
        }
    }
}
