using System;
using System.Collections.Generic;

namespace WebApiTestCase.Common.Extensions
{
    public static class RandomExtensions
    {
        public static T NextItem<T>(this Random random, IList<T> list)
            => list[random.Next(list.Count)];
    }
}