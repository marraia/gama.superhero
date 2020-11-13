using SuperHero.Application.AppUser.Input;
using SuperHero.Application.AppUser.Output;
using System.Threading.Tasks;

namespace SuperHero.Application.AppUser.Interfaces
{
    public interface IUserAppService
    {
        Task<UserViewModel> InsertAsync(UserInput user);
        Task<UserViewModel> UpdateAsync(int id, UserInput user);
    }
}
