using JetBrains.Annotations;

namespace Selkie.Services.Aco
{
    public interface IServiceColony
    {
        bool IsRunning { get; }
        double PheromonesMinimum { get; }
        double PheromonesMaximum { get; }
        double PheromonesAverage { get; }

        [NotNull]
        double[][] PheromonesToArray();

        void Start(int times);

        void Stop();
    }
}