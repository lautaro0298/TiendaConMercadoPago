
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AppStore.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticateService _autService;

        public UserAuthenticationController(IUserAuthenticateService autService)
        {
            _autService = autService;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }
            var retultado = await _autService.LoginAsync(loginModel);
            if (retultado.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Msg"] = retultado.Message;
                return RedirectToAction(nameof(Login));
            }
        }
        public async Task<IActionResult> logout()
        {
            await _autService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}