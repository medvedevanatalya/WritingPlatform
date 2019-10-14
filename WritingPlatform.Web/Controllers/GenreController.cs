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
    public class GenreController : Controller
    {
        protected IMapper mapper;

        public GenreController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var genresBO = DependencyResolver.Current.GetService<GenreBO>();
            var genresList = genresBO.GetGenresList().OrderBy(n => n.NameGenre);
            ViewBag.Genres = genresList.Select(a => mapper.Map<GenreViewModel>(a)).ToList();

            return View();
        }

        public ActionResult CreateAndEdit(int? id)
        {
            var genresBO = DependencyResolver.Current.GetService<GenreBO>();
            var genresModel = mapper.Map<GenreViewModel>(genresBO);

            if (id == null)
            {
                ViewBag.Header = "Создание Жанра";
            }
            else
            {
                var genresBOList = genresBO.GetGenreById(id);
                genresModel = mapper.Map<GenreViewModel>(genresBOList);
                ViewBag.Header = "Редактирование Жанра";
            }

            return View(genresModel);
        }

        [HttpPost]
        public ActionResult CreateAndEdit(GenreViewModel genresModel)
        {
            var genresBO = mapper.Map<GenreBO>(genresModel);
            if (ModelState.IsValid)
            {
                genresBO.Save();
                return RedirectToActionPermanent("Index", "Genre");
            }
            else
            {
                return View(genresModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var genre = DependencyResolver.Current.GetService<GenreBO>().GetGenreById(id);
            genre.Delete(id);

            return RedirectToActionPermanent("Index", "Genre");
        }
    }
}