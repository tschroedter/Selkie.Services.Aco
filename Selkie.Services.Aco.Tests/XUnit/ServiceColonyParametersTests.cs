using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Assert = Xunit.Assert;

namespace Selkie.Services.Aco.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class ServiceColonyParametersTests
    {
        [Theory]
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

        [Theory]
        public void CostPerLineRoundtripTest()
        {
            // Arrange
            var sut = new ServiceColonyParameters();

            int[] costPerLine =
            {
                1,
                2
            };

            // Act
            sut.CostPerLine = costPerLine;

            // Assert
            Assert.Equal(costPerLine,
                         sut.CostPerLine);
        }
    }
}