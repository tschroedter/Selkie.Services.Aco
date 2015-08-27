using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;

namespace Selkie.Services.Aco.Handlers
{
    public sealed class StartHandler
        : SelkieMessageHandler <StartMessage>
    {
        private readonly IColonySourceManager m_Manager;

        public StartHandler([NotNull] IColonySourceManager manager)
        {
            m_Manager = manager;
        }

        public override void Handle(StartMessage message)
        {
            m_Manager.Source.Start(message.Times);
        }
    }
}