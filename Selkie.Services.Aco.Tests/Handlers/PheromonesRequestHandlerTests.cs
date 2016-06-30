using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.EasyNetQ;
using Selkie.NUnit.Extensions;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Services.Aco.Handlers;

namespace Selkie.Services.Aco.Tests.Handlers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class PheromonesRequestHandlerHandlerTests
    {
        private const double Tolerance = 0.01;

        [Theory]
        [AutoNSubstituteData]
        public void HandleSendsReplyTest([NotNull] [Frozen] IServiceColony colony,
                                         [NotNull] [Frozen] ISelkieBus bus,
                                         [NotNull] PheromonesRequestHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(new PheromonesRequestMessage());

            // Assert
            bus.Received()
               .PublishAsync(
                             Arg.Is <PheromonesMessage>(
                                                        x =>
                                                        Math.Abs(x.Average - colony.PheromonesAverage) < Tolerance &&
                                                        Math.Abs(x.Minimum - colony.PheromonesMinimum) < Tolerance));
        }
    }
}