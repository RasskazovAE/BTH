using System;
using System.Collections.Generic;
using System.Linq;

namespace BTH.Core.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitByLength(this string str, int chunkSize)
        {
            return Enumerable.Range(0, (int)Math.Ceiling((double)str.Length / chunkSize))
                .Select(i =>
                {
                    if (str.Length - i * chunkSize > chunkSize)
                        return str.Substring(i * chunkSize, chunkSize);
                    else
                        return str.Substring(i * chunkSize);
                });
        }
    }
}
