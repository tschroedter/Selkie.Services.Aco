using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.Services.Common;
using Selkie.Services.Common.Messages;
using Selkie.Windsor;

namespace Selkie.Services.Aco
{
    [ProjectComponent(Lifestyle.Singleton)]
    public class AcoService
        : BaseService,
          IAcoService
    {
        public const string ServiceName = "Aco Service";

        public AcoService([NotNull] ISelkieBus bus,
                          [NotNull] ISelkieLogger logger,
                          [NotNull] ISelkieManagementClient client)
            : base(bus,
                   logger,
                   client,
                   ServiceName)
        {
        }

        protected override void ServiceStart()
        {
            var message = new ServiceStartedResponseMessage
                          {
                              ServiceName = ServiceName
                          };

            Bus.Publish(message);
        }

        protected override void ServiceStop()
        {
            var message = new ServiceStoppedResponseMessage
                          {
                              ServiceName = ServiceName
                          };

            Bus.Publish(message);
        }

        protected override void ServiceInitialize()
        {
        }
    }
}