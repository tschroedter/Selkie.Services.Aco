using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps.Common
{
    public class GivenDidNotReceivePingResponseMessageStep : BaseStep
    {
        [Given(@"Did not receive ping response message")]
        public override void Do()
        {
            ScenarioContext.Current [ "IsReceivedPingResponse" ] = false;
        }
    }
}