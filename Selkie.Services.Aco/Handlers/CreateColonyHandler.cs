using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Windsor;

namespace Selkie.Services.Aco.Handlers
{
    [ProjectComponent(Lifestyle.Startable)]
    public class CreateColonyHandler
        : BaseHandler <CreateColonyMessage>,
          ICreateColonyHandler
    {
        public CreateColonyHandler([NotNull] ILogger logger,
                                   [NotNull] IBus bus,
                                   [NotNull] IColonySourceManager manager)
            : base(logger,
                   bus,
                   manager)
        {
        }

        internal override void Handle(CreateColonyMessage message)
        {
            var parameters = new ServiceColonyParameters
                             {
                                 CostMatrix = message.CostMatrix,
                                 CostPerLine = message.CostPerLine
                             };

            IServiceColony colony = Manager.CreateColony(parameters);

            Manager.UpdateSource(colony);

            Bus.PublishAsync(new CreatedColonyMessage());
        }
    }
}