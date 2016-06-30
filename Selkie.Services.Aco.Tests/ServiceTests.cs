using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.EasyNetQ;
using Selkie.NUnit.Extensions;
using Selkie.Services.Common.Messages;

namespace Selkie.Services.Aco.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ServiceTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void ServiceStartSendsMessageTest([NotNull] [Frozen] ISelkieBus bus,
                                                 [NotNull] Service sut)
        {
            // Arrange
            // Act
            sut.Start();

            // Assert
            bus.Received().Publish(Arg.Is <ServiceStartedResponseMessage>(x => x.ServiceName == Service.ServiceName));
        }

        [Theory]
        [AutoNSubstituteData]
        public void ServiceStopSendsMessageTest([NotNull] [Frozen] ISelkieBus bus,
                                                [NotNull] Service sut)
        {
            // Arrange
            // Act
            sut.Stop();

            // Assert
            bus.Received().Publish(Arg.Is <ServiceStoppedResponseMessage>(x => x.ServiceName == Service.ServiceName));
        }
    }
}