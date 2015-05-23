using JetBrains.Annotations;

namespace Selkie.Services.Aco
{
    public interface IServiceColonyParameters
    {
        [NotNull]
        int[][] CostMatrix { get; set; }

        [NotNull]
        int[] CostPerLine { get; set; }
    }
}