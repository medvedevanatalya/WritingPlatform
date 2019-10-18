using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WritingPlatform.BusinessLayer.BusinessObjects;
using WritingPlatform.Web.Models;

namespace WritingPlatform.Web.Controllers
{
    public class AccountController : Controller
    {
        protected IMapper mapper;

        public AccountController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = DependencyResolver.Current.GetService<UserBO>().GetUserByLogin(loginModel.LoginUser);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(loginModel.LoginUser, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Логин/Пароль введены не верно");
                }            
            }    
            return View(loginModel);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = DependencyResolver.Current.GetService<UserBO>().GetUserByLogin(registerModel.LoginUser);

                if (user == null)
                {
                    var roleUser = DependencyResolver.Current.GetService<RoleBO>().GetRoleByName("user");
                    var userBO = mapper.Map<UserBO>(registerModel);
                    userBO.RoleId = roleUser.Id; 
                    userBO.Save();
                    if (userBO != null)
                    {
                        FormsAuthentication.SetAuthCookie(registerModel.LoginUser, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(registerModel);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}