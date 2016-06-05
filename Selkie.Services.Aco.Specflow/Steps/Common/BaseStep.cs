using System;
using JetBrains.Annotations;
using Selkie.EasyNetQ;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps.Common
{
    [Binding]
    public abstract class BaseStep
    {
        protected BaseStep()
        {
            Bus = ( ISelkieBus ) ScenarioContext.Current [ "ISelkieBus" ];
        }

        protected ISelkieBus Bus { get; private set; }

        public void SleepWaitAndDo([NotNull] Func <bool> breakIfTrue,
                                   [NotNull] Action doSomething)
        {
            Helper.SleepWaitAndDo(breakIfTrue,
                                  doSomething);
        }

        public void DoNothing()
        {
        }

        public abstract void Do();
    }
}