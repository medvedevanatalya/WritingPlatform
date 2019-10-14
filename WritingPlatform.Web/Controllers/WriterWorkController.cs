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
    public class WriterWorkController : Controller
    {
        protected IMapper mapper;
        public WriterWorkController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public ActionResult Index()
        {
            var writerWorkBO = DependencyResolver.Current.GetService<WriterWorkBO>().GetWritersWorksList();
            var usersBO = DependencyResolver.Current.GetService<UserBO>().GetUsersList();
            var genresBO = DependencyResolver.Current.GetService<GenreBO>().GetGenresList();

            ViewBag.WritersWorks = writerWorkBO.Select(m => mapper.Map<WriterWorkViewModel>(m)).ToList();
            ViewBag.Users = usersBO.Select(a => mapper.Map<UserViewModel>(a)).ToList();
            ViewBag.Genres = genresBO.Select(a => mapper.Map<GenreViewModel>(a)).ToList();

            return View();
        }

        public ActionResult CreateAndEdit(int? id)
        {
            var usersBO = DependencyResolver.Current.GetService<UserBO>();
            var genresBO = DependencyResolver.Current.GetService<GenreBO>();

            var writerWorkBO = DependencyResolver.Current.GetService<WriterWorkBO>();
            var writersWorksModel = mapper.Map<WriterWorkViewModel>(writerWorkBO);

            if (id == null)
            {
                ViewBag.Header = "Создание произведения";
            }
            else
            {
                var writersWorksBOList = writerWorkBO.GetWriterWorkById(id);
                writersWorksModel = mapper.Map<WriterWorkViewModel>(writersWorksBOList);
                ViewBag.Header = "Редактирование произведения";
            }
            ViewBag.Users = new SelectList(usersBO.GetUsersList().Select(m => mapper.Map<UserViewModel>(m)).ToList(), "Id", "LoginUser");
            ViewBag.Genres = new SelectList(genresBO.GetGenresList().Select(m => mapper.Map<GenreViewModel>(m)).ToList(), "Id", "NameGenre");

            return View(writersWorksModel);
        }

        [HttpPost]
        public ActionResult CreateAndEdit(WriterWorkViewModel writersWorksModel)
        {
            var writerWorkBO = mapper.Map<WriterWorkBO>(writersWorksModel);
            if (ModelState.IsValid)
            {
                writerWorkBO.Save();
                return RedirectToActionPermanent("Index", "WriterWork");
            }
            else
            {
                return View(writersWorksModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var writerWork = DependencyResolver.Current.GetService<WriterWorkBO>().GetWriterWorkById(id);
            writerWork.Delete(id);

            return RedirectToActionPermanent("Index", "WriterWork");
        }
    }
}