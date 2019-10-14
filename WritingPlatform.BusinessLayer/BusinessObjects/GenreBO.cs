using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using WritingPlatform.DataLayer;
using WritingPlatform.DataLayer.UnitOfWork;

namespace WritingPlatform.BusinessLayer.BusinessObjects
{
    public class GenreBO : BusinessObjectBase
    {
        private readonly IUnityContainer unityContainer;

        public int Id { get; set; } 
        public string NameGenre { get; set; }

        public GenreBO(IMapper mapper, UnitOfWorkFactory unitOfWorkFactory, IUnityContainer unityContainer)
            : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public GenreBO GetGenreById(int? id)
        {
            GenreBO genre;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genre = unitOfWork.GenreUoWRepository.GetAll().Where(a => a.Id == id).Select(item => mapper.Map<GenreBO>(item)).FirstOrDefault();
            }
            return genre;
        }

        public List<GenreBO> GetGenresList()
        {
            List<GenreBO> genres = new List<GenreBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genres = unitOfWork.GenreUoWRepository.GetAll().Select(item => mapper.Map<GenreBO>(item)).ToList();
            }
            return genres;
        }

        void Create(IUnitOfWork unitOfWork)
        {
            var genre = mapper.Map<Genres>(this);
            unitOfWork.GenreUoWRepository.Create(genre);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork unitOfWork)
        {
            var genre = mapper.Map<Genres>(this);
            unitOfWork.GenreUoWRepository.Update(genre);
            unitOfWork.Save();
        }

        public void Save()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                if (Id != 0)
                {
                    Update(unitOfWork);
                }
                else
                {
                    Create(unitOfWork);
                }
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.GenreUoWRepository.Delete(id);
                unitOfWork.Save();
            }
        }
    }
}
