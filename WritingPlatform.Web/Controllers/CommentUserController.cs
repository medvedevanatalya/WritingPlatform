﻿using AutoMapper;
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

        [ChildActionOnly]
        public ActionResult _Create(int writerWorkId)
        {
            var usersBO = DependencyResolver.Current.GetService<UserBO>();
            var writerWorkBO = DependencyResolver.Current.GetService<WriterWorkBO>();

            var commentUserBO = DependencyResolver.Current.GetService<CommentUserBO>();
            var commentsUsersModel = mapper.Map<CommentUserViewModel>(commentUserBO);

            ViewBag.Users = new SelectList(usersBO.GetUsersList().Select(m => mapper.Map<UserViewModel>(m)).ToList(), "Id", "LoginUser");
            ViewBag.WritersWorks = new SelectList(writerWorkBO.GetWritersWorksList().Select(m => mapper.Map<WriterWorkViewModel>(m)).ToList(), "Id", "TitleWork");

            commentsUsersModel.WriterWorkId = writerWorkId;
            return View(commentsUsersModel);
        }

        [HttpPost]
        public ActionResult _Create(CommentUserViewModel commentsUsersModel)
        {
            var commentUserBO = mapper.Map<CommentUserBO>(commentsUsersModel);
            var usersBO = DependencyResolver.Current.GetService<UserBO>(); 

            var userId = usersBO.GetUserByLogin(User.Identity.Name).Id;
            var writerId = commentsUsersModel.WriterWorkId;

            if (ModelState.IsValid)
            {
                var newComm = mapper.Map<CommentUserBO>(commentsUsersModel);
                newComm.UserId = userId;
                newComm.WriterWorkId = writerId;
                newComm.CommentsText = commentUserBO.CommentsText;
                newComm.Save();
                
                return RedirectToActionPermanent("WriterWorkPage", "WriterWork", new { id =  writerId });
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

        [ChildActionOnly]
        public ActionResult _CommentsPartialView(int writerWorkId)
        {
            var usersBO = DependencyResolver.Current.GetService<UserBO>().GetUsersList().ToList();
            var commentUserBO = DependencyResolver.Current.GetService<CommentUserBO>().GetCommentsUsersList().Where(x => x.WriterWorkId == writerWorkId).ToList();

            ViewBag.Users = usersBO.Select(a => mapper.Map<UserViewModel>(a)).ToList();
            ViewBag.CommentsUsers = commentUserBO.Select(m => mapper.Map<CommentUserViewModel>(m)).ToList();

            return PartialView();
        }
    }
}