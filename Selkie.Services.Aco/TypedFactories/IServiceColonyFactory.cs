using JetBrains.Annotations;
using Selkie.Windsor;

namespace Selkie.Services.Aco.TypedFactories
{
    public interface IServiceColonyFactory : ITypedFactory
    {
        IServiceColony Create([NotNull] IServiceColonyParameters parameters);

        [UsedImplicitly]
        void Release([NotNull] IServiceColony colony);
    }
}