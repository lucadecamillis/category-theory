using System.Collections.Generic;

namespace Category.Theory.Monads
{
    public class MaybeComparer<T> : IComparer<Maybe<T>>
    {
        public static IComparer<Maybe<T>> Instance { get; } = new MaybeComparer<T>();

        #region CTOR

        private MaybeComparer()
        {
            
        }

        #endregion

        #region IComparer

        public int Compare(Maybe<T> x, Maybe<T> y)
        {
            var result =
                from xx in x
                from yy in y
                select Comparer<T>.Default.Compare(xx, yy);

            if (result.TryGetValue(out int value))
            {
                return value;
            }

            return CheckEmpty(x, y);
        }

        #endregion

        /// <summary>
        /// One of the two maybes is empty
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int CheckEmpty(Maybe<T> x, Maybe<T> y)
        {
            if (x.HasValue())
            {
                return 1;
            }

            if (y.HasValue())
            {
                return -1;
            }

            return 0;
        }
    }    
}