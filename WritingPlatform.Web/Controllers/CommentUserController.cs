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
    public class CommentUserController : Controller
    {
        protected IMapper mapper;
        public CommentUserController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public ActionResult Index()
        {
            var writerWorkBO = DependencyResolver.Current.GetService<CommentUserBO>().GetCommentsUsersList();
            var usersBO = DependencyResolver.Current.GetService<UserBO>().GetUsersList();

            ViewBag.WritersWorks = writerWorkBO.Select(m => mapper.Map<CommentUserViewModel>(m)).ToList();
            ViewBag.Users = usersBO.Select(a => mapper.Map<UserViewModel>(a)).ToList();


            return View();
        }

        public ActionResult CreateAndEdit(int? id)
        {
            var usersBO = DependencyResolver.Current.GetService<UserBO>();

            var writerWorkBO = DependencyResolver.Current.GetService<CommentUserBO>();
            var writersWorksModel = mapper.Map<CommentUserViewModel>(writerWorkBO);

            if (id == null)
            {
                ViewBag.Header = "Создание Комментария";
            }
            else
            {
                var writersWorksBOList = writerWorkBO.GetCommentUserById(id);
                writersWorksModel = mapper.Map<CommentUserViewModel>(writersWorksBOList);
                ViewBag.Header = "Редактирование комментария";
            }
            ViewBag.Users = new SelectList(usersBO.GetUsersList().Select(m => mapper.Map<UserViewModel>(m)).ToList(), "Id", "LoginUser");

            return View(writersWorksModel);
        }

        [HttpPost]
        public ActionResult CreateAndEdit(CommentUserViewModel writersWorksModel)
        {
            var writerWorkBO = mapper.Map<CommentUserBO>(writersWorksModel);
            if (ModelState.IsValid)
            {
                writerWorkBO.Save();
                return RedirectToActionPermanent("Index", "CommentUser");
            }
            else
            {
                return View(writersWorksModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var writerWork = DependencyResolver.Current.GetService<CommentUserBO>().GetCommentUserById(id);
            writerWork.Delete(id);

            return RedirectToActionPermanent("Index", "CommentUser");
        }
    }
}