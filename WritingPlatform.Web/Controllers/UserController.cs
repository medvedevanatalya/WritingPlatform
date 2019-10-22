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

        [Authorize]
        public ActionResult Index(int? id)
        {
            var usersBO = DependencyResolver.Current.GetService<UserBO>();
            var rolesBO = DependencyResolver.Current.GetService<RoleBO>();
            var writerWorkBO = DependencyResolver.Current.GetService<WriterWorkBO>();

            ViewBag.Users = usersBO.GetUsersList().Select(a => mapper.Map<UserViewModel>(a)).ToList();
            ViewBag.Roles = rolesBO.GetRolesList().Select(a => mapper.Map<RoleViewModel>(a)).ToList();
            ViewBag.CountWriterWork = writerWorkBO.GetWritersWorksList().Select(m=>mapper.Map<WriterWorkBO>(m)).Where(x=>x.UserId==id).Count();

            return View();
        }

        public ActionResult Edit(int? id)
        {
            var rolesBO = DependencyResolver.Current.GetService<RoleBO>();
            var usersBO = DependencyResolver.Current.GetService<UserBO>();

            //var usersModel = mapper.Map<UserViewModel>(usersBO);

            //if (id == null)
            //{
            //    //ViewBag.Header = "Создание Пользователя";
            //    //return RedirectToActionPermanent("Register", "Account");                                      
            //}      
            //else
            //{
                var usersBOList = usersBO.GetUserById(id);
                var usersModel = mapper.Map<UserViewModel>(usersBOList);
                ViewBag.Header = "Редактирование профиля";
            //}
            ViewBag.Roles = new SelectList(rolesBO.GetRolesList().Select(m => mapper.Map<RoleViewModel>(m)).ToList(), "Id", "NameRole");

            return View(usersModel);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel usersModel)
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