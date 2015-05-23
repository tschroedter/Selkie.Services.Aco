using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class GivenDidNotAReceiveAStartedMessageStep : BaseStep
    {
        [Given(@"Did not a receive a StartedMessage")]
        public override void Do()
        {
            ScenarioContext.Current [ "IsReceivedStartedMessage" ] = false;
        }
    }
}