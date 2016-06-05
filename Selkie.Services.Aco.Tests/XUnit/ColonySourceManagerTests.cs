using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.Services.Aco.TypedFactories;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.Services.Aco.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class ColonySourceManagerTests
    {
        [Theory]
        [AutoNSubstituteData]
        // ReSharper disable once TooManyArguments
        public void CreateColonyTest([NotNull] [Frozen] IServiceColony colony,
                                     [NotNull] IServiceColonyParameters parameters,
                                     [NotNull] ColonySourceManager sut)
        {
            // Arrange
            // Act
            IServiceColony actual = sut.CreateColony(parameters);

            // Assert
            Assert.Equal(colony,
                         actual);
        }

        [Theory]
        [AutoNSubstituteData]
        public void SourceDefaultTest([NotNull] ColonySourceManager sut)
        {
            Assert.NotNull(sut.Source);
        }

        [Theory]
        [AutoNSubstituteData]
        public void UpdateSourceCallsReleaseTest([NotNull] IServiceColony colony,
                                                 [NotNull] [Frozen] IServiceColonyFactory factory,
                                                 [NotNull] ColonySourceManager sut)
        {
            // Arrange
            // Act
            sut.UpdateSource(colony);

            // Assert
            factory.Received().Release(Arg.Any <IServiceColony>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void UpdateSourceUpdatesSourceTest([NotNull] IServiceColony colony,
                                                  [NotNull] ColonySourceManager sut)
        {
            // Arrange
            // Act
            sut.UpdateSource(colony);

            // Assert
            Assert.Equal(colony,
                         sut.Source);
        }
    }
}