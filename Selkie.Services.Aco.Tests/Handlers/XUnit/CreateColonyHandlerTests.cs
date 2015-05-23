using System.Diagnostics.CodeAnalysis;
using Castle.Core.Logging;
using EasyNetQ;
using NSubstitute;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Services.Aco.Handlers;
using Xunit;

namespace Selkie.Services.Aco.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class CreateColonyHandlerTests
    {
        [Fact]
        public void HandleCallCreateColonyTest()
        {
            // Arrange
            var manager = Substitute.For <IColonySourceManager>();

            var sut = new CreateColonyHandler(Substitute.For <ILogger>(),
                                              Substitute.For <IBus>(),
                                              manager);

            // Act
            var message = new CreateColonyMessage
                          {
                              CostMatrix = new[]
                                           {
                                               new[]
                                               {
                                                   1,
                                                   2
                                               },
                                               new[]
                                               {
                                                   3,
                                                   4
                                               }
                                           },
                              CostPerLine = new[]
                                            {
                                                1,
                                                2
                                            }
                          };

            sut.Handle(message);

            // Assert
            manager.Received()
                   .CreateColony(
                                 Arg.Is <IServiceColonyParameters>(
                                                                   x =>
                                                                   x.CostMatrix == message.CostMatrix &&
                                                                   x.CostPerLine == message.CostPerLine));
        }

        [Fact]
        public void HandleSendsMessageTest()
        {
            // Arrange
            var bus = Substitute.For <IBus>();

            var sut = new CreateColonyHandler(Substitute.For <ILogger>(),
                                              bus,
                                              Substitute.For <IColonySourceManager>());

            // Act
            sut.Handle(new CreateColonyMessage());

            // Assert
            bus.Received().PublishAsync(Arg.Any <CreatedColonyMessage>());
        }
    }
}