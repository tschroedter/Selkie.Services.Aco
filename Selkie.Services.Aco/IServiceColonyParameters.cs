using JetBrains.Annotations;
using Selkie.Aco.Anthill;

namespace Selkie.Services.Aco
{
    public interface IServiceColonyParameters
    {
        [NotNull]
        int[][] CostMatrix { get; set; }

        [NotNull]
        int[] CostPerFeature { get; set; }

        AntSettings.TrailStartNodeType IsFixedStartNode { get; set; }

        int FixedStartNode { get; set; }
    }
}