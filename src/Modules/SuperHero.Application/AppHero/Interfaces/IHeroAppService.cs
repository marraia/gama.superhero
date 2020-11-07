using SuperHero.Application.AppHero.Input;
using SuperHero.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHero.Application.AppHero.Interfaces
{
    public interface IHeroAppService
    {
        Task<Hero> InsertAsync(HeroInput hero);
        Task<Hero> GetByIdAsync(int id);
        IEnumerable<Hero> Get();
    }
}
