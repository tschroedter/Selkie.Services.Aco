using System;
using Selkie.EasyNetQ.Extensions;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Windsor.Extensions;
using TechTalk.SpecFlow;

// ReSharper disable once CheckNamespace

namespace Selkie.Services.Aco.Specflow.Steps.Common
{
    public partial class ServiceHandlers
    {
        public void SubscribeOther()
        {
            m_Bus.SubscribeHandlerAsync <CreatedColonyMessage>(m_Logger,
                                                               GetType().FullName,
                                                               CreatedColonyHandler);

            m_Bus.SubscribeHandlerAsync <StartedMessage>(m_Logger,
                                                         GetType().FullName,
                                                         StartedHandler);

            m_Bus.SubscribeHandlerAsync <BestTrailMessage>(m_Logger,
                                                           GetType().FullName,
                                                           BestTrailHandler);

            m_Bus.SubscribeHandlerAsync <FinishedMessage>(m_Logger,
                                                          GetType().FullName,
                                                          FinishedHandler);
        }

        private void BestTrailHandler(BestTrailMessage message)
        {
            ScenarioContext.Current [ "IsReceivedBestTrailMessage" ] = true;

            string trailText = string.Join(",",
                                           message.Trail);

            Console.WriteLine("Iteration {0}: Length = {1} Trail = {2}".Inject(message.Iteration,
                                                                               message.Length,
                                                                               trailText));
        }

        private void StartedHandler(StartedMessage message)
        {
            ScenarioContext.Current [ "IsReceivedStartedMessage" ] = true;
        }

        private void FinishedHandler(FinishedMessage message)
        {
            ScenarioContext.Current [ "IsReceivedFinishedMessage" ] = true;
        }

        private void CreatedColonyHandler(CreatedColonyMessage message)
        {
            ScenarioContext.Current [ "IsReceivedCreatedColonyMessage" ] = true;
        }
    }
}