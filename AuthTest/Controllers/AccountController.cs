using AuthTest.Models;
using AuthTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace AuthTest.Controllers;

public class AccountController : Controller
{
    private readonly AuthTest.Data.AppDbContext _db;

    public AccountController(AuthTest.Data.AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Login == model.Login &&
                u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(user);

                return RedirectToAction("Index", "Home");
            }
        }
        return View(model);
    }
    private async Task Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
        };
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", 
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(id),
           new AuthenticationProperties
           {
               IsPersistent = true,
               ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
           }

);
    }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {

        if (ModelState.IsValid)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
            if (user == null)
            {
                Role role = _db.Roles.FirstOrDefault(r => r.Name == "user");
                _db.Users.Add(new Models.User 
                        {
                            Email = model.Email, 
                            Password = model.Password,
                            RoleId = 2,
                            Role = role
                    }
                );

                await _db.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
        }
        return View(model);
    }


    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

}
