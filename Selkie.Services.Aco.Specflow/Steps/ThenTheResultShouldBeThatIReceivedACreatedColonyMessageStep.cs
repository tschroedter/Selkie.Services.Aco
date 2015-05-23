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
            SleepWaitAndDo(() => ( bool ) ScenarioContext.Current [ "IsReceivedCreatedColonyMessage" ],
                           WhenISendACreateColonyMessage);

            Assert.True(( bool ) ScenarioContext.Current [ "IsReceivedCreatedColonyMessage" ]);
        }

        private void WhenISendACreateColonyMessage()
        {
            new WhenISendACreateColonyMessageStep().Do();
        }
    }
}