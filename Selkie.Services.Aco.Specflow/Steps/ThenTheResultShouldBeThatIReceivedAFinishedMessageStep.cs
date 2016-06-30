using NUnit.Framework;
using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class ThenTheResultShouldBeThatIReceivedAFinishedMessageStep : BaseStep
    {
        [Then(@"the result should be that I received a FinishedMessage")]
        public override void Do()
        {
            SleepWaitAndDo(() => GetBoolValueForScenarioContext("IsReceivedFinishedMessage"),
                           WhenISendAStartMessage);

            Assert.True(GetBoolValueForScenarioContext("IsReceivedFinishedMessage"));
        }

        private void WhenISendAStartMessage()
        {
            new WhenISendAStartMessageStep().Do();
        }
    }
}