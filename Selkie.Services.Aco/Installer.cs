﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Selkie.EasyNetQ;
using Selkie.Services.Common;
using Selkie.Windsor;

namespace Selkie.Services.Aco
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "Selkie.";
        }

        // ReSharper disable once CodeAnnotationAnalyzer
        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            base.InstallComponents(container,
                                   store);

            var register = container.Resolve <IRegisterMessageHandlers>();

            register.Register(container,
                              Assembly.GetAssembly(typeof ( Installer )));

            container.Release(register);

            // ReSharper disable MaximumChainedReferences
            container.Register(
                               Classes.FromThisAssembly()
                                      .BasedOn <IService>()
                                      .WithServiceFromInterface(typeof ( IService ))
                                      .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));
            // ReSharper restore MaximumChainedReferences
        }
    }
}