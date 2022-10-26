using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scribe_Factory_Core.Common;
using Scribe_Factory_Core.Models;
using Scribe_Factory_Core.Repositories;
using Scribe_Factory_Core.Repositories.Interfaces;

namespace Scribe_Factory_Core.Controllers
{
    public class UserController : Controller
    {
        IUserManagementRepository UserManagementRepository;
        public UserController(IUserManagementRepository UserManagementRepository)
        {
            this.UserManagementRepository = UserManagementRepository;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = UserManagementRepository.LoginUser(email, password);
            if (user != null)
            {
                SessionHelper.SetObject(HttpContext.Session, "CurrentUser", user);
                return new JsonResult(true) { StatusCode = 200 };
            }
            else
            {
                return new JsonResult(false) { StatusCode = 200 };
            }
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(RegisterUserViewModel registerUserViewModel)
        {
            if (ModelState.IsValid)
            {
                if (UserManagementRepository.IsUserExist(registerUserViewModel.Email))
                {
                    ViewBag.UserExist = "Email already exist. Do you own an account?";
                    return View();
                }
                else
                {
                    if (UserManagementRepository.RegisterUser(registerUserViewModel))
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            return View();
        }
       
    }
}
