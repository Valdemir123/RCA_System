﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCA.Data;
using RCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RCA.Controllers
{
    public class HomeController : Controller
    {
        private readonly RCAContext _context;
        public HomeController(RCAContext context)
        {
            _context = context;
        }




        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Contact()
        {
            return View();
        }



        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            var _UserLogin = new Class_UserLogin()
            {
                UserName = "",
                Password = "",
                ReEnter = "",
                PassCheck = ""
            };

            ViewBag.Message = "";
            return View(_UserLogin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Class_UserLogin _UserLogin)
        {
            _UserLogin.UserName = _UserLogin.UserName.ToUpper();

            var _UserFound = _context.Class_User.Where(m => m.UserName == _UserLogin.UserName).FirstOrDefault();
            if (_UserFound != null)
            {
                if (_UserFound.Password == "Inicial")
                {
                    if (_UserLogin.Password != _UserLogin.PassCheck)
                    {
                        ViewBag.Message = "(Passwords) inconsistente!";
                        return View(_UserLogin);
                    }
                    _UserFound.Password = _UserLogin.Password;

                    _context.Update(_UserFound);
                    _context.SaveChanges();
                }
                else if (_UserFound.Password != _UserLogin.Password)
                {
                    ViewBag.Message = "(Password) invalida!";
                    return View(_UserLogin);
                }

                var _Company = _context.Class_Company.FirstOrDefault(m => m.Id == _UserFound.CompanyId);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, _UserFound.UserName),
                    new Claim(ClaimTypes.Role, _UserFound.TypeAccessId.ToString()),
                    new Claim("CompanyName", _Company.Name),
                    new Claim("CompanyId", _UserFound.CompanyId.ToString())
                };

                var identidadeDeUsuario = new ClaimsIdentity(claims, "Index");
                ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

                var propriedadesDeAutenticacao = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(1),
                    IsPersistent = true
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
            }
            else
            {
                ViewBag.Message = "(UserLogin) não encontrado!";
                return View(_UserLogin);
            }

            return RedirectToAction(nameof(Index));
        }
        //
        public JsonResult FIND_USER(string _UserName)
        {
            string _Pass = null;

            var _UserFound = _context.Class_User.Where(m => m.UserName == _UserName.ToUpper()).FirstOrDefault();
            if (_UserFound != null)
            {
                _Pass = _UserFound.Password;
            }
            //
            return Json(_Pass);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
