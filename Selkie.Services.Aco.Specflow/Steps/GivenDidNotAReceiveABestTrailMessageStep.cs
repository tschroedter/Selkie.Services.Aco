using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class GivenDidNotAReceiveABestTrailMessageStep : BaseStep
    {
        [Given(@"Did not a receive a BestTrailMessage")]
        public override void Do()
        {
            ScenarioContext.Current [ "IsReceivedBestTrailMessage" ] = false;
        }
    }
}