using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace JUtil.WinForm.Extensions
{
    

    /// <summary>
    /// Extension System.Windows.Forms.ComboBox
    /// </summary>
    public static class ExtComboBox
    {
        /// <summary>
        /// fill combobox by DataTable's field values
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dt"></param>
        /// <param name="field"></param>
        public static void Fill(this ComboBox source, DataTable dt, string field)
        {
            source.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string fieldValue = row[field].ToString();
                source.Items.Add(fieldValue);
            }
        }

        public class ListControlItem
        {
            public object Value { get; set; }
            public string Text { get; set; }
        }

        public static void InitComboBox(this ComboBox cbx, List<ListControlItem> listControlItems)
        {
            cbx.Items.Clear();

            foreach (ListControlItem listControlItem in listControlItems)
                cbx.Items.Add(listControlItem);

            cbx.DisplayMember = "Text";
            cbx.ValueMember = "Value";
        }

        public static void InitComboBox<T>(this ComboBox cbx) where T : struct, System.IConvertible
        {
            cbx.Items.Clear();

            Type t = typeof(T);

            foreach (T value in Enum.GetValues(t))
            {
                //string text = Enum.GetName(t, value);

                string text = GetEnumDescription(value);

                cbx.Items.Add(new ListControlItem { Text = text, Value = value });
            }

            cbx.DisplayMember = "Text";
            cbx.ValueMember = "Value";
        }

        //public static string GetEnumDescription(Enum value)
        public static string GetEnumDescription(object value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static T GetValue<T>(this ComboBox cbx) where T : struct, System.IConvertible
        {
            ListControlItem item = (ListControlItem)cbx.SelectedItem;
            return (T)item.Value;
        }

        public static void SetValue<T>(this ComboBox cbx, T value) where T : struct, System.IConvertible
        {
            ComboBox cb = cbx;

            int iDefaultValue = Convert.ToInt32(value);
            for (int i = 0; i < cb.Items.Count; i++)
            {
                ListControlItem item = (ListControlItem)cb.Items[i];

                int iValue = Convert.ToInt32(item.Value);

                if ((iDefaultValue & iValue) == iValue)
                    cb.SelectedIndex = i;
            }
        }

        public static bool SetNonEnumValue(this ComboBox cbx, object value)
        {
            ComboBox cb = cbx;

            for (int i = 0; i < cb.Items.Count; i++)
            {
                ListControlItem item = (ListControlItem)cb.Items[i];

                if(item.Value.Equals(value))
                {
                    cb.SelectedIndex = i;
                    return true;
                }
            }

            return false;
        }

        public static object GetNonEnumValue(this ComboBox cbx)
        {
            ListControlItem item = (ListControlItem)cbx.SelectedItem;
            return item != null ? item.Value : null;
        }

        public static string GetText(this ComboBox cbx)
        {
            ListControlItem item = (ListControlItem)cbx.SelectedItem;
            return item != null ? item.Text : string.Empty;
        }

    } // end of ExtComboBox
}
