using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Aco.TypedFactories;

namespace Selkie.Services.Aco.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ColonySourceManagerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void CreateColonyTest([NotNull] [Frozen] IServiceColony colony,
                                     [NotNull] IServiceColonyParameters parameters,
                                     [NotNull] ColonySourceManager sut)
        {
            // Arrange
            // Act
            IServiceColony actual = sut.CreateColony(parameters);

            // Assert
            Assert.AreEqual(colony,
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
            Assert.AreEqual(colony,
                            sut.Source);
        }
    }
}