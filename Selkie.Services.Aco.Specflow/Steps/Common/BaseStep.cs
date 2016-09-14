using System;
using System.Linq;
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

        public static bool GetBoolForComparingGuids(
            [NotNull] string keyOne,
            [NotNull] string keyTwo)
        {
            Guid guidOne = GetGuidValueFromScenarioContext(keyOne);
            Guid guidTwo = GetGuidValueFromScenarioContext(keyTwo);

            if ( Guid.Empty == guidOne ||
                 Guid.Empty == guidTwo )
            {
                return false;
            }

            return guidOne == guidTwo;
        }

        public static bool GetBoolValueForScenarioContext([NotNull] string key)
        {
            if ( !ScenarioContext.Current.Keys.Contains(key) )
            {
                return false;
            }

            var result = ( bool ) ScenarioContext.Current [ key ];

            return result;
        }

        public static Guid GetCurrentColonyId()
        {
            return GetGuidValueFromScenarioContext("ColonyId_ReceivedCreatedColonyMessage");
        }

        public static Guid GetGuidValueFromScenarioContext([NotNull] string key)
        {
            if ( !ScenarioContext.Current.Keys.Contains(key) )
            {
                return Guid.Empty;
            }

            var guid = ( Guid ) ScenarioContext.Current [ key ];

            return guid;
        }

        public abstract void Do();

        public void DoNothing()
        {
        }

        public void SleepWaitAndDo([NotNull] Func <bool> breakIfTrue,
                                   [NotNull] Action doSomething)
        {
            Helper.SleepWaitAndDo(breakIfTrue,
                                  doSomething);
        }
    }
}