using JetBrains.Annotations;

namespace Selkie.Services.Aco
{
    public interface IColonySourceManager
    {
        [NotNull]
        IServiceColony Source { get; }

        [NotNull]
        IServiceColony CreateColony([NotNull] IServiceColonyParameters parameters);

        void UpdateSource([NotNull] IServiceColony colony);
    }
}