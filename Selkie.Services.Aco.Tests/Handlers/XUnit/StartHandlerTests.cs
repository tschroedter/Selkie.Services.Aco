using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Services.Aco.Handlers;
using Selkie.XUnit.Extensions;
using Xunit.Extensions;

namespace Selkie.Services.Aco.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class StartHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void HandleCallsStopTest([NotNull] [Frozen] IServiceColony colony,
                                        [NotNull] StartHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(new StartMessage
                       {
                           Times = 1000
                       });

            // Assert
            colony.Received().Start(1000);
        }
    }
}