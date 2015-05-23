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
    public class StopRequestHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void HandleCallsStopTest([NotNull] [Frozen] IServiceColony colony,
                                        [NotNull] StopRequestHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(new StopRequestMessage());

            // Assert
            colony.Received().Stop();
        }
    }
}