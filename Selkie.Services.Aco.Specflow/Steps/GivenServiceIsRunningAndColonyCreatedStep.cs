using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class GivenServiceIsRunningAndColonyCreatedStep : BaseStep
    {
        [Given(@"Service is running and colony created")]
        public override void Do()
        {
            new GivenServiceIsRunningStep().Do();
            new WhenISendACreateColonyMessageStep().Do();
            new ThenTheResultShouldBeThatIReceivedACreatedColonyMessageStep().Do();
        }
    }
}