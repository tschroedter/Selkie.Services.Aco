using System;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Services.Aco.Specflow.Steps.Common;
using Selkie.Windsor.Extensions;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class WhenISendAStartMessageStep : BaseStep
    {
        [When(@"I send a StartMessage")]
        public override void Do()
        {
            Guid colonyId = GetCurrentColonyId();

            if ( Guid.Empty == colonyId )
            {
                Console.WriteLine("Received colony id is {0}!".Inject(colonyId));
                return;
            }

            var message = new StartMessage
                          {
                              ColonyId = colonyId,
                              Times = 10
                          };

            Bus.PublishAsync(message);
        }
    }
}