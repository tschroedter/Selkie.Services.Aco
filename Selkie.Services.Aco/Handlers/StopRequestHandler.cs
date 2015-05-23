using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Windsor;

namespace Selkie.Services.Aco.Handlers
{
    [ProjectComponent(Lifestyle.Startable)]
    public sealed class StopRequestHandler
        : BaseHandler <StopRequestMessage>,
          IStopRequestHandler
    {
        public StopRequestHandler([NotNull] ILogger logger,
                                  [NotNull] IBus bus,
                                  [NotNull] IColonySourceManager manager)
            : base(logger,
                   bus,
                   manager)
        {
        }

        internal override void Handle(StopRequestMessage message)
        {
            Manager.Source.Stop();
        }
    }
}