using Castle.Core;
using Selkie.Aop.Aspects;
using Selkie.Windsor;

namespace Selkie.Services.Aco
{
    [Interceptor(typeof(MessageHandlerAspect))]
    [ProjectComponent(Lifestyle.Transient)]
    public class ServiceColonyParameters : IServiceColonyParameters
    {
        // Todo: Fix - Ant colony doesn't like empty array for CostMatrix
        private int[][] m_CostMatrix =
        {
            new[]
            {
                1,
                10
            },
            new[]
            {
                10,
                1
            }
        };

        // Todo: Fix - Ant colony doesn't like empty array for CostPerLine
        private int[] m_CostPerLine =
        {
            1,
            1,
            1,
            1
        };

        public int[][] CostMatrix
        {
            get
            {
                return m_CostMatrix;
            }
            set
            {
                m_CostMatrix = value;
            }
        }

        public int[] CostPerLine
        {
            get
            {
                return m_CostPerLine;
            }
            set
            {
                m_CostPerLine = value;
            }
        }
    }
}