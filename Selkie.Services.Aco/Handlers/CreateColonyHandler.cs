using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aco.Anthill;
using Selkie.Aop.Aspects;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;

namespace Selkie.Services.Aco.Handlers
{
    [Interceptor(typeof( MessageHandlerAspect ))]
    public class CreateColonyHandler
        : SelkieMessageHandler <CreateColonyMessage>
    {
        public CreateColonyHandler([NotNull] ISelkieBus bus,
                                   [NotNull] IColonySourceManager manager)
        {
            m_Bus = bus;
            m_Manager = manager;
        }

        private readonly ISelkieBus m_Bus;
        private readonly IColonySourceManager m_Manager;

        public override void Handle(CreateColonyMessage message)
        {
            AntSettings.TrailStartNodeType trailStartNodeType = message.IsFixedStartNode
                                                                    ? AntSettings.TrailStartNodeType.Fixed
                                                                    : AntSettings.TrailStartNodeType.Random;
            var parameters = new ServiceColonyParameters
                             {
                                 CostMatrix = message.CostMatrix,
                                 CostPerFeature = message.CostPerFeature,
                                 IsFixedStartNode = trailStartNodeType,
                                 FixedStartNode = message.FixedStartNode
                             };

            IServiceColony colony = m_Manager.CreateColony(parameters);

            m_Manager.UpdateSource(colony);

            m_Bus.PublishAsync(new CreatedColonyMessage());
        }
    }
}