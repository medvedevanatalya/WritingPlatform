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
    public class RoleController : Controller
    {
        protected IMapper mapper;

        public RoleController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var rolesBO = DependencyResolver.Current.GetService<RoleBO>();
            var rolesList = rolesBO.GetRolesList().OrderBy(n => n.NameRole);
            ViewBag.Roles = rolesList.Select(a => mapper.Map<RoleViewModel>(a)).ToList();
           
            return View();
        }

        public ActionResult CreateAndEdit(int? id)
        {
            var rolesBO = DependencyResolver.Current.GetService<RoleBO>();
            var rolesModel = mapper.Map<RoleViewModel>(rolesBO);

            if (id == null)
            {
                ViewBag.Header = "Создание Роли";
            }
            else
            {
                var rolesBOList = rolesBO.GetRoleById(id);
                rolesModel = mapper.Map<RoleViewModel>(rolesBOList);
                ViewBag.Header = "Редактирование Роли";
            }

            return View(rolesModel);
        }

        [HttpPost]
        public ActionResult CreateAndEdit(RoleViewModel rolesModel)
        {
            var roleBO = mapper.Map<RoleBO>(rolesModel);
            if (ModelState.IsValid)
            {
                roleBO.Save();
                return RedirectToActionPermanent("Index", "Role");
            }
            else
            {
                return View(rolesModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var role = DependencyResolver.Current.GetService<RoleBO>().GetRoleById(id);
            role.Delete(id);

            return RedirectToActionPermanent("Index", "Role");
        }      
    }
}