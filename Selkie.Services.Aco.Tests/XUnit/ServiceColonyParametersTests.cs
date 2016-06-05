using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Selkie.Services.Aco.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class ServiceColonyParametersTests
    {
        [Fact]
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
            Assert.Equal(costMatrix,
                         sut.CostMatrix);
        }

        [Fact]
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
            Assert.Equal(costPerFeature,
                         sut.CostPerFeature);
        }
    }
}