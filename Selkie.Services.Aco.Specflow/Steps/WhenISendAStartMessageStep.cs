using Selkie.Services.Aco.Common.Messages;
using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class WhenISendAStartMessageStep : BaseStep
    {
        [When(@"I send a StartMessage")]
        public override void Do()
        {
            var message = new StartMessage
                          {
                              Times = 10
                          };

            Bus.PublishAsync(message);
        }
    }
}