using System;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// Number only textbox
    /// </summary>
    public class NumEdit : CompositeControl, IControlAccessor
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

                if (UsingRangeValidator && NumType.Equals(eNumType.Integer))
                {
                    return MaximumValue.Length;
                }
                else
                {
                    return Convert.ToInt32(o);
                }
            }
            set { ViewState["MaxLength"] = value; }
        }
        #endregion

        #region MaximumValue
        /// <summary>定義RangeValidator的MaximumValue</summary>
        public string MaximumValue
        {
            get
            {
                object o = ViewState["MaximumValue"];
                if (o == null)
                {
                    o = string.Empty;
                }
                return Convert.ToString(o);
            }
            set { ViewState["MaximumValue"] = value; }
        }
        #endregion

        #region MinimumValue
        /// <summary>定義RangeValidator的MinimumValue</summary>
        public string MinimumValue
        {
            get
            {
                object o = ViewState["MinimumValue"];
                if (o == null)
                {
                    o = string.Empty;
                }
                return Convert.ToString(o);
            }
            set { ViewState["MinimumValue"] = value; }
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

        #region NumType
        /// <summary>取得或設定目前的NumEdit所要使用的類型</summary>
        public eNumType NumType
        {
            get
            {
                object o = ViewState["NumType"];
                if (o == null)
                {
                    o = DefNumType;
                }
                return (eNumType)o;
            }
            set { ViewState["NumType"] = value; }
        }
        #endregion
        #endregion

        #region PrivateMembers

        /// <summary>定義目前NumEdit支援的類型</summary>
        public enum eNumType
        {
            /// <summary>
            /// 整數
            /// </summary>
            Integer = 0,

            /// <summary>
            /// 浮點數
            /// </summary>
            Double
        }

        /// <summary>預設為整數</summary>
        private const eNumType DefNumType = eNumType.Integer;

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

        #region Tip
        /// <summary>當滑鼠mouseover到textbox時,顯示的ToolTip</summary>
        private string Tip
        {
            get
            {
                string message = "";
                if (Required)
                {
                    message += RequiredErrorMessage;
                }

                if (UsingRangeValidator)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        message += ",且";
                    }
                    
                    message += RangeErrorMessage;
                }
                return message;
            }
        }
        #endregion

        #region RangeErrorMessage
        /// <summary>RangeValidator錯誤時,顯示的內容</summary>
        private string RangeErrorMessage
        {
            get { return string.Format("輸入數值必須在{0}到{1}之間", MinimumValue, MaximumValue); }
        }
        #endregion

        #region UsingRangeValidator
        /// <summary>判斷是否使用RangeValidator,必須定義MaximumValue,以及MinimumValue</summary>
        private bool UsingRangeValidator
        {
            get { return !string.IsNullOrEmpty(MaximumValue) && !string.IsNullOrEmpty(MinimumValue); }
        }
        #endregion

        private TextBox tb = new TextBox();

        private FilteredTextBoxExtender filter = new FilteredTextBoxExtender();
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
            tb.CssClass = CssClass;
            tb.MaxLength = MaxLength;
            if (UsingRangeValidator)
            {
                tb.ToolTip = Tip;
            }
            Controls.Add(tb);

            filter.ID = "filter";
            filter.TargetControlID = tb.ID;
            switch (NumType)
            {
                case eNumType.Integer:
                    {
                        filter.FilterType = FilterTypes.Numbers;
                    }
                    break;

                case eNumType.Double:
                    {
                        // 設定當數值型態為Double時, 必須支援的keycode
                        filter.FilterType = FilterTypes.Numbers | FilterTypes.Custom;
                        filter.FilterMode = FilterModes.ValidChars;
                        filter.ValidChars = ".";
                    }
                    break;
            }
            Controls.Add(filter);

            // 如果有定義最大值最小值範圍, 就設定RangeValidator
            if (UsingRangeValidator)
            {
                RangeValidator rangeV = new RangeValidator();
                rangeV.ID = "rgV";
                rangeV.ControlToValidate = tb.ID;
                rangeV.Display = ValidatorDisplay.Dynamic;
                rangeV.ValidationGroup = ValidationGroup;
                rangeV.MaximumValue = MaximumValue;
                rangeV.MinimumValue = MinimumValue;
                switch (NumType)
                {
                    case eNumType.Integer:
                        {
                            rangeV.Type = ValidationDataType.Integer;
                        }
                        break;

                    case eNumType.Double:
                        {
                            rangeV.Type = ValidationDataType.Double;
                        }
                        break;
                }
                rangeV.ErrorMessage = RangeErrorMessage;
                Controls.Add(rangeV);
            }

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
            bool match = ctl is NumEdit ||
                         ctl.GetType().IsSubclassOf(typeof(NumEdit));
            return match;
        }

        /// <summary>
        /// get NumEdit value
        /// </summary>
        public object GetValue(System.Web.UI.Control ctl)
        {
            NumEdit ne = (NumEdit)ctl;
            return ne.Text;
        }

        /// <summary>
        /// set NumEdit value
        /// </summary>
        public void SetValue(System.Web.UI.Control ctl, object value)
        {
            NumEdit ne = (NumEdit)ctl;
            ne.Text = value.ToString();
        }

        #endregion
    }
}
