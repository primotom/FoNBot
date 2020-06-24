using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using FoNBot.Service;

namespace FoNBot
{
    public class ApplicationInitialization
    {
        public ApplicationInitialization()
        {
            Container = new WindsorContainer();
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));
            Container.Register(Component.For<IWindsorContainer>().Instance(Container));
        }

        public IWindsorContainer Container { get; }

        public void RegisterDependencies()
        {
            // TODO
            Container.Register(
                Component.For<ItemParserService>()
            );
        }
    }
}