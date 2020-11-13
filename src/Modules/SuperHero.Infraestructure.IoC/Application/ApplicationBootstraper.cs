using Microsoft.Extensions.DependencyInjection;
using SuperHero.Application.AppHero;
using SuperHero.Application.AppHero.Interfaces;
using SuperHero.Application.AppUser;
using SuperHero.Application.AppUser.Interfaces;

namespace SuperHero.Infraestructure.IoC.Application
{
    internal class ApplicationBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<ILoginAppService, LoginAppService>();
            services.AddScoped<IHeroAppService, HeroAppService>();
        }
    }
}