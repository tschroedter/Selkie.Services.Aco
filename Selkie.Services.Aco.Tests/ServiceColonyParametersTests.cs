using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Selkie.Services.Aco.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ServiceColonyParametersTests
    {
        [Test]
        public void CostMatrixRoundtripTest()
        {
            // Arrange
            var sut = new ServiceColonyParameters();

            int[][] costMatrix =
            {
                new[]
                {
                    1,
                    2
                }
            };

            // Act
            sut.CostMatrix = costMatrix;

            // Assert
            Assert.AreEqual(costMatrix,
                            sut.CostMatrix);
        }

        [Test]
        public void CostPerFeatureRoundtripTest()
        {
            // Arrange
            var sut = new ServiceColonyParameters();

            int[] costPerFeature =
            {
                1,
                2
            };

            // Act
            sut.CostPerFeature = costPerFeature;

            // Assert
            Assert.AreEqual(costPerFeature,
                            sut.CostPerFeature);
        }
    }
}