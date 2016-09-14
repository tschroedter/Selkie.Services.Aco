using System;
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
            m_Bus.SubscribeAsync <CreatedColonyMessage>(GetType().FullName,
                                                        CreatedColonyHandler);

            m_Bus.SubscribeAsync <StartedMessage>(GetType().FullName,
                                                  StartedHandler);

            m_Bus.SubscribeAsync <BestTrailMessage>(GetType().FullName,
                                                    BestTrailHandler);

            m_Bus.SubscribeAsync <FinishedMessage>(GetType().FullName,
                                                   FinishedHandler);
        }

        private void BestTrailHandler(BestTrailMessage message)
        {
            ScenarioContext.Current [ "ColonyId_ReceivedBestTrailMessage" ] = message.ColonyId;
            ScenarioContext.Current [ "IsReceivedBestTrailMessage" ] = true;

            string trailText = string.Join(",",
                                           message.Trail);

            Console.WriteLine("[ColonyId: {0}] Iteration {1}: Length = {2} Trail = {3}".Inject(message.ColonyId,
                                                                                               message.Iteration,
                                                                                               message.Length,
                                                                                               trailText));
        }

        private void CreatedColonyHandler(CreatedColonyMessage message)
        {
            Console.WriteLine("[ColonyId: {0}] Created colony!".Inject(message.ColonyId));

            ScenarioContext.Current [ "ColonyId_ReceivedCreatedColonyMessage" ] = message.ColonyId;
            ScenarioContext.Current [ "IsReceivedCreatedColonyMessage" ] = true;
        }

        private void FinishedHandler(FinishedMessage message)
        {
            Console.WriteLine("[ColonyId: {0}] Finished colony with!".Inject(message.ColonyId));

            ScenarioContext.Current [ "ColonyId_ReceivedFinishedMessage" ] = message.ColonyId;
            ScenarioContext.Current [ "IsReceivedFinishedMessage" ] = true;
        }

        private void StartedHandler(StartedMessage message)
        {
            Console.WriteLine("[ColonyId: {0}] Started colony!".Inject(message.ColonyId));

            ScenarioContext.Current [ "ColonyId_ReceivedStartedMessage" ] = message.ColonyId;
            ScenarioContext.Current [ "IsReceivedStartedMessage" ] = true;
        }
    }
}