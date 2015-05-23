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
            SleepWaitAndDo(() => ( bool ) ScenarioContext.Current [ "IsReceivedStartedMessage" ],
                           WhenISendAStartMessage);

            Assert.True(( bool ) ScenarioContext.Current [ "IsReceivedStartedMessage" ]);
        }

        private void WhenISendAStartMessage()
        {
            new WhenISendAStartMessageStep().Do();
        }
    }
}