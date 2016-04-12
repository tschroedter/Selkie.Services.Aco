using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Selkie.EasyNetQ;
using Selkie.Services.Common;

namespace Selkie.Services.Aco.Console.Client
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    internal class Program
    {
        private static IServicesManager s_Manager;
        private static ISelkieConsole s_Console;
        private static ISelkieBus s_Bus;
        private static IExceptionThrownMessageToStringConverter s_Converter;

        private static void Main()
        {
            IWindsorInstaller assembly = FromAssembly.This();

            var container = new WindsorContainer();
            container.Install(assembly);

            s_Bus = container.Resolve <ISelkieBus>();
            s_Console = container.Resolve <ISelkieConsole>();
            s_Manager = container.Resolve <IServicesManager>();
            s_Converter = container.Resolve <IExceptionThrownMessageToStringConverter>();

            s_Manager.WaitForAllServices();

            CallService();

            s_Console.WriteLine("Press 'Return' to exit!");
            s_Console.ReadLine();

            StopServices();

            container.Dispose();
        }

        private static void StopServices()
        {
            s_Console.WriteLine("Stopping services...");
            s_Manager.StopServices();
        }

        private static void CallService()
        {
            var client = new AcoServiceClient(s_Bus,
                                              s_Console,
                                              s_Converter);

            client.CreateColony();
            client.StartColony();
            client.WaitForFinishColony();
            client.ForceException();
        }
    }
}