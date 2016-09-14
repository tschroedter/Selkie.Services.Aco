using System;
using Castle.Core;
using Selkie.Aco.Anthill;
using Selkie.Aop.Aspects;
using Selkie.Windsor;

namespace Selkie.Services.Aco
{
    [Interceptor(typeof( MessageHandlerAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public class ServiceColonyParameters : IServiceColonyParameters
    {
        // Todo: Fix - Ant colony doesn't like empty array for CostMatrix
        // Todo: Fix - Ant colony doesn't like empty array for CostPerFeature

        public ServiceColonyParameters()
        {
            CostMatrix = new[]
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

            CostPerFeature = new[]
                             {
                                 1,
                                 1,
                                 1,
                                 1
                             };

            IsFixedStartNode = AntSettings.TrailStartNodeType.Random;

            FixedStartNode = 0;
        }

        public Guid ColonyId { get; set; }


        public int[][] CostMatrix { get; set; }

        public int[] CostPerFeature { get; set; }

        public AntSettings.TrailStartNodeType IsFixedStartNode { get; set; }

        public int FixedStartNode { get; set; }
    }
}