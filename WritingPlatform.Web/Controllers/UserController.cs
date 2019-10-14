using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WritingPlatform.BusinessLayer.BusinessObjects;
using WritingPlatform.Web.Models;

namespace WritingPlatform.Web.Controllers
{
    public class UserController : Controller
    {
        protected IMapper mapper;

        public UserController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var usersBO = DependencyResolver.Current.GetService<UserBO>().GetUsersList().OrderBy(n => n.LoginUser);
            var rolesBO = DependencyResolver.Current.GetService<RoleBO>().GetRolesList();
            ViewBag.Users = usersBO.Select(a => mapper.Map<UserViewModel>(a)).ToList();
            ViewBag.Roles = rolesBO.Select(a => mapper.Map<RoleViewModel>(a)).ToList();

            return View();
        }

        public ActionResult CreateAndEdit(int? id)
        {
            var usersBO = DependencyResolver.Current.GetService<UserBO>();
            var rolesBO = DependencyResolver.Current.GetService<RoleBO>();

            var usersModel = mapper.Map<UserViewModel>(usersBO);

            if (id == null)
            {
                ViewBag.Header = "Создание Пользователя";
            }
            else
            {
                var usersBOList = usersBO.GetUserById(id);
                usersModel = mapper.Map<UserViewModel>(usersBOList);
                ViewBag.Header = "Редактирование Пользователя";
            }
            ViewBag.Roles = new SelectList(rolesBO.GetRolesList().Select(m => mapper.Map<RoleViewModel>(m)).ToList(), "Id", "NameRole");

            return View(usersModel);
        }

        [HttpPost]
        public ActionResult CreateAndEdit(UserViewModel usersModel)
        {
            var userBO = mapper.Map<UserBO>(usersModel);
            if (ModelState.IsValid)
            {
                userBO.Save();
                return RedirectToActionPermanent("Index", "User");
            }
            else
            {
                return View(usersModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var user = DependencyResolver.Current.GetService<UserBO>().GetUserById(id);
            user.Delete(id);

            return RedirectToActionPermanent("Index", "User");
        }
    }
}