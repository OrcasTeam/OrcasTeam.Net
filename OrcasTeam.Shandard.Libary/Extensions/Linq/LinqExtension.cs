using System;
using System.Collections.Generic;
using System.Linq;

namespace OrcasTeam.Shandard.Libary.Extensions
{
   public static class LinqExtension
    {
        /// <summary>
        ///          如果条件为真,则通过指定条件进行筛选<see cref="IEnumerable{T}"/>>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition,
            Func<TSource, bool> predicate)
            => condition ? source.Where(predicate) : source;

        /// <summary>
        ///          根据指定条件去重<see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            if (source?.Any() !=true) yield break;
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                    yield return element;
            }
        }

        /// <summary>
        ///          根据指定长度将<see cref="IEnumerable{T}"/>进行分组
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(this IEnumerable<TSource> source, int batchSize)
        {
            if (batchSize <= 0)
                throw new ArgumentException($"{nameof(batchSize)}必须大于0");
            if (source?.Any() != true) yield break;
            while (source.Any())
            {
                yield return source.Take(batchSize);
                source = source.Skip(batchSize);
            }
        }

        /// <summary>
        ///         批量添加指定集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void AddRange<T>(this IList<T> source, IEnumerable<T> target)
        {
            if(null== target) throw new  ArgumentNullException($"{nameof(target)}");
            foreach (var t in target)
                source.Add(t);
        }
    }
}
