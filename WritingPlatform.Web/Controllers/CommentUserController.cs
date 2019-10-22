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
            var commentUserBO = DependencyResolver.Current.GetService<CommentUserBO>().GetCommentsUsersList();
            var usersBO = DependencyResolver.Current.GetService<UserBO>().GetUsersList();
            var writerWorkBO = DependencyResolver.Current.GetService<WriterWorkBO>().GetWritersWorksList();

            ViewBag.CommentsUsers = commentUserBO.Select(m => mapper.Map<CommentUserViewModel>(m)).ToList();
            ViewBag.WritersWorks = writerWorkBO.Select(m => mapper.Map<WriterWorkViewModel>(m)).ToList();
            ViewBag.Users = usersBO.Select(a => mapper.Map<UserViewModel>(a)).ToList();

            return View();
        }

        public ActionResult CreateAndEdit(int? id)
        {
            var usersBO = DependencyResolver.Current.GetService<UserBO>();
            var writerWorkBO = DependencyResolver.Current.GetService<WriterWorkBO>();

            var commentUserBO = DependencyResolver.Current.GetService<CommentUserBO>();
            var commentsUsersModel = mapper.Map<CommentUserViewModel>(commentUserBO);
            if (id == null)
            {
                ViewBag.Header = "Создание комментария";
            }
            else
            {
                var commentUser = commentUserBO.GetCommentUserById(id);
                commentsUsersModel = mapper.Map<CommentUserViewModel>(commentUser);
                ViewBag.Header = "Редактирование комментария";
            }
            ViewBag.Users = new SelectList(usersBO.GetUsersList().Select(m => mapper.Map<UserViewModel>(m)).ToList(), "Id", "LoginUser");
            ViewBag.WritersWorks = new SelectList(writerWorkBO.GetWritersWorksList().Select(m => mapper.Map<WriterWorkViewModel>(m)).ToList(), "Id", "TitleWork");

            return View(commentsUsersModel);
        }

        [HttpPost]
        public ActionResult CreateAndEdit(CommentUserViewModel commentsUsersModel)
        {
            var commentUserBO = mapper.Map<CommentUserBO>(commentsUsersModel);
            if (ModelState.IsValid)
            {
                commentUserBO.Save();
                return RedirectToActionPermanent("Index", "CommentUser");
            }
            else
            {
                return View(commentsUsersModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var commentUser = DependencyResolver.Current.GetService<CommentUserBO>().GetCommentUserById(id);
            commentUser.Delete(id);

            return RedirectToActionPermanent("Index", "CommentUser");
        }

        public ActionResult Create(int writerWorkId)
        {
            var usersBO = DependencyResolver.Current.GetService<UserBO>();
            var commentUserBO = DependencyResolver.Current.GetService<CommentUserBO>();
           
            var commentsUsersModel = mapper.Map<CommentUserViewModel>(commentUserBO);

            var userId = usersBO.GetUserByLogin(User.Identity.Name).Id;

            commentsUsersModel.WriterWorkId = writerWorkId;
            commentsUsersModel.UserId = userId;

            return View(commentsUsersModel);
        }

        [HttpPost]
        public ActionResult Create(CommentUserViewModel commentsUsersModel)
        {
            var newComment = mapper.Map<CommentUserBO>(commentsUsersModel);
            if(ModelState.IsValid)
            {
                newComment.Save();
            }
            ////return RedirectToActionPermanent("Index", "CommentUser");
            return View(newComment);
        }
    }
}