using Petrovich.Business.Exceptions;
using Petrovich.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Business.Tests
{
    public class ErrorCodeTests
    {
        [Fact]
        public void TestForMissingOrDuplicateErrorCodeResourceStrings()
        {
            var enumNames = EnumUtils.GetValues<ErrorCode>().ToArray();
            var enumValues = new List<int>();
            var resManager = new System.Resources.ResourceManager("Petrovich.Business.Properties.Resources", typeof(ErrorCode).Assembly);

            var brokenCodes = new List<string>();

            for (var index = 0; index < enumNames.Length; index++)
            {
                var enumName = enumNames.GetValue(index);

                var enumValue = $"E{(int)enumName}";
                enumValues.Add((int)enumNames.GetValue(index));
                var res = resManager.GetString(enumValue);
                if (res == null)
                {
                    brokenCodes.Add(enumValue);
                }
            }

            var duplicates = enumValues
                .GroupBy(ev => ev)
                .Select(code => new { Code = code.Key, Count = code.Count() })
                .Where(code => code.Count > 1)
                .ToList();

            Assert.Equal(0, brokenCodes.Count);
            Assert.Equal(0, duplicates.Count);
        }
    }
}
