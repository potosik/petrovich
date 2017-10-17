using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Mappers
{
    public static class EnumMapper
    {
        public static TResult Map<TSource, TResult>(TSource value)
            where TSource : struct
            where TResult : struct
        {
            if (typeof(TSource).IsEnum && typeof(TSource).IsEnum)
            {
                return (TResult)Enum.Parse(typeof(TSource), value.ToString());
            }

            throw new ArgumentException("Enumerations type accepted only");
        }

        public static TResult? Map<TSource, TResult>(TSource? value)
           where TSource : struct
           where TResult : struct
        {
            if (typeof(TSource).IsEnum && typeof(TSource).IsEnum)
            {
                if (!value.HasValue)
                {
                    return null;
                }

                return (TResult)Enum.Parse(typeof(TSource), value.Value.ToString());
            }

            throw new ArgumentException("Enumerations type accepted only");
        }
    }
}
