using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using JUtil.Web.Extensions;
using JUtil.Web;
using System.Web.UI;
using MKB.TimePicker;

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// Date TextBox
    /// </summary>
    public class DateEdit : CompositeControl, IControlAccessor
    {

        /// <summary>只能依靠CalendarExtender選取日期</summary>
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

        /// <summary>
        /// 是否要使用時間選擇器
        /// </summary>
        public bool UseDateTimePicker
        {
            get
            {
                object o = ViewState["UseDateTimePicker"];
                if (o == null)
                {
                    o = false;
                }
                return Convert.ToBoolean(o);
            }
            set { ViewState["UseDateTimePicker"] = value; }
        }

        /// <summary>指定驗證的群組</summary>
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

        /// <summary>與實際的textbox界接</summary>
        public string Text
        {
            get {
                //return tb.Text; 

                string value = tb.Text;

                if ("____/__/__" == value || string.IsNullOrEmpty(value))
                    value = null;

                if (UseDateTimePicker)
                {
                    DateTime dt = DateTime.ParseExact(value, "yyyy/MM/dd", null);

                    int Hour = timeSelector.Hour;
                    if (timeSelector.AmPm == TimeSelector.AmPmSpec.PM)
                    {
                        if (Hour <= 12)
                            Hour += 12;
                        
                        if (Hour == 24)
                            Hour = 0;
                    }
                    
                    int Minute = timeSelector.Minute;
                    int Second = timeSelector.Second;

                    dt = new DateTime(dt.Year, dt.Month, dt.Day, Hour, Minute, Second);

                    value = dt.ToString("yyyy/MM/dd HH:mm:ss");
                }

                return value;
            }
            set 
            {
                if (UseDateTimePicker)
                {
                    DateTime dt = DateTime.ParseExact(value, "yyyy/MM/dd HH:mm:ss", null);

                    tb.Text = dt.ToString("yyyy/MM/dd");

                    timeSelector.AmPm = (1 <= dt.Hour && dt.Hour <= 12) ? TimeSelector.AmPmSpec.AM : TimeSelector.AmPmSpec.PM;
                    timeSelector.Hour = dt.Hour == 0 ? 12 : dt.Hour;
                    timeSelector.Minute = dt.Minute;
                    timeSelector.Second = dt.Second;
                }
                else
                {
                    tb.Text = value; 
                }
                
            }
        }

        #region "private member & property & enum"
        private const string FormatErrorMessage = "日期不合法";
        private const string RequiredErrorMessage = "請輸入日期";

        private const string DateExpression = "^(____/__/__)|((19|20|21)\\d\\d[/](0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01]))$";
        private TextBox tb = new TextBox();
        private MaskedEditExtender editMask = new MaskedEditExtender();
        private ImageButton ib = new ImageButton();
        private CalendarExtender ce = new CalendarExtender();
        #endregion
        private RegularExpressionValidator re = new RegularExpressionValidator();

        private TimeSelector timeSelector = new TimeSelector();

        #region "OnLoad"
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

        #region "OnPreRender"
        /// <remarks>在此註冊css, javascript</remarks>
        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            Type type = this.GetType();
            ClientScriptManager.RegisterEmbeddedCSS(type, "JUtil.Web.WebControls.TextBox.DateEdit.DateEdit.css");
        }
        #endregion

        #region "CreateChildControls"
        /// <summary>定義複合控件架構</summary>
        protected override void CreateChildControls()
        {
            tb.ID = "tb";
            tb.AppendClass("text");
            tb.AppendClass(CssClass);
            if (ReadOnly)
                tb.Attributes["readonly"] = "readonly";

            editMask.ID = "editMask";
            editMask.TargetControlID = tb.ID;
            editMask.Mask = "9999/99/99";
            editMask.AutoComplete = true;
            editMask.ClearMaskOnLostFocus = false;

            ib.ID = "ib";
            ib.AppendClass("image");
            ib.TabIndex = -1;
            // 必須將上此條件否則在設計模式檢視會出例外
            if (!DesignMode)
            {
                ib.ImageUrl = ClientScriptManager.GetWebResourceUrl(this.GetType(), "JUtil.Web.WebControls.TextBox.DateEdit.Calendar.png");
            }
            ib.CausesValidation = false;

            ce.ID = "ce";
            ce.TargetControlID = tb.ID;
            ce.PopupButtonID = ib.ID;
            ce.Format = "yyyy/MM/dd";

            // 解決CalendarExtender在ModalPopupExtender被遮蔽的問題
            // 必須呼叫JUtil.Web.JavaScript.jutil-all.js
            /*
            <asp:ScriptManager ID="SM" runat="server">
                <Scripts>
                  <asp:ScriptReference Assembly="JUtil.Web" Name="JUtil.Web.JavaScript.jutil-all.js" />
                </Scripts>
            </asp:ScriptManager>
            */
            ce.OnClientShown = "AjaxControlToolKitPlugin.RaiseZIndex";

            re.ID = "re";
            re.ControlToValidate = tb.ID;
            re.SetFocusOnError = false;
            re.ErrorMessage = FormatErrorMessage;
            re.Display = ValidatorDisplay.Dynamic;
            re.ValidationExpression = DateExpression;
            re.ValidationGroup = ValidationGroup;

            CssClass = "dateEdit";

            Controls.Add(tb);
            Controls.Add(editMask);
            Controls.Add(ib);
            Controls.Add(ce);

            if (UseDateTimePicker)
            {
                timeSelector.SelectedTimeFormat = TimeSelector.TimeFormat.Twelve;
                timeSelector.MinuteIncrement = 1;

                timeSelector.AmPm = TimeSelector.AmPmSpec.PM;
                timeSelector.Hour = 12;
                timeSelector.Minute = 0;
                
                Controls.Add(timeSelector);
            }

            Controls.Add(re);

            if (Required)
            {
                RequiredFieldValidator rv = new RequiredFieldValidator();
                rv.ID = "rv";
                rv.ControlToValidate = tb.ID;
                rv.InitialValue = "____/__/__";
                rv.ErrorMessage = RequiredErrorMessage;
                rv.Display = ValidatorDisplay.Dynamic;
                rv.SetFocusOnError = false;
                rv.ValidationGroup = ValidationGroup;
                Controls.Add(rv);
            }

            
        }
        #endregion


        #region IControlAccessor 成員

        /// <summary>
        /// 判斷是否為指定型別
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public bool IsMatchType(Control ctl)
        {
            bool match = ctl is DateEdit;
            return match;
        }

        /// <summary>
        /// Get Date
        /// </summary>
        public object GetValue(Control ctl)
        {
            DateEdit de = (DateEdit)ctl;
            return de.Text;
        }

        /// <summary>
        /// Set Date
        /// </summary>
        public void SetValue(Control ctl, object value)
        {
            DateEdit de = (DateEdit)ctl;
            de.Text = value.ToString();
        }

        #endregion
    }
}

