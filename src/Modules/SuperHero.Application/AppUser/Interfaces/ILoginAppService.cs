
using SuperHero.Application.AppUser.Output;
using System.Threading.Tasks;

namespace SuperHero.Application.AppUser.Interfaces
{
    public interface ILoginAppService
    {
        Task<UserViewModel> LoginAsync(string login, string password);
    }
}
