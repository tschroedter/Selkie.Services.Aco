using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.Aco.Anthill;
using Selkie.Aco.Anthill.Interfaces;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Common.Interfaces;
using Selkie.Common.Interfaces;
using Selkie.EasyNetQ;
using Selkie.NUnit.Extensions;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Windsor;

namespace Selkie.Services.Aco.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ServiceColonyTests
    {
        private const double Tolerance = 0.01;

        [Theory]
        [AutoNSubstituteData]
        public void CheckColonyIdBeforeExecute_Execute_ForMatchingColonyIds(
            [NotNull] Guid colonyId,
            [NotNull] [Frozen] ServiceColony sut)
        {
            // Arrange
            var wasExecuted = false;
            var action = new Action(() => wasExecuted = true);

            // Act
            sut.CheckColonyIdBeforeExecute(colonyId,
                                           colonyId,
                                           action);

            // Assert
            Assert.True(wasExecuted);
        }

        [Theory]
        [AutoNSubstituteData]
        public void CheckColonyIdBeforeExecute_DoesNotExecute_ForNotMatchingColonyIds(
            [NotNull] Guid colonyIdOne,
            [NotNull] Guid colonyIdTwo,
            [NotNull] [Frozen] ServiceColony sut)
        {
            // Arrange
            var wasExecuted = false;
            var action = new Action(() => wasExecuted = true);

            // Act
            sut.CheckColonyIdBeforeExecute(colonyIdOne,
                                           colonyIdTwo,
                                           action);

            // Assert
            Assert.False(wasExecuted);
        }

        [Theory]
        [AutoNSubstituteData]
        public void ConstructorCallsAntSettingsFactoryTest([NotNull] [Frozen] IAntSettingsFactory factory,
                                                           [NotNull] [Frozen] IServiceColonyParameters parameters,
                                                           [NotNull] ServiceColony sut)
        {
            // Arrange
            // Act
            // Assert
            factory.Received().Create(parameters.IsFixedStartNode,
                                      parameters.FixedStartNode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void ConstructorCallsColonyFactoryTest([NotNull] [Frozen] IColonyFactory factory,
                                                      [NotNull] [Frozen] IDistanceGraph graph,
                                                      [NotNull] [Frozen] IAntSettings antSettings,
                                                      [NotNull] ServiceColony sut)
        {
            // Arrange
            // Act
            // Assert
            factory.Received().Create(graph,
                                      antSettings);
        }

        [Theory]
        [AutoNSubstituteData]
        public void ColonyIdTest([NotNull] [Frozen] IServiceColonyParameters parameters,
                                 [NotNull] ServiceColony sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(parameters.ColonyId,
                            sut.ColonyId);
        }

        [Theory]
        [AutoNSubstituteData]
        public void PheromonesMinimumTest([NotNull] [Frozen] IColony colony,
                                          [NotNull] ServiceColony sut)
        {
            // Arrange
            colony.PheromonesMinimum.Returns(123.0);

            // Act
            double actual = sut.PheromonesMinimum;

            // Assert
            Assert.True(Math.Abs(123.0 - actual) < Tolerance);
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnBestTrailChangedSendsMessageUsingEventsValuesTest(
            [NotNull] ISelkieBus bus,
            [NotNull] IServiceColonyParameters parameters,
            [NotNull] BestTrailChangedEventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSutGivenBusAndParameters(bus,
                                                               parameters);

            // Act
            sut.OnBestTrailChanged(this,
                                   eventArgs);

            // Assert
            bus.Received().PublishAsync(Arg.Is <BestTrailMessage>(x => IsMatchBestTrailMessage(sut.ColonyId,
                                                                                               eventArgs,
                                                                                               x)));
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnFinishedChangedSendsMessageTest(
            [NotNull] ISelkieBus bus,
            [NotNull] IServiceColonyParameters parameters,
            [NotNull] FinishedEventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSutGivenBusAndParameters(bus,
                                                               parameters);

            // Act
            sut.OnFinished(this,
                           eventArgs);

            // Assert
            bus.Received().PublishAsync(Arg.Is <FinishedMessage>(x => IsMatchFinishedMessage(sut.ColonyId,
                                                                                             eventArgs,
                                                                                             x)));
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnFinishedSetsIsRunningToFalseTest([NotNull] ISelkieBus bus,
                                                       [NotNull] EventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSutGivenBus(bus);
            SetIsRunningToTrue(sut);

            // Act
            sut.OnFinished(this,
                           new FinishedEventArgs());

            // Assert
            Assert.False(sut.IsRunning);
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnStartedSendsMessageTest(
            [NotNull] ISelkieBus bus,
            [NotNull] IServiceColonyParameters parameters,
            [NotNull] EventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSutGivenBusAndParameters(bus,
                                                               parameters);

            // Act
            sut.OnStarted(this,
                          eventArgs);

            // Assert
            bus.Received().PublishAsync(Arg.Is <StartedMessage>(x => x.ColonyId == parameters.ColonyId));
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnStartedSetsIsRunningToTrueTest([NotNull] EventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSut();

            // Act
            sut.OnStarted(this,
                          eventArgs);

            // Assert
            Assert.True(sut.IsRunning);
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnStoppedSendsMessageTest(
            [NotNull] ISelkieBus bus,
            [NotNull] IServiceColonyParameters parameters,
            [NotNull] EventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSutGivenBusAndParameters(bus,
                                                               parameters);

            // Act
            sut.OnStopped(this,
                          eventArgs);

            // Assert
            bus.Received().PublishAsync(Arg.Is <StoppedMessage>(x => x.ColonyId == parameters.ColonyId));
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnStoppedSetsIsRunningToFalseTest([NotNull] ISelkieBus bus,
                                                      [NotNull] EventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSutGivenBus(bus);
            SetIsRunningToTrue(sut);

            // Act
            sut.OnStopped(this,
                          eventArgs);

            // Assert
            Assert.False(sut.IsRunning);
        }

        [Theory]
        [AutoNSubstituteData]
        public void PheromonesAverageTest([NotNull] [Frozen] IColony colony,
                                          [NotNull] ServiceColony sut)
        {
            // Arrange
            colony.PheromonesAverage.Returns(1.0);

            // Act
            double actual = sut.PheromonesAverage;

            // Assert
            Assert.True(Math.Abs(actual - colony.PheromonesAverage) < Tolerance);
        }

        [Theory]
        [AutoNSubstituteData]
        public void PheromonesMaximumTest([NotNull] [Frozen] IColony colony,
                                          [NotNull] ServiceColony sut)
        {
            // Arrange
            colony.PheromonesMaximum.Returns(1.0);

            // Act
            double actual = sut.PheromonesMaximum;

            // Assert
            Assert.True(Math.Abs(actual - colony.PheromonesMaximum) < Tolerance);
        }

        [Theory]
        [AutoNSubstituteData]
        public void PheromonesToArrayTest([NotNull] [Frozen] IColony colony,
                                          [NotNull] ServiceColony sut)
        {
            // Arrange
            double[][] array =
            {
                new[]
                {
                    1.0
                }
            };

            colony.PheromonesToArray().Returns(array);

            // Act
            double[][] actual = sut.PheromonesToArray();

            // Assert
            Assert.True(array == actual);
        }

        [Theory]
        [AutoNSubstituteData]
        public void StartCallsColonyStartTest(
            [NotNull] [Frozen] IColony colony,
            [NotNull] [Frozen] ServiceColony sut)
        {
            // Arrange
            // Act
            sut.Start(sut.ColonyId,
                      1);

            // Assert
            colony.Received().Start(1);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Start_DoesNotCallStart_ForNotMatchingColonyIds(
            [NotNull] [Frozen] IColony colony,
            [NotNull] [Frozen] ISelkieBus bus,
            [NotNull] [Frozen] IServiceColonyParameters parameter)
        {
            // Arrange
            Guid notMatchingColonyId = Guid.NewGuid();

            ServiceColony sut = CreateSutGivenBusAndParameters(bus,
                                                               parameter);

            // Act
            sut.Start(notMatchingColonyId,
                      1);

            // Assert
            colony.DidNotReceiveWithAnyArgs().Start(1);
        }

        [Theory]
        [AutoNSubstituteData]
        public void StopCallsColonyStopTest([NotNull] [Frozen] IColony colony,
                                            [NotNull] ServiceColony sut)
        {
            // Arrange
            // Act
            sut.Stop(sut.ColonyId);

            // Assert
            colony.Received().Stop();
        }

        private static ServiceColony CreateSut()
        {
            return CreateSut(Substitute.For <IDisposer>());
        }

        private static ServiceColony CreateSut(IDisposer disposer)
        {
            var sut = new ServiceColony(disposer,
                                        Substitute.For <ISelkieLogger>(),
                                        Substitute.For <ISelkieBus>(),
                                        Substitute.For <IColonyFactory>(),
                                        Substitute.For <IDistanceGraphFactory>(),
                                        Substitute.For <IAntSettingsFactory>(),
                                        Substitute.For <IServiceColonyParameters>());
            return sut;
        }

        private static ServiceColony CreateSutGivenBus(
            [NotNull] ISelkieBus bus)
        {
            return CreateSutGivenBusAndParameters(bus,
                                                  Substitute.For <IServiceColonyParameters>());
        }

        private static ServiceColony CreateSutGivenBusAndParameters(
            [NotNull] ISelkieBus bus,
            [NotNull] IServiceColonyParameters parameters)
        {
            return new ServiceColony(Substitute.For <IDisposer>(),
                                     Substitute.For <ISelkieLogger>(),
                                     bus,
                                     Substitute.For <IColonyFactory>(),
                                     Substitute.For <IDistanceGraphFactory>(),
                                     Substitute.For <IAntSettingsFactory>(),
                                     parameters);
        }

        private static bool IsMatchBestTrailMessage(
            Guid colonyId,
            BestTrailChangedEventArgs eventArgs,
            BestTrailMessage x)
        {
            return x.ColonyId == colonyId &&
                   Math.Abs(x.Alpha - eventArgs.Alpha) < Tolerance && Math.Abs(x.Beta - eventArgs.Beta) < Tolerance &&
                   Math.Abs(x.Gamma - eventArgs.Gamma) < Tolerance && x.Iteration == eventArgs.Iteration &&
                   Math.Abs(x.Length - eventArgs.Length) < Tolerance && x.Trail == eventArgs.Trail &&
                   x.Type == eventArgs.AntType;
        }

        private static bool IsMatchFinishedMessage(
            Guid colonyId,
            FinishedEventArgs eventArgs,
            FinishedMessage x)
        {
            return x.ColonyId == colonyId &&
                   x.FinishTime == eventArgs.FinishTime && x.StartTime == eventArgs.StartTime &&
                   x.Times == eventArgs.Times;
        }

        private void SetIsRunningToTrue(ServiceColony sut)
        {
            sut.OnStarted(this,
                          new EventArgs());

            Assert.True(sut.IsRunning);
        }

        [Test]
        public void AdapterSubscribesToBestTrailChangedTest()
        {
            // Arrange
            var colony = Substitute.For <IColony>();
            var factory = Substitute.For <IColonyFactory>();

            factory.Create(Arg.Any <IDistanceGraph>(),
                           Arg.Any <IAntSettings>()).Returns(colony);

            // Act
            var sut = new ServiceColony(Substitute.For <IDisposer>(),
                                        Substitute.For <ISelkieLogger>(),
                                        Substitute.For <ISelkieBus>(),
                                        factory,
                                        Substitute.For <IDistanceGraphFactory>(),
                                        Substitute.For <IAntSettingsFactory>(),
                                        Substitute.For <IServiceColonyParameters>());

            // Assert
            colony.Received().BestTrailChanged += sut.OnBestTrailChanged;
        }

        [Test]
        public void ColonyIsNotNullTest()
        {
            // Arrange
            // Act
            ServiceColony sut = CreateSut();

            // Assert
            Assert.NotNull(sut.Colony);
        }

        [Test]
        public void DisposeCallsDisposeTest()
        {
            // Arrange
            var disposer = Substitute.For <IDisposer>();

            ServiceColony sut = CreateSut(disposer);

            // Act
            sut.Dispose();

            // Assert
            disposer.Received().Dispose();
        }

        [Test]
        public void DisposeDoesNotCallsDisposeIfDisposedTest()
        {
            // Arrange
            var disposer = Substitute.For <IDisposer>();
            disposer.IsDisposed.Returns(true);

            ServiceColony sut = CreateSut(disposer);

            // Act
            sut.Dispose();

            // Assert
            disposer.DidNotReceive().Dispose();
        }
    }
}