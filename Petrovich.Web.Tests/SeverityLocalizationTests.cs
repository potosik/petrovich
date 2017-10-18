using Petrovich.Business.Models.Enumerations;
using Petrovich.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Web.Tests
{
    public class SeverityLocalizationTests
    {
        [Fact]
        public void TestForMissingOrDuplicateSeverityResourceStrings()
        {
            var enumNames = EnumUtils.GetValues<LogSeverityBusiness>().ToArray();
            var enumValues = new List<string>();
            var resManager = new System.Resources.ResourceManager("Petrovich.Web.Properties.Resources", typeof(Startup).Assembly);

            var brokenSeverities = new List<string>();

            for (var index = 0; index < enumNames.Length; index++)
            {
                var enumName = enumNames.GetValue(index).ToString();                
                enumValues.Add(enumName);

                var res = resManager.GetString(enumName);
                if (res == null)
                {
                    brokenSeverities.Add(enumName);
                }
            }

            var duplicates = enumValues
                .GroupBy(ev => ev)
                .Select(code => new { Code = code.Key, Count = code.Count() })
                .Where(code => code.Count > 1)
                .ToList();

            Assert.Equal(0, brokenSeverities.Count);
            Assert.Equal(0, duplicates.Count);
        }

        [Fact]
        public void TestForMissingOrDuplicatePrityTypeResourceStrings()
        {
            var enumNames = EnumUtils.GetValues<PriceTypeBusiness>().ToArray();
            var enumValues = new List<string>();
            var resManager = new System.Resources.ResourceManager("Petrovich.Web.Properties.Resources", typeof(Startup).Assembly);

            var brokenPriceTypes = new List<string>();

            for (var index = 0; index < enumNames.Length; index++)
            {
                var enumName = enumNames.GetValue(index).ToString();
                enumValues.Add(enumName);

                var res = resManager.GetString($"PriceType_{enumName}");
                if (res == null)
                {
                    brokenPriceTypes.Add(enumName);
                }
            }

            var duplicates = enumValues
                .GroupBy(ev => ev)
                .Select(code => new { Code = code.Key, Count = code.Count() })
                .Where(code => code.Count > 1)
                .ToList();

            Assert.Equal(0, brokenPriceTypes.Count);
            Assert.Equal(0, duplicates.Count);
        }
    }
}
