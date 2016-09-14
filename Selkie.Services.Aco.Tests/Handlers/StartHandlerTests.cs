using System;
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
    internal sealed class StartHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void HandleCallsStartTest([NotNull] [Frozen] IServiceColony colony,
                                         [NotNull] StartHandler sut)
        {
            // Arrange
            Guid colonyId = Guid.NewGuid();

            // Act
            sut.Handle(new StartMessage
                       {
                           ColonyId = colonyId,
                           Times = 1000
                       });

            // Assert
            colony.Received().Start(colonyId,
                                    1000);
        }
    }
}