using NUnit.Framework;
using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class ThenTheResultShouldBeThatIReceivedAStartedMessageStep : BaseStep
    {
        [Then(@"the result should be that I received a StartedMessage")]
        public override void Do()
        {
            SleepWaitAndDo(() => GetBoolForComparingGuids("ColonyId_ReceivedCreatedColonyMessage",
                                                          "ColonyId_ReceivedStartedMessage"),
                           WhenISendAStartMessage);

            Assert.True(GetBoolValueForScenarioContext("IsReceivedStartedMessage"));
        }

        private void WhenISendAStartMessage()
        {
            new WhenISendAStartMessageStep().Do();
        }
    }
}