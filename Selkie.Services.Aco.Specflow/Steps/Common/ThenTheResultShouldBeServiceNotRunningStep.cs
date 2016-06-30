using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Selkie.Services.Aco.Specflow.Steps.Common
{
    public class ThenTheResultShouldBeServiceNotRunningStep : BaseStep
    {
        [Then(@"the result should be service not running")]
        public override void Do()
        {
            SleepWaitAndDo(() => GetProcessHasExitedValueForScenarioContext("ExeProcess"),
                           DoNothing);

            Assert.True(( ( Process ) ScenarioContext.Current [ "ExeProcess" ] ).HasExited,
                        "Process didn't exited!");
        }

        private static bool GetProcessHasExitedValueForScenarioContext([NotNull] string key)
        {
            if ( !ScenarioContext.Current.Keys.Contains(key) )
            {
                return false;
            }

            var result = ( Process ) ScenarioContext.Current [ key ];

            return result.HasExited;
        }
    }
}