using System.Diagnostics.CodeAnalysis;
using Selkie.Aop.Messages;
using Xunit;

namespace Selkie.Services.Aco.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    public sealed class ExceptionThrownMessageToStringConverterTests
    {
        [Fact]
        public void Convert_ReturnsString_ForException()
        {
            // Arrange
            var sut = new ExceptionThrownMessageToStringConverter();

            // Act
            string actual = sut.Convert(CreateMessage());

            // Assert
            Assert.Equal("Invocation: Invocation\r\nMessage: Message\r\nStackTrace: StackTrace\r\n",
                         actual);
        }

        [Fact]
        public void Convert_ReturnsString_ForExceptionWithInnerExceptions()
        {
            // Arrange
            var sut = new ExceptionThrownMessageToStringConverter();

            // Act
            string actual = sut.Convert(CreateMessageWithInnerExceptions());

            // Assert
            Assert.Equal("Invocation: Invocation\r\nMessage: Message\r\nStackTrace: StackTrace\r\n" +
                         "Inner Exception:\r\nInvocation: Inner Invocation\r\nMessage: Inner Message\r\nStackTrace: Inner StackTrace\r\n",
                         actual);
        }

        private ExceptionThrownMessage CreateMessage()
        {
            var information = new ExceptionInformation
                              {
                                  Invocation = "Invocation",
                                  Message = "Message",
                                  StackTrace = "StackTrace"
                              };

            return new ExceptionThrownMessage
                   {
                       Exception = information
                   };
        }

        private ExceptionThrownMessage CreateMessageWithInnerExceptions()
        {
            var information = new ExceptionInformation
                              {
                                  Invocation = "Invocation",
                                  Message = "Message",
                                  StackTrace = "StackTrace"
                              };

            var innerInformation = new ExceptionInformation
                                   {
                                       Invocation = "Inner Invocation",
                                       Message = "Inner Message",
                                       StackTrace = "Inner StackTrace"
                                   };

            return new ExceptionThrownMessage
                   {
                       Exception = information,
                       InnerExceptions = new[]
                                         {
                                             innerInformation
                                         }
                   };
        }
    }
}