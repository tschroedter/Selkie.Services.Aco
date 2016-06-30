using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Services.Aco.Handlers;

namespace Selkie.Services.Aco.Tests.Handlers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class StopRequestHandlerTests
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