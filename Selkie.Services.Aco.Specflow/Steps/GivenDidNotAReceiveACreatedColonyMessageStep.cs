using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class GivenDidNotAReceiveACreatedColonyMessageStep : BaseStep
    {
        [Given(@"Did not a receive a CreatedColonyMessage")]
        public override void Do()
        {
            ScenarioContext.Current [ "IsReceivedCreatedColonyMessage" ] = false;
        }
    }
}