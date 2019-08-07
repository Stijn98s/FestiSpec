using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FestiApp.Util
{
    public static class ObservableCollectionExtension
    {
        public static ObservableCollection<T> CopyFrom<T>(this ObservableCollection<T> str, ICollection<T> collection)
        {
            str.Clear();

            foreach (var x1 in collection)
            {
                str.Add(x1);
            }

            return str;
        }
    }
}