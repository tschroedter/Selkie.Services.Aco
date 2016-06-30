using System.Diagnostics.CodeAnalysis;
using Castle.Windsor.Installer;
using Selkie.Services.Common;

namespace Selkie.Services.Aco.Windows.Service
{
    [ExcludeFromCodeCoverage]
    public class AcoServiceMain : ServiceMain
    {
        public void Run()
        {
            StartServiceAndRunForever(FromAssembly.This(),
                                      Aco.Service.ServiceName);
        }
    }
}