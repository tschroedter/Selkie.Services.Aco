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
            SleepWaitAndDo(() => ( bool ) ScenarioContext.Current [ "IsReceivedBestTrailMessage" ],
                           WhenISendAStartMessage);

            Assert.True(( bool ) ScenarioContext.Current [ "IsReceivedBestTrailMessage" ]);
        }

        private void WhenISendAStartMessage()
        {
            new WhenISendAStartMessageStep().Do();
        }
    }
}