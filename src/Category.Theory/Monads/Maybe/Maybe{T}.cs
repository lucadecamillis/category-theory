﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Category.Theory.Monads
{
    public abstract class Maybe<T>
    {
        protected Maybe()
        {

        }

        public abstract bool HasValue();

        public abstract bool Empty();

        public abstract bool TryGetValue(out T value);

        public abstract bool EqualsTo(T item);

        public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (TryGetValue(out T value))
            {
                TResult result = selector(value);
                if (result != null)
                {
                    return result;
                }
            }

            return None<TResult>.Instance;
        }

        /// <summary>
        /// Selection of a nullable struct value
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Maybe<TResult> Select<TResult>(Func<T, TResult?> selector) where TResult : struct
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (TryGetValue(out T value))
            {
                TResult? result = selector(value);
                if (result.HasValue)
                {
                    return result.Value;
                }
            }

            return None<TResult>.Instance;
        }

        public Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (TryGetValue(out T value))
            {
                return selector(value);
            }

            return None<TResult>.Instance;
        }

        public TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc)
        {
            if (someFunc == null)
            {
                throw new ArgumentNullException(nameof(someFunc));
            }

            if (TryGetValue(out T value))
            {
                return someFunc(value);
            }
            else
            {
                return noneFunc();
            }
        }

        public Maybe<TResult> OfType<TResult>()
        {
            if (TryGetValue(out T value) && value is TResult t)
            {
                return t;
            }

            return None<TResult>.Instance;
        }

        public IEnumerable<TResult> AsEnumerable<TResult>(Func<T, IEnumerable<TResult>> selector)
        {
            return this.Select(selector).GetValueOrFallback(Enumerable.Empty<TResult>());
        }

        public IList<TResult> AsList<TResult>(Func<T, IList<TResult>> selector)
        {
            return this.Select(selector).GetValueOrFallback(Array.Empty<TResult>());
        }

        public static implicit operator Maybe<T>(T value) => new Some<T>(value);

        public static implicit operator Maybe<T>(Types.None _) => None<T>.Instance;
    }
}