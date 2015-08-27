using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;

namespace Selkie.Services.Aco.Handlers
{
    public sealed class PheromonesRequestHandler
        : SelkieMessageHandler <PheromonesRequestMessage>
    {
        private readonly ISelkieBus m_Bus;
        private readonly IColonySourceManager m_Manager;

        public PheromonesRequestHandler([NotNull] ISelkieBus bus,
                                        [NotNull] IColonySourceManager manager)
        {
            m_Bus = bus;
            m_Manager = manager;
        }

        public override void Handle(PheromonesRequestMessage message)
        {
            IServiceColony colony = m_Manager.Source;

            var reply = new PheromonesMessage
                        {
                            Average = colony.PheromonesAverage,
                            Maximum = colony.PheromonesMaximum,
                            Minimum = colony.PheromonesMinimum,
                            Values = colony.PheromonesToArray()
                        };

            m_Bus.PublishAsync(reply);
        }
    }
}