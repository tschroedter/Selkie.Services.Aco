using Selkie.Services.Aco.Common.Messages;
using Selkie.Services.Aco.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps
{
    public class WhenISendACreateColonyMessageStep : BaseStep
    {
        private readonly int[][] m_CostMatrix =
        {
            new[]
            {
                1,
                2,
                3,
                4
            },
            new[]
            {
                5,
                6,
                7,
                8
            },
            new[]
            {
                9,
                10,
                11,
                12
            },
            new[]
            {
                13,
                14,
                15,
                16
            }
        };

        private readonly int[] m_CostPerLine =
        {
            1,
            1,
            1,
            1
        };

        [When(@"I send a CreateColonyMessage")]
        public override void Do()
        {
            var message = new CreateColonyMessage
                          {
                              CostMatrix = m_CostMatrix,
                              CostPerLine = m_CostPerLine
                          };

            Bus.PublishAsync(message);
        }
    }
}