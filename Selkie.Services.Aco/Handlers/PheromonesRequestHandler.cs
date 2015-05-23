using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Windsor;

namespace Selkie.Services.Aco.Handlers
{
    [ProjectComponent(Lifestyle.Startable)]
    public sealed class PheromonesRequestHandler
        : BaseHandler <PheromonesRequestMessage>,
          IPheromonesRequestHandler
    {
        public PheromonesRequestHandler([NotNull] ILogger logger,
                                        [NotNull] IBus bus,
                                        [NotNull] IColonySourceManager manager)
            : base(logger,
                   bus,
                   manager)
        {
        }

        internal override void Handle(PheromonesRequestMessage message)
        {
            IServiceColony colony = Manager.Source;

            var reply = new PheromonesMessage
                        {
                            Average = colony.PheromonesAverage,
                            Maximum = colony.PheromonesMaximum,
                            Minimum = colony.PheromonesMinimum,
                            Values = colony.PheromonesToArray()
                        };

            Bus.PublishAsync(reply);
        }
    }
}