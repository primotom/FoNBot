using System;
using Castle.Windsor;

namespace FoNBot
{
    public class CastleServiceProvider : IServiceProvider
    {
        private readonly IWindsorContainer container;

        public CastleServiceProvider(IWindsorContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            return container.Resolve(serviceType);
        }
    }
}