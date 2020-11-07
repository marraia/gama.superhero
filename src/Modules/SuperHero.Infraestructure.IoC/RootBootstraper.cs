using Microsoft.Extensions.DependencyInjection;
using SuperHero.Infraestructure.IoC.Application;
using SuperHero.Infraestructure.IoC.Repositories;

namespace SuperHero.Infraestructure.IoC
{
    public class RootBootstraper
    {
        public void RootRegisterServices(IServiceCollection services)
        {
            new ApplicationBootstraper().ChildServiceRegister(services);
            new RepositoriesBootstraper().ChildServiceRegister(services);
        }
    }
}
