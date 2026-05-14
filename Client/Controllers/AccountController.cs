using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Client.Services;
using Shared.DTOs;
using System.Security.Claims;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid) return View(loginDto);

            var result = await _authService.LoginAsync(loginDto);

            if (result != null)
            {
                await SignInUser(result);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            return View(loginDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (!ModelState.IsValid) return View(registerDto);

            // Por defecto, asignamos un rol si no se especifica (debería venir del form o ser fijo para registros públicos)
            if (registerDto.IdRol == 0) registerDto.IdRol = 2; // Asumiendo que 2 es un rol base

            var result = await _authService.RegisterAsync(registerDto);

            if (result != null)
            {
                await SignInUser(result);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Error al registrar el usuario. El correo podría estar en uso.");
            return View(registerDto);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        private async Task SignInUser(AuthResponseDTO authResponse)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, authResponse.Nombre),
                new Claim(ClaimTypes.Email, authResponse.Correo),
                new Claim(ClaimTypes.Role, authResponse.Rol),
                new Claim("JWToken", authResponse.Token) // Guardamos el JWT para usarlo en peticiones de API
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
