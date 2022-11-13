using CP_LAB_5.Data;
using CP_LAB_5.Mapper;
using CP_LAB_5.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CP_LAB_5.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext _context;

        private readonly ICustomMapper _customMapper;

        public AccountController(UserContext context)
        {
            _context = context;
            _customMapper = new CustomMapper();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel userRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var users = await _context.Users.FirstOrDefaultAsync(u => u.Password == userRegistrationViewModel.Password &&
                                                         u.Email == userRegistrationViewModel.Email);
                if (users == null)
                {
                    var user = _customMapper.MapToUser(userRegistrationViewModel);

                    await _context.Users.AddAsync(user);

                    await _context.SaveChangesAsync();

                    await Authentication(user.UserName, user.Email);

                    return RedirectToAction("Main", "Home");
                }
            }
            else
                ModelState.AddModelError("", "User with that email already exist");

            return View(userRegistrationViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userLoginViewModel)
        {

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Password == userLoginViewModel.Password &&
                                                       u.UserName == userLoginViewModel.UserName);

                if (user != null)
                {
                    await Authentication(user.UserName, user.Email);

                    return RedirectToAction("Main", "Home");
                }

                ModelState.AddModelError("", "Incorrect login or password");
            }

            return View(userLoginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == HttpContext.User.FindFirst(ClaimTypes.Email).Value);

            var viewModel = new ProfileViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                FIO = user.FIO,
                Password = user.Password,
                Phone = user.Phone
            };

            return View(viewModel);
        }

        private async Task Authentication(string name, string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, email),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email)
            };

            var identity = new ClaimsIdentity(claims, "UserCookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("UserCookieAuth", claimsPrincipal);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("UserCookieAuth");

            return RedirectToAction("Main", "Home");
        }
    }
}
