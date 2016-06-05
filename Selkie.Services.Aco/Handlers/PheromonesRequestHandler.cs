using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;

namespace Selkie.Services.Aco.Handlers
{
    [Interceptor(typeof( MessageHandlerAspect ))]
    public class PheromonesRequestHandler
        : SelkieMessageHandler <PheromonesRequestMessage>
    {
        public PheromonesRequestHandler([NotNull] ISelkieBus bus,
                                        [NotNull] IColonySourceManager manager)
        {
            m_Bus = bus;
            m_Manager = manager;
        }

        private readonly ISelkieBus m_Bus;
        private readonly IColonySourceManager m_Manager;

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