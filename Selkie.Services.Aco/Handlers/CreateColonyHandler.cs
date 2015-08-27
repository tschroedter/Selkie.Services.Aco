using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;

namespace Selkie.Services.Aco.Handlers
{
    public class CreateColonyHandler
        : SelkieMessageHandler <CreateColonyMessage>
    {
        private readonly ISelkieBus m_Bus;
        private readonly IColonySourceManager m_Manager;

        public CreateColonyHandler([NotNull] ISelkieBus bus,
                                   [NotNull] IColonySourceManager manager)
        {
            m_Bus = bus;
            m_Manager = manager;
        }

        public override void Handle(CreateColonyMessage message)
        {
            var parameters = new ServiceColonyParameters
                             {
                                 CostMatrix = message.CostMatrix,
                                 CostPerLine = message.CostPerLine
                             };

            IServiceColony colony = m_Manager.CreateColony(parameters);

            m_Manager.UpdateSource(colony);

            m_Bus.PublishAsync(new CreatedColonyMessage());
        }
    }
}