using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;

namespace JUtil.Web
{
    #region HttpSessionStateAdapter
    /// <summary>
    /// HttpSessionStateAdapter
    /// </summary>
    public class HttpSessionStateAdapter : IDictionary<string, object>
    {
        private HttpSessionState Session;

        /// <summary>
        /// HttpSessionStateAdapter
        /// </summary>
        /// <param name="Session"></param>
        public HttpSessionStateAdapter(HttpSessionState Session)
        {
            this.Session = Session;
        }

        #region IDictionary<string,object> 成員

        /// <summary>
        /// IDictionary.ContainsKey
        /// </summary>
        public void Add(string key, object value)
        {
            Session.Add(key, value);
        }

        /// <summary>
        /// IDictionary.ContainsKey
        /// </summary>
        public bool ContainsKey(string key)
        {
            foreach (object objKey in Session.Keys)
            {
                if (objKey.ToString().Equals(key))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// IDictionary.Keys
        /// </summary>
        public ICollection<string> Keys
        {
            get 
            {
                List<string> keys = new List<string>();

                foreach (string Key in Session.Keys)
                {
                    keys.Add(Key);
                }

                return keys;
            }
        }

        /// <summary>
        /// IDictionary.Remove
        /// </summary>
        public bool Remove(string key)
        {
            bool contain = ContainsKey(key);

            if (contain)
                Session.Remove(key);

            return contain;
        }

        /// <summary>
        /// IDictionary.TryGetValue
        /// </summary>
        public bool TryGetValue(string key, out object value)
        {
            bool contain = ContainsKey(key);

            if (contain)
                value = Session[key];
            else
                value = null;

            return contain;
        }

        /// <summary>
        /// IDictionary.Values
        /// </summary>
        public ICollection<object> Values
        {
            get
            {
                List<object> values = new List<object>();

                foreach (string Key in Session.Keys)
                {
                    values.Add(Session[Key]);
                }

                return values;
            }
        }

        /// <summary>
        /// IDictionary.this[string key]
        /// </summary>
        public object this[string key]
        {
            get
            {
                return Session[key];
            }
            set
            {
                Session[key] = value;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<string,object>> 成員

        /// <summary>
        /// ICollection[KeyValuePair[string,object]].Add
        /// </summary>
        public void Add(KeyValuePair<string, object> item)
        {
            Session.Add(item.Key, item.Value);
        }

        /// <summary>
        /// ICollection[KeyValuePair[string,object>>]Clear
        /// </summary>
        public void Clear()
        {
            Session.Clear();
        }

        /// <summary>
        /// ICollection[KeyValuePair[string,object]].Contains
        /// </summary>
        public bool Contains(KeyValuePair<string, object> item)
        {
            foreach (object objKey in Session.Keys)
            {
                if (objKey.ToString().Equals(item.Key))
                {
                    if (Session[item.Key] == item.Value)
                    {
                        return true;
                    }
                }
                    
            }
            return false;
        }

        /// <summary>
        /// ICollection[KeyValuePair[string,object]].CopyTo
        /// </summary>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            int index = 0;

            foreach (string Key in Session.Keys)
            {
                KeyValuePair<string, object> KVP = new KeyValuePair<string, object>(Key, Session[Key]);

                if (index < arrayIndex)
                {
                    array.SetValue(KVP, index);
                   
                    index = index + 1;
                }
            }
        }

        /// <summary>
        /// ICollection[KeyValuePair[string,object]].Count
        /// </summary>
        public int Count
        {
            get { return Session.Count; }
        }

        /// <summary>
        /// ICollection[KeyValuePair[string,object]].IsReadOnly
        /// </summary>
        public bool IsReadOnly
        {
            get { return Session.IsReadOnly; }
        }

        /// <summary>
        /// ICollection[KeyValuePair[string,object]].Remove
        /// </summary>
        public bool Remove(KeyValuePair<string, object> item)
        {
            foreach (object objKey in Session.Keys)
            {
                if (objKey.ToString().Equals(item.Key))
                {
                    if (Session[item.Key] == item.Value)
                    {
                        Session.Remove(item.Key);

                        return true;
                    }
                }

            }
            return false;
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,object>> 成員

        /// <summary>
        /// http://stackoverflow.com/questions/1682484/explicit-implementation-of-interfaces-getenumerator-causes-stack-overflow
        /// </summary>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (string Key in Session.Keys)
            {
                yield return new KeyValuePair<string, object>(Key, Session[Key]);
            }
        }
        #endregion

        #region IEnumerable 成員

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (string Key in Session.Keys)
            {
                yield return new KeyValuePair<string, object>(Key, Session[Key]);
            }
        }

        #endregion
    }
    #endregion

}
