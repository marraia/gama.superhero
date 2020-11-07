using Microsoft.Extensions.DependencyInjection;
using SuperHero.Domain.Interfaces.Repositories;
using SuperHero.Infrastructure.Repositories;

namespace SuperHero.Infraestructure.IoC.Repositories
{
    internal class RepositoriesBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IHeroRepository, HeroRepository>();
        }
    }
}
