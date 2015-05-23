using System.Diagnostics.CodeAnalysis;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.Services.Common.Messages;
using Selkie.XUnit.Extensions;
using Xunit.Extensions;

namespace Selkie.Services.Aco.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class ServiceTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void ServiceStartSendsMessageTest([NotNull] [Frozen] IBus bus,
                                                 [NotNull] AcoService sut)
        {
            // Arrange
            // Act
            sut.Start();

            // Assert
            bus.Received().Publish(Arg.Is <ServiceStartedResponseMessage>(x => x.ServiceName == AcoService.ServiceName));
        }

        [Theory]
        [AutoNSubstituteData]
        public void ServiceStopSendsMessageTest([NotNull] [Frozen] IBus bus,
                                                [NotNull] AcoService sut)
        {
            // Arrange
            // Act
            sut.Stop();

            // Assert
            bus.Received().Publish(Arg.Is <ServiceStoppedResponseMessage>(x => x.ServiceName == AcoService.ServiceName));
        }
    }
}