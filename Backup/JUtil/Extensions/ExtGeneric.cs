using System.Collections.Generic;

namespace JUtil.Extensions
{
    public class ExtGeneric
    {
        public delegate bool FilteringConditionDelegate<T>(T datum);

        public static List<T> Filter<T>(IEnumerable<T> data, FilteringConditionDelegate<T> FilteringCondition)
        {
            List<T> result = new List<T>();
            foreach (T datum in data)
            {
                if (FilteringCondition(datum))
                    result.Add(datum);
            }
            return result;
        }

        //[Obsolete("廢棄此Function。使用在移除元素上，結果不如預期。")]
        //public static IEnumerable<T> Filter<T>(IEnumerable<T> data, FilteringConditionDelegate<T> FilteringCondition) 
        //{
        //    foreach (T datum in data)
        //    {
        //        if (FilteringCondition(datum))
        //            yield return datum;
        //    }
        //}

    
    } // end of ExtGeneric
}
