using System.ComponentModel;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// 繼承原先的DropDownList, 
    /// 目的在於避免先設值, 再連結DataSource時
    /// 發生錯誤
    /// </summary>
    public class XDropDownList : DropDownList
    {
        private string FCachedSelectedValue = null;

        /// <summary>
        /// Auto Add Value If Exception occur !
        /// </summary>
        public bool AutoAddValueOnException
        {
            // [
            get { return _AutoAddValueOnException; }
            set { _AutoAddValueOnException = value; }
        }
        private bool _AutoAddValueOnException = true;

        /// <summary>
        /// Keep SelectedValue If DataSource not prepare
        /// </summary>
        public override string SelectedValue
        {
            get { return base.SelectedValue; }
            set
            {
                if (this.Items.Count != 0)
                {
                    ListItem oItem = this.Items.FindByValue(value);
                    if ((oItem == null))
                    {
                        // [ 當 Items 不存在時，自動新增例外值於清單中
                        if (_AutoAddValueOnException && (!string.IsNullOrEmpty(value)))
                        {
                            string sValue = value.ToString();
                            try
                            {
                                this.Items.Add(new ListItem(sValue + "-", value));
                                base.SelectedValue = value;
                            }
                            catch (Exception ex)
                            {
                                Log.E(ex);
                            }
                        }
                        else
                        {
                            this.SelectedIndex = -1;
                            //當 Items 不存在時，避免Exception
                        }
                    }
                    else
                    {
                        base.SelectedValue = value;
                    }
                    // not binding yet ==> cache selected value and wait for PerformDataBinding to fulfill
                }
                else
                {
                    FCachedSelectedValue = value;
                }
            }
        }

        /// <summary>
        /// Do DataBind
        /// </summary>
        protected override void PerformDataBinding(System.Collections.IEnumerable data)
        {
            // [
            try
            {
                base.PerformDataBinding(data);
                //] [
            }
            catch (Exception ex)
            {
                Log.E("XDropDownList.PerformDataBinding:(%s)%s", ex.GetType().ToString(), ex.Message);
            }
            //DataSoruceID 資料繫結後再設定 SelectedValue 屬性值
            if (((FCachedSelectedValue != null)))
            {
                this.SelectedValue = FCachedSelectedValue;
                FCachedSelectedValue = null;
            }
        }
        
    }
}
