using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPGeekEF.Models;
using TE.AuthLib;
using TE.AuthLib.DAL;

namespace NPGeekEF.Controllers
{
    public class AccountController : NPGeekBaseController
    {
        public AccountController(IUserDAO userDAO, IAuthProvider authProvider) : base(authProvider)
        {

        }

        [Authorize]  //Must be logged in to see Account/Index page
        [HttpGet]
        public IActionResult Index()
        {
            User user = authProvider.GetCurrentUser();
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(User user)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // TODO 04 (Controller): Process the login
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            // Ensure the fields were filled out
            if (ModelState.IsValid)
            {
                // Check that they provided correct credentials
                bool validLogin = authProvider.Login(loginViewModel.Email, loginViewModel.Password);
                if (validLogin)
                {
                    // Redirect the user where you want them to go after successful login
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(loginViewModel);
        }

        [Authorize] // Must be logged in to log out
        [HttpGet]
        public IActionResult Logout()
        {
            // Clear user from session
            authProvider.Logout();

            // Redirect the user where you want them to go after logoff
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // TODO 05 (Controller): Process the register request
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            // Register them as a new user (and set default role)
            // When a user registers they need to be given a role. If you don't need anything special
            // just give them "User".
            authProvider.Register(registerViewModel.Email, registerViewModel.Password, role: "User");

            // Redirect the user where you want them to go after registering
            return RedirectToAction("Index", "Account");
        }
    }
}