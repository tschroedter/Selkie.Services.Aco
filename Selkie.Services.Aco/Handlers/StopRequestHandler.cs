using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;

namespace Selkie.Services.Aco.Handlers
{
    [Interceptor(typeof ( MessageHandlerAspect ))]
    public class StopRequestHandler
        : SelkieMessageHandler <StopRequestMessage>
    {
        private readonly IColonySourceManager m_Manager;

        public StopRequestHandler([NotNull] IColonySourceManager manager)
        {
            m_Manager = manager;
        }

        public override void Handle(StopRequestMessage message)
        {
            m_Manager.Source.Stop();
        }
    }
}