using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO loginRequest = new();
            return View(loginRequest); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            APIResponse response = await _authService.LoginAsync<APIResponse>(loginRequest);
            if(response.IsSuccess && response != null)
            {
                TokenDTO model = JsonConvert.DeserializeObject<TokenDTO>(Convert.ToString(response.Result));
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(model.AccessToken);

                //HttpContext.Session.SetString(SD.AccessToken, model.AccessToken);
                _tokenProvider.SetToken(model);
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                //identity.AddClaim(new Claim(ClaimTypes.Name, model.User.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u=>u.Type== "unique_name").Value));
                identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u=>u.Type=="role").Value));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", response.ErrorMessages.FirstOrDefault());
                return View(loginRequest);

            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegistrationRequestDTO registrationRequest = new();
            var roleList = new List<SelectListItem> {
            new SelectListItem{Text = SD.Admin, Value=SD.Admin},
            new SelectListItem{Text = SD.Customer, Value=SD.Customer},

            };
            ViewBag.RoleList = roleList;
            return View(registrationRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequest)
        {
            if (ModelState.IsValid)
            {
                APIResponse response = await _authService.RegisterAsync<APIResponse>(registrationRequest);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ModelState.AddModelError("CustomError", response.ErrorMessages.FirstOrDefault());
                    return View(registrationRequest);
                }
            }
            else
            {
                return View(registrationRequest);
            }
         
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.AccessToken, "");
            return RedirectToAction("Index", "Home");

        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
