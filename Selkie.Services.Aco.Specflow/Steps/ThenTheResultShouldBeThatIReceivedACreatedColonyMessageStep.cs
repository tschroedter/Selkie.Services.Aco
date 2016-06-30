using NUnit.Framework;
using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class ThenTheResultShouldBeThatIReceivedACreatedColonyMessageStep : BaseStep
    {
        [Then(@"the result should be that I received a CreatedColonyMessage")]
        public override void Do()
        {
            SleepWaitAndDo(() => GetBoolValueForScenarioContext("IsReceivedCreatedColonyMessage"),
                           WhenISendACreateColonyMessage);

            Assert.True(GetBoolValueForScenarioContext("IsReceivedCreatedColonyMessage"));
        }

        private void WhenISendACreateColonyMessage()
        {
            new WhenISendACreateColonyMessageStep().Do();
        }
    }
}