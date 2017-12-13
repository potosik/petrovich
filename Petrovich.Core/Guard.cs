using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core
{
    public static class Guard
    {
        public static void NotNullArgument(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void NotNullReference(object value, string message)
        {
            if (value == null)
            {
                throw new NullReferenceException(message);
            }
        }

        public static void ValidateIdentifier(int id, string paramName)
        {
            if (id <= Constants.Validation.UnassignedId)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        public static void ValidateIdentifier(Guid id, string paramName)
        {
            if (id == Constants.Validation.UnassignedGuidId)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        public static void NotNullOrWhiteSpace(string value, string paramName)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void CollectionNotEmpty<T>(IEnumerable<T> collection, string paramName)
        {
            if (!collection.Any())
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }
    }
}
