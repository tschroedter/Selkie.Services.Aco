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
            SleepWaitAndDo(() => ( bool ) ScenarioContext.Current [ "IsReceivedFinishedMessage" ],
                           WhenISendAStartMessage);

            Assert.True(( bool ) ScenarioContext.Current [ "IsReceivedFinishedMessage" ]);
        }

        private void WhenISendAStartMessage()
        {
            new WhenISendAStartMessageStep().Do();
        }
    }
}