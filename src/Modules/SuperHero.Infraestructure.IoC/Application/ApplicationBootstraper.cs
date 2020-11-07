using Microsoft.Extensions.DependencyInjection;
using SuperHero.Application.AppHero;
using SuperHero.Application.AppHero.Interfaces;

namespace SuperHero.Infraestructure.IoC.Application
{
    internal class ApplicationBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IHeroAppService, HeroAppService>();
        }
    }
}