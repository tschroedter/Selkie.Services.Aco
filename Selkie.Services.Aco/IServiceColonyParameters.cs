using JetBrains.Annotations;
using Selkie.Aco.Anthill;

namespace Selkie.Services.Aco
{
    public interface IServiceColonyParameters
    {
        [NotNull]
        int[][] CostMatrix { get; set; }

        [NotNull]
        int[] CostPerLine { get; set; }

        AntSettings.TrailStartNodeType IsFixedStartNode { get; set; }

        int FixedStartNode { get; set; }
    }
}