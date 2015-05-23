using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class GivenDidNotAReceiveAFinishedMessageStep : BaseStep
    {
        [Given(@"Did not a receive a FinishedMessage")]
        public override void Do()
        {
            ScenarioContext.Current [ "IsReceivedFinishedMessage" ] = false;
        }
    }
}