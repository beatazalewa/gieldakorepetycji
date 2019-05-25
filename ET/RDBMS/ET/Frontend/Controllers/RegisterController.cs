using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class RegisterController : Controller
    {
        private ETContext _context;
        public RegisterController(ETContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult LoginPage()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string logEmail, string logPassword)
        {
            var Hasher = new PasswordHasher<User>();
            User foundUser = _context.Users.SingleOrDefault(user => user.Email == logEmail);

            if (foundUser == null || 0 == Hasher.VerifyHashedPassword(foundUser, foundUser.Pass, logPassword))
            {
                ViewBag.Message = "Login lub hasło nieprawidłowe";
                return View("Index");
            }
            else
            {
                HttpContext.Session.SetInt32("UserId", foundUser.UserId);
                return RedirectToAction("Index", "Account", new { accountNum = HttpContext.Session.GetInt32("UserId")});
            }

        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel NewUser)
        {
            if (ModelState.IsValid)
            {
                User ExistingUser = _context.Users.SingleOrDefault(user => user.Email == NewUser.Email);
                if (ExistingUser != null)
                {
                    ViewBag.Message = "Użytkownik z takim emailem już istnieje!";
                    return View("Index");
                }
                PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                User User = new User
                {
                    FirstName = NewUser.FirstName,
                    LastName = NewUser.LastName,
                    Email = NewUser.Email,
                    Pass = NewUser.Password,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow, 
                    Age = NewUser.Age,
                    Nickname = NewUser.Nickname, 
                    PhoneNumber = NewUser.PhoneNumber
                };
                _context.Add(User);
                _context.SaveChanges();
                User LoggedUser = _context.Users.SingleOrDefault(user => user.Email == NewUser.Email);
                HttpContext.Session.SetInt32("UserId", User.UserId);
                return RedirectToAction("Index", "Account", new { accountNum = HttpContext.Session.GetInt32("UserId") });
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("Logoff")]
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }


    }
}