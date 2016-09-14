using System;
using JetBrains.Annotations;

namespace Selkie.Services.Aco
{
    public interface IServiceColony
    {
        bool IsRunning { get; }
        double PheromonesMinimum { get; }
        double PheromonesMaximum { get; }
        double PheromonesAverage { get; }
        Guid ColonyId { get; }

        [NotNull]
        double[][] PheromonesToArray();

        void Start(Guid colonyId,
                   int times);

        void Stop(Guid colonyId);
    }
}