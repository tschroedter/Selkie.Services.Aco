﻿using System;
using System.Diagnostics.CodeAnalysis;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.Aco.Anthill;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Common;
using Selkie.Common;
using Selkie.Services.Aco.Common.Messages;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.Services.Aco.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class ServiceColonyTests
    {
        private const double Tolerance = 0.01;

        [Fact]
        public void ColonyIsNotNullTest()
        {
            // Arrange
            // Act
            ServiceColony sut = CreateSut();

            // Assert
            Assert.NotNull(sut.Colony);
        }

        private static ServiceColony CreateSut()
        {
            return new ServiceColony(Substitute.For <IDisposer>(),
                                     Substitute.For <IBus>(),
                                     Substitute.For <IColonyFactory>(),
                                     Substitute.For <IDistanceGraphFactory>(),
                                     Substitute.For <IServiceColonyParameters>());
        }

        [Fact]
        public void AdapterSubscribesToBestTrailChangedTest()
        {
            // Arrange
            var colony = Substitute.For <IColony>();
            var factory = Substitute.For <IColonyFactory>();
            factory.Create(Arg.Any <IDistanceGraph>()).Returns(colony);

            // Act
            var sut = new ServiceColony(Substitute.For <IDisposer>(),
                                        Substitute.For <IBus>(),
                                        factory,
                                        Substitute.For <IDistanceGraphFactory>(),
                                        Substitute.For <IServiceColonyParameters>());

            // Assert
            colony.Received().BestTrailChanged += sut.OnBestTrailChanged;
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnBestTrailChangedSendsMessageTest([NotNull] IBus bus,
                                                       [NotNull] BestTrailChangedEventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSutGivenBus(bus);

            // Act
            sut.OnBestTrailChanged(this,
                                   eventArgs);

            // Assert
            bus.PublishAsync(Arg.Is <BestTrailMessage>(x => IsMatchBestTrailMessage(eventArgs,
                                                                                    x)));
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnStoppedSendsMessageTest([NotNull] IBus bus,
                                              [NotNull] EventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSutGivenBus(bus);

            // Act
            sut.OnStopped(this,
                          eventArgs);

            // Assert
            bus.PublishAsync(Arg.Any <StoppedMessage>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnStartedSendsMessageTest([NotNull] IBus bus,
                                              [NotNull] EventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSutGivenBus(bus);

            // Act
            sut.OnStarted(this,
                          eventArgs);

            // Assert
            bus.PublishAsync(Arg.Any <StartedMessage>());
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

        private static bool IsMatchBestTrailMessage(BestTrailChangedEventArgs eventArgs,
                                                    BestTrailMessage x)
        {
            // ReSharper disable once PossibleUnintendedReferenceComparison
            return Math.Abs(x.Alpha - eventArgs.Alpha) < Tolerance && Math.Abs(x.Beta - eventArgs.Beta) < Tolerance &&
                   Math.Abs(x.Gamma - eventArgs.Gamma) < Tolerance && x.Iteration == eventArgs.Iteration &&
                   Math.Abs(x.Length - eventArgs.Length) < Tolerance && x.Trail == eventArgs.Trail &&
                   x.Type == eventArgs.AntType;
        }

        [Theory]
        [AutoNSubstituteData]
        public void OnFinishedChangedSendsMessageTest([NotNull] IBus bus,
                                                      [NotNull] FinishedEventArgs eventArgs)
        {
            // Arrange
            ServiceColony sut = CreateSutGivenBus(bus);

            // Act
            sut.OnFinished(this,
                           eventArgs);

            // Assert
            bus.PublishAsync(Arg.Is <FinishedMessage>(x => IsMatchFinishedMessage(eventArgs,
                                                                                  x)));
        }

        private static bool IsMatchFinishedMessage(FinishedEventArgs eventArgs,
                                                   FinishedMessage x)
        {
            return x.FinishTime == eventArgs.FinishTime && x.StartTime == eventArgs.StartTime &&
                   x.Times == eventArgs.Times;
        }

        private static ServiceColony CreateSutGivenBus([NotNull] IBus bus)
        {
            return new ServiceColony(Substitute.For <IDisposer>(),
                                     bus,
                                     Substitute.For <IColonyFactory>(),
                                     Substitute.For <IDistanceGraphFactory>(),
                                     Substitute.For <IServiceColonyParameters>());
        }

        [Fact]
        public void DisposeCallsDisposeTest()
        {
            // Arrange
            var disposer = Substitute.For <IDisposer>();

            var sut = new ServiceColony(disposer,
                                        Substitute.For <IBus>(),
                                        Substitute.For <IColonyFactory>(),
                                        Substitute.For <IDistanceGraphFactory>(),
                                        Substitute.For <IServiceColonyParameters>());

            // Act
            sut.Dispose();

            // Assert
            disposer.Received().Dispose();
        }

        [Fact]
        public void DisposeDoesNotCallsDisposeIfDisposedTest()
        {
            // Arrange
            var disposer = Substitute.For <IDisposer>();
            disposer.IsDisposed.Returns(true);

            var sut = new ServiceColony(disposer,
                                        Substitute.For <IBus>(),
                                        Substitute.For <IColonyFactory>(),
                                        Substitute.For <IDistanceGraphFactory>(),
                                        Substitute.For <IServiceColonyParameters>());

            // Act
            sut.Dispose();

            // Assert
            disposer.DidNotReceive().Dispose();
        }

        [Theory]
        [AutoNSubstituteData]
        public void DPheromonesMinimumTest([NotNull] [Frozen] IColony colony,
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
        public void StopCallsColonyStopTest([NotNull] [Frozen] IColony colony,
                                            [NotNull] ServiceColony sut)
        {
            // Arrange
            // Act
            sut.Stop();

            // Assert
            colony.Received().Stop();
        }

        [Theory]
        [AutoNSubstituteData]
        public void StartCallsColonyStopTest([NotNull] [Frozen] IColony colony,
                                             [NotNull] ServiceColony sut)
        {
            // Arrange
            // Act
            sut.Start(1);

            // Assert
            colony.Received().Start(1);
        }
    }
}