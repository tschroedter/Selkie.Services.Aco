using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.Services.Aco.TypedFactories;
using Selkie.Windsor;

namespace Selkie.Services.Aco
{
    [Interceptor(typeof( MessageHandlerAspect ))]
    [ProjectComponent(Lifestyle.Singleton)]
    public class ColonySourceManager : IColonySourceManager
    {
        public ColonySourceManager([NotNull] IServiceColonyFactory factory,
                                   [NotNull] IServiceColonyParameters parameters)
        {
            m_Factory = factory;
            Source = m_Factory.Create(parameters);
        }

        internal const int DefaultRuntime = 1000;
        private readonly IServiceColonyFactory m_Factory;

        public IServiceColony Source { get; private set; }

        public void UpdateSource(IServiceColony colony)
        {
            m_Factory.Release(Source);

            Source = colony;
        }

        public IServiceColony CreateColony(IServiceColonyParameters parameters)
        {
            IServiceColony colony = m_Factory.Create(parameters);

            return colony;
        }
    }
}