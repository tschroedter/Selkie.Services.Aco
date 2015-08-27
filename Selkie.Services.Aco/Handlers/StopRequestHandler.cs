using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;

namespace Selkie.Services.Aco.Handlers
{
    public sealed class StopRequestHandler
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