using NUnit.Framework;
using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class ThenTheResultShouldBeThatIReceivedABestTrailMessageStep : BaseStep
    {
        [Then(@"the result should be that I received a BestTrailMessage")]
        public override void Do()
        {
            SleepWaitAndDo(() => GetBoolValueForScenarioContext("IsReceivedBestTrailMessage"),
                           WhenISendAStartMessage);

            Assert.True(GetBoolValueForScenarioContext("IsReceivedBestTrailMessage"));
        }

        private void WhenISendAStartMessage()
        {
            new WhenISendAStartMessageStep().Do();
        }
    }
}