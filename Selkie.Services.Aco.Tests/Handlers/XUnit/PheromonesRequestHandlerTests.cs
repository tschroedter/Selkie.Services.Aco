using System;
using System.Diagnostics.CodeAnalysis;
using EasyNetQ;
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
    public class PheromonesRequestHandlerHandlerTests
    {
        private const double Tolerance = 0.01;

        [Theory]
        [AutoNSubstituteData]
        public void HandleSendsReplyTest([NotNull] [Frozen] IServiceColony colony,
                                         [NotNull] [Frozen] IBus bus,
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