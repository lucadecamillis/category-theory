using System.Collections.Generic;

namespace Category.Theory.Linq
{
    internal static class LinqExtensions
    {
        /// <summary>
        /// Check whether the given collection contains a single element of the given type.
        /// Return the single element in the out parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsSingle<T>(this IEnumerable<T> collection, out T element)
        {
            element = default(T);

            if (collection == null)
            {
                // The collection is null, no element present
                return false;
            }

            // Check if we have the first element
            IEnumerator<T> enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return false;
            }

            // Store the first element
            element = enumerator.Current;

            // Now we shouldn't be able to go any further with our enumerator
            return !enumerator.MoveNext();
        }

        /// <summary>
        /// Try to get the first item out of the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool TryFirst<T>(this IEnumerable<T> collection, out T element)
        {
            element = default(T);

            if (collection == null)
            {
                // The collection is null, no element present
                return false;
            }

            // Check if we have the first element
            IEnumerator<T> enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return false;
            }

            // Store the first element
            element = enumerator.Current;

            return true;
        }
    }
}