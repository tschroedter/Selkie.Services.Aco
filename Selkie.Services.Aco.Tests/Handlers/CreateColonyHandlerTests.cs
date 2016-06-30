using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Services.Aco.Handlers;

namespace Selkie.Services.Aco.Tests.Handlers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class CreateColonyHandlerTests
    {
        [Test]
        public void HandleCallCreateColonyTest()
        {
            // Arrange
            var manager = Substitute.For <IColonySourceManager>();

            var sut = new CreateColonyHandler(Substitute.For <ISelkieBus>(),
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
                              CostPerFeature = new[]
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
                                                                   x.CostPerFeature == message.CostPerFeature));
        }

        [Test]
        public void HandleSendsMessageTest()
        {
            // Arrange
            var bus = Substitute.For <ISelkieBus>();

            var sut = new CreateColonyHandler(bus,
                                              Substitute.For <IColonySourceManager>());

            // Act
            sut.Handle(new CreateColonyMessage());

            // Assert
            bus.Received().PublishAsync(Arg.Any <CreatedColonyMessage>());
        }
    }
}