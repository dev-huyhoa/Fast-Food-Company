
using HUYHOA_PS13016_ASM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUYHOA_PS13016_ASM.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserFullName") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserFullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string email, string Password)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Users.Where(x => x.Email.Equals(email) && x.UserPassWord.Equals(Password)).FirstOrDefault();
                if (data != null)
                {
                    HttpContext.Session.SetString("Email", email);
                    HttpContext.Session.SetString("UserFullName", data.UserFullName);
                    HttpContext.Session.SetInt32("Roles", data.RoleID);
                    if (data.RoleID == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product");
                    }                   
                }
                else
                {
                    ViewBag.error = "Email hoặc mật khẩu không đúng";
                    return View();
                }
            }
            return View();
        }

        public IActionResult logOut()
        {
            HttpContext.Session.Clear();
            if (HttpContext.Session.GetString("UserFullName") == null)
            {

            }
            return RedirectToAction("Index", "Login");
        }
    }
}
