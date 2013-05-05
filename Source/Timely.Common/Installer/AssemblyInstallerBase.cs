﻿using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Timely.Common.Installer
{
    public abstract class AssemblyInstallerBase : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterFactories(container, store);
            RegisterSingletons(container, store);
            RegisterAssemblyClasses(container);
        }

        private void RegisterAssemblyClasses(IWindsorContainer container)
        {
            container.Register(Classes.FromAssemblyContaining(this.GetType()).Pick().WithService.DefaultInterfaces().LifestyleTransient());
        }

        protected abstract void RegisterFactories(IWindsorContainer container, IConfigurationStore store);

        protected abstract void RegisterSingletons(IWindsorContainer container, IConfigurationStore store);
    }
}