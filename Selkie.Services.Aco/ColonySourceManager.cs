using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.Services.Aco.TypedFactories;
using Selkie.Windsor;

namespace Selkie.Services.Aco
{
    [Interceptor(typeof(MessageHandlerAspect))]
    [ProjectComponent(Lifestyle.Singleton)]
    public class ColonySourceManager : IColonySourceManager
    {
        internal const int DefaultRuntime = 1000;
        private readonly IServiceColonyFactory m_Factory;

        public ColonySourceManager([NotNull] IServiceColonyFactory factory,
                                   [NotNull] IServiceColonyParameters parameters)
        {
            m_Factory = factory;
            Source = m_Factory.Create(parameters);
        }

        public IServiceColony Source { get; private set; }

        public void UpdateSource(IServiceColony colony)
        {
            m_Factory.Release(Source);

            Source = colony;
        }

        public IServiceColony CreateColony(IServiceColonyParameters parameters)
        {
            IServiceColony adapter = m_Factory.Create(parameters);

            return adapter;
        }
    }
}