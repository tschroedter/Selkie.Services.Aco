using System;
using System.Threading;
using JetBrains.Annotations;

namespace Selkie.Services.Aco.Specflow.Steps.Common
{
    public static class Helper
    {
        public const string ServiceName = "Aco Service";
        public const string WorkingFolder = @"C:\Development\Selkie\Services\Aco\Selkie.Services.Aco.Console\bin\Debug\";
        public const string FilenName = WorkingFolder + "Selkie.Services.Aco.Console.exe";
        private static readonly TimeSpan SleepTime = TimeSpan.FromSeconds(1.0);

        public static void SleepWaitAndDo([NotNull] Func <bool> breakIfTrue,
                                          [NotNull] Action doSomething)
        {
            for ( var i = 0 ; i < 10 ; i++ )
            {
                Thread.Sleep(SleepTime);

                if ( breakIfTrue() )
                {
                    break;
                }

                doSomething();
            }
        }
    }
}