using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.Xunit;
using Selkie.Services.Aco.Handlers;
using Selkie.XUnit.Extensions;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Selkie.Services.Aco.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class BaseHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void StartSubscribesToMessageTest([NotNull] [Frozen] IBus bus,
                                                 [NotNull] TestBaseHandler sut)
        {
            // Arrange
            string subscriptionId = sut.GetType().ToString();

            // Act
            sut.Start();

            // Assert
            bus.Received().SubscribeAsync(subscriptionId,
                                          Arg.Any <Func <TestMessage, Task>>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void HandleCallsHandlerTest([NotNull] TestBaseHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(new TestMessage());

            // Assert
            Assert.True(sut.HandleWasCalled);
        }

        [Fact]
        public void StartCallsLoggerTest()
        {
            // Arrange
            var logger = Substitute.For <ILogger>();

            var sut = new TestBaseHandler(logger,
                                          Substitute.For <IBus>(),
                                          Substitute.For <IColonySourceManager>());

            // Act
            sut.Start();

            // Assert
            logger.Received().Info(Arg.Is <string>(x => x.StartsWith("Subscribing to message")));
        }

        [Fact]
        public void StopCallsLoggerTest()
        {
            // Arrange
            var logger = Substitute.For <ILogger>();

            var sut = new TestBaseHandler(logger,
                                          Substitute.For <IBus>(),
                                          Substitute.For <IColonySourceManager>());

            // Act
            sut.Stop();

            // Assert
            logger.Received().Info(Arg.Is <string>(x => x.StartsWith("Received stop")));
        }

        [Fact]
        public void LoggerIsSetTest()
        {
            // Arrange
            var logger = Substitute.For <ILogger>();

            var sut = new TestBaseHandler(logger,
                                          Substitute.For <IBus>(),
                                          Substitute.For <IColonySourceManager>());

            // Act

            // Assert
            Assert.True(logger == sut.TestLogger);
        }

        public class TestBaseHandler : BaseHandler <TestMessage>
        {
            public bool HandleWasCalled;

            public TestBaseHandler([NotNull] ILogger logger,
                                   [NotNull] IBus bus,
                                   [NotNull] IColonySourceManager manager)
                : base(logger,
                       bus,
                       manager)
            {
            }

            internal ILogger TestLogger
            {
                get
                {
                    return Logger;
                }
            }

            // todo need a real TestBus to send/receive messages, so we can make this method protected
            internal override void Handle(TestMessage message)
            {
                HandleWasCalled = true;
            }
        }

        public class TestMessage
        {
        }
    }
}