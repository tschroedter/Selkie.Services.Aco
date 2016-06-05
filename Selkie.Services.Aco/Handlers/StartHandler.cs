using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;

namespace Selkie.Services.Aco.Handlers
{
    [Interceptor(typeof( MessageHandlerAspect ))]
    public class StartHandler
        : SelkieMessageHandler <StartMessage>
    {
        public StartHandler([NotNull] IColonySourceManager manager)
        {
            m_Manager = manager;
        }

        private readonly IColonySourceManager m_Manager;

        public override void Handle(StartMessage message)
        {
            m_Manager.Source.Start(message.Times);
        }
    }
}