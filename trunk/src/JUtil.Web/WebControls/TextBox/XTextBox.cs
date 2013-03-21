using System;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// TextBox with RequiredValidator
    /// </summary>
    public class XTextBox : CompositeControl, IControlAccessor
    {
        #region PublicMembers
        
        #region MaxLength
        /// <summary>
        /// 設定輸入最大長度, 當有使用RangeValidator時, 且NumType為整數
        /// 會自動設定合適的最大長度
        /// </summary>
        public int MaxLength
        {
            get
            {
                object o = ViewState["MaxLength"];
                if (o == null)
                {
                    o = 0;
                }
                return Convert.ToInt32(o);
            }
            set { ViewState["MaxLength"] = value; }
        }
        #endregion

        #region Text
        /// <summary>與實際的textbox界接</summary>
        public string Text
        {
            get
            {
                EnsureChildControls();
                return tb.Text;
            }
            set
            {
                EnsureChildControls();
                tb.Text = value;
            }
        }
        #endregion

        #region ReadOnly
        /// <summary>
        /// is ReadOnly TextBox or not
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                object o = ViewState["ReadOnly"];
                if (o == null)
                {
                    o = false;
                }
                return Convert.ToBoolean(o);
            }
            set { ViewState["ReadOnly"] = value; }
        }
        #endregion

        #region Required
        /// <summary>是否為必填欄位</summary>
        public bool Required
        {
            get
            {
                object o = ViewState["Required"];
                if (o == null)
                {
                    o = false;
                }
                return Convert.ToBoolean(o);
            }
            set { ViewState["Required"] = value; }
        }
        #endregion

        #region ValidationGroup
        /// <summary>指定Validator為哪一個要驗證的群組</summary>
        public string ValidationGroup
        {
            get
            {
                object o = ViewState["ValidationGroup"];
                if (o == null)
                {
                    o = string.Empty;
                }
                return Convert.ToString(o);
            }
            set { ViewState["ValidationGroup"] = value; }
        }
        #endregion

        #region RequiredErrorMessage
        /// <summary>RequiredErrorMessage</summary>
        public string RequiredErrorMessage
        {
            get
            {
                object o = ViewState["RequiredErrorMessage"];
                if (o == null)
                {
                    o = "此為必填欄位";
                }
                return Convert.ToString(o);
            }
            set { ViewState["RequiredErrorMessage"] = value; }
        }
        #endregion

        #endregion

        #region PrivateMembers

        private TextBox tb = new TextBox();

        #endregion

        #region Event Handlers
        #region OnLoad
        /// <summary></summary>
        /// <remarks>TODO: Page.IsPostBack 是否足夠判斷在UpdatePanel之內的控件</remarks>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            if (Page.IsPostBack)
            {
                EnsureChildControls();
            }
        }
        #endregion

        #region CreateChildControls
        /// <summary>定義複合控件架構</summary>
        protected override void CreateChildControls()
        {
            tb.ID = "tb";
            tb.MaxLength = MaxLength;
            if (ReadOnly)
                tb.Attributes["readonly"] = "readonly";
            Controls.Add(tb);

            // 如果有設定Required為True, 就加入RequiredFieldValidator
            if (Required)
            {
                RequiredFieldValidator requiredV = new RequiredFieldValidator();
                requiredV.ID = "requiredV";
                requiredV.ControlToValidate = tb.ID;
                requiredV.Display = ValidatorDisplay.Dynamic;
                requiredV.ValidationGroup = ValidationGroup;
                requiredV.ErrorMessage = RequiredErrorMessage;
                Controls.Add(requiredV);
            }
        }
        #endregion
        #endregion

        #region IControlAccessor 成員

        /// <summary>
        /// tell if whether NumEdit
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public bool IsMatchType(System.Web.UI.Control ctl)
        {
            bool match = ctl is JUtil.Web.WebControls.XTextBox ||
                         ctl.GetType().IsSubclassOf(typeof(JUtil.Web.WebControls.XTextBox));
            return match;
        }

        /// <summary>
        /// get NumEdit value
        /// </summary>
        public object GetValue(System.Web.UI.Control ctl)
        {
            JUtil.Web.WebControls.XTextBox ne = (JUtil.Web.WebControls.XTextBox)ctl;
            return ne.Text;
        }

        /// <summary>
        /// set NumEdit value
        /// </summary>
        public void SetValue(System.Web.UI.Control ctl, object value)
        {
            JUtil.Web.WebControls.XTextBox ne = (JUtil.Web.WebControls.XTextBox)ctl;
            ne.Text = value.ToString();
        }

        #endregion
    }
}
