
using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Repositories.Implementation
{
    public class UserAuthenticateService : IUserAuthenticateService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserAuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<Status> LoginAsync(LoginModel login)
        {
            var status = new Status();
            var user=await userManager.FindByNameAsync(login.userName!);
            if(user==null){
                status.StatusCode=0;
                status.Message="El username es invalido";
                return status;
            }
            if(!await userManager.CheckPasswordAsync(user,login.Password!)){
                status.StatusCode=0;
                status.Message="El password es incorrecto";
                return status;
            }
            var retultado =await signInManager.PasswordSignInAsync(user,login.Password!,true,false);
            if(!retultado.Succeeded){
                status.StatusCode=0;
                status.Message="Las credenciales no son correctas";
                
            }
            status.StatusCode=1;
            status.Message="El login fue exitoso";
            return status;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}