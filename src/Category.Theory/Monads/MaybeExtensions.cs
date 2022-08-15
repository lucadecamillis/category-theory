using Category.Theory.Linq;

namespace Category.Theory.Monads;

public static class MaybeExtensions
{
    /// <summary>
    /// Convert the given maybe to nullable (for value types)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="maybe"></param>
    /// <returns></returns>
    public static T? ToNullable<T>(this Maybe<T> maybe) where T : struct
    {
        if (maybe.HasItem)
        {
            return maybe.Item;
        }

        return null;
    }

    /// <summary>
    /// Convert the given nullable into maybe (for value types)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="nullable"></param>
    /// <returns></returns>
    public static Maybe<T> FromNullable<T>(this T? nullable) where T : struct
    {
        if (nullable.HasValue)
        {
            return Maybe.FromItem(nullable.Value);
        }

        return Maybe.Empty<T>();
    }

    /// <summary>
    /// Select a string from the given maybe.
    /// Assure the string is not null or empty spaces.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="maybe"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static Maybe<string> SelectString<T>(this Maybe<T> maybe, Func<T, string> selector)
    {
        if (maybe.HasItem)
        {
            return TrySelectString(maybe.Item, selector);
        }

        return Maybe.Empty<string>();
    }

    /// <summary>
    /// Try select a string from the given object.
    /// Assure the string is not null or empty spaces.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="candidate"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static Maybe<string> TrySelectString<T>(this T candidate, Func<T, string> selector)
    {
        if (candidate == null)
        {
            return Maybe.Empty<string>();
        }

        string stringValue = selector(candidate);
        if (!string.IsNullOrWhiteSpace(stringValue))
        {
            return Maybe.FromItem(stringValue);
        }

        return Maybe.Empty<string>();
    }

    /// <summary>
    /// Select only items in the list of maybe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    public static IEnumerable<T> SelectItems<T>(this IEnumerable<Maybe<T>> items)
    {
        if (items.NullOrEmpty())
        {
            return Enumerable.Empty<T>();
        }

        return items
            .Where(i => i.HasItem)
            .Select(i => i.Item);
    }

    /// <summary>
    /// Select the element given the key in the dictionary
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static Maybe<T> Select<K, T>(this IDictionary<K, T> dictionary, K key)
    {
        if (dictionary.NullOrEmpty())
        {
            return Maybe.Empty<T>();
        }

        T value;
        if (dictionary.TryGetValue(key, out value))
        {
            return Maybe.FromItem(value);
        }

        return Maybe.Empty<T>();
    }

    /// <summary>
    /// Select the maybe element given the key in the dictionary
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static Maybe<T> Select<K, T>(this IDictionary<K, Maybe<T>> dictionary, K key)
    {
        if (dictionary.NullOrEmpty())
        {
            return Maybe.Empty<T>();
        }

        Maybe<T> value;
        if (dictionary.TryGetValue(key, out value))
        {
            return value;
        }

        return Maybe.Empty<T>();
    }

    /// <summary>
    /// Select the item at the given index in case the index is defined.
    /// Otherwise return an empty object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="idx"></param>
    /// <returns></returns>
    public static Maybe<T> Select<T>(this IList<T> source, int idx)
    {
        if (source.NullOrEmpty())
        {
            // Empty collection
            return Maybe.Empty<T>();
        }

        if (idx < 0 || idx >= source.Count)
        {
            // Index out of bound
            return Maybe.Empty<T>();
        }

        return Maybe.FromItem(source[idx]);
    }

    /// <summary>
    /// Check whether the given source collection contains one and only one element.
    /// In that case return the element, otherwise returns an empty object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Maybe<T> TrySingle<T>(this IEnumerable<T> source)
    {
        T element;
        if (source.IsSingle(out element))
        {
            return Maybe.FromItem(element);
        }

        return Maybe.Empty<T>();
    }

    /// <summary>
    /// Return the first occurrence of the element in the source
    /// or an empty object in case the list is empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Maybe<T> TryFirst<T>(this IEnumerable<T> source)
    {
        if (source.NullOrEmpty())
        {
            return Maybe.Empty<T>();
        }

        return Maybe.FromItem(source.First());
    }

    /// <summary>
    /// Select an <see cref="IEnumerable{T}"/> from the given maybe source
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="maybe"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<TResult> AsEnumerable<T, TResult>(this Maybe<T> maybe, Func<T, IEnumerable<TResult>> selector)
    {
        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        if (maybe.HasItem)
        {
            IEnumerable<TResult> result = selector(maybe.Item);
            if (!result.NullOrEmpty())
            {
                return result;
            }
        }

        return Enumerable.Empty<TResult>();
    }
}