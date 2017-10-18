using Petrovich.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Web.Tests.Models
{
    public class JsonResponseTests
    {
        private const string ExceptionTypeName = "ArgumentNullException";
        private const string ExceptionMessage = "Value cannot be null.";
        private const string NewLineSymbols = "\r\n";
        
        [Fact]
        public void FormatExceptionMessage_WhenExceptionIsNull_ReturnsEmptyString()
        {
            var result = JsonResponseViewModel.FormatExceptionMessage(null);

            Assert.NotNull(result);
            Assert.Equal("", result);
        }

        [Fact]
        public void FormatExceptionMessage_WhenExceptionHasOneLevel_ReturnsExceptionMessageAndType()
        {
            var result = JsonResponseViewModel.FormatExceptionMessage(new ArgumentNullException());

            Assert.NotNull(result);
            Assert.Equal($"Type: {ExceptionTypeName}{NewLineSymbols}{ExceptionMessage}{NewLineSymbols}", result);
        }

        [Fact]
        public void FormatExceptionMessage_WhenExceptionHasMultipleLevel_ReturnsAllTypesAndMessages()
        {
            var expectedString = $"Type: {ExceptionTypeName}{NewLineSymbols}{ExceptionMessage}{NewLineSymbols}Type: {ExceptionTypeName}{NewLineSymbols}{ExceptionMessage}{NewLineSymbols}Type: {ExceptionTypeName}{NewLineSymbols}{ExceptionMessage}{NewLineSymbols}";
            var exception = new ArgumentNullException(ExceptionMessage, 
                                new ArgumentNullException(ExceptionMessage, 
                                    new ArgumentNullException()));

            var result = JsonResponseViewModel.FormatExceptionMessage(exception);

            Assert.NotNull(result);
            Assert.Equal(expectedString, result);
        }
    }
}
