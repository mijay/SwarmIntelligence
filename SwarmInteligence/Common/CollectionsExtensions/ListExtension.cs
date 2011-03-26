using System.Collections.Generic;

namespace Common.CollectionsExtensions
{
    public static class ListExtension
    {
        /// <summary>
        /// Add an <paramref name="elem"/> to <paramref name="list"/> at <paramref name="index"/>.
        /// Fill <paramref name="list"/> with default elements of type <typeparamref name="T"/>
        /// if it is necessary to achieve <paramref name="index"/>.
        /// </summary>
        public static void SetAt<T>(this IList<T> list, int index, T elem)
        {
            if(list.Count > index)
                list[index] = elem;
            else {
                for(int i = index - list.Count; i > 0; --i)
                    list.Add(default(T));
                list.Add(elem);
            }
        }
    }
}