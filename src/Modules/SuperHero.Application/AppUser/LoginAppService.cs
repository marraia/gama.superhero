using Marraia.Notifications.Interfaces;
using SuperHero.Application.AppUser.Interfaces;
using SuperHero.Application.AppUser.Output;
using SuperHero.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.Application.AppUser
{
    public class LoginAppService : ILoginAppService
    {
        private readonly ISmartNotification _notification;
        private readonly IUserRepository _userRepository;

        public LoginAppService(ISmartNotification notification,
                                IUserRepository userRepository)
        {
            _notification = notification;
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> LoginAsync(string login, string password)
        {
            var user = await _userRepository
                                .GetByLoginAsync(login)
                                .ConfigureAwait(false);

            if (user == default)
            {
                _notification.NewNotificationBadRequest("Usuário não encontrado!");
                return default;
            }

            if (!user.IsEqualPassword(password))
            {
                _notification.NewNotificationBadRequest("Senha incorreta!");
                return default;
            }

            return new UserViewModel(user.Id, user.Login, user.Name, user.Profile, user.Created);
        }
    }
}
