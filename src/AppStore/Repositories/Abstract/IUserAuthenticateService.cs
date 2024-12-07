using AppStore.Models.DTO;

namespace AppStore.Repositories.Abstract;
public interface  IUserAuthenticateService
{
    Task<Status> LoginAsync(LoginModel login);
    Task LogoutAsync();
}