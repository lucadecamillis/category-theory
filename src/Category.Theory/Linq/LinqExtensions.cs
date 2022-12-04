using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Category.Theory.Linq
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Specifies whether the given collection is null or contains no elements
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool NullOrEmpty(this IEnumerable collection)
        {
            return collection == null || !collection.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Specifies whether the given collection is null or contains no elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool NullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        /// <summary>
        /// Check whether the given collection contains a single element of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsSingle<T>(this IEnumerable<T> collection)
        {
            return !collection.NullOrEmpty() && !collection.Skip(1).Any();
        }

        /// <summary>
        /// Check whether the given collection contains a single element of the given type.
        /// Return the single element in the out parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsSingle<T>(this IEnumerable collection, out T element)
        {
            element = default(T);

            if (collection == null)
            {
                // The collection is null, no element present
                return false;
            }

            // Check if we have the first element
            IEnumerator enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return false;
            }

            if (!(enumerator.Current is T))
            {
                // The first element is not of type T
                return false;
            }

            // Store the first element
            element = (T)enumerator.Current;

            // Now we shouldn't be able to go any further with our enumerator
            return !enumerator.MoveNext();
        }
    }
}