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
    public class WriterWorkBO : BusinessObjectBase
    {
        private readonly IUnityContainer unityContainer;

        public int Id { get; set; } 
        public string TitleWork { get; set; }
        public string WriterWorkText { get; set; }     
        public int GenreId { get; set; }  
        public int UserId { get; set; } 
        public DateTime? PublicationDate { get; set; }

        public WriterWorkBO(IMapper mapper, UnitOfWorkFactory unitOfWorkFactory, IUnityContainer unityContainer)
            : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public WriterWorkBO GetWriterWorkById(int? id)
        {
            WriterWorkBO genre;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genre = unitOfWork.WriterWorkUoWRepository.GetAll().Where(a => a.Id == id).Select(item => mapper.Map<WriterWorkBO>(item)).FirstOrDefault();
            }
            return genre;
        }

        public List<WriterWorkBO> GetWritersWorksList()
        {
            List<WriterWorkBO> genre = new List<WriterWorkBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genre = unitOfWork.WriterWorkUoWRepository.GetAll().Select(item => mapper.Map<WriterWorkBO>(item)).ToList();
            }
            return genre;
        }

        void Create(IUnitOfWork unitOfWork)
        {
            var role = mapper.Map<WritersWorks>(this);
            unitOfWork.WriterWorkUoWRepository.Create(role);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork unitOfWork)
        {
            var genre = mapper.Map<WritersWorks>(this);
            unitOfWork.WriterWorkUoWRepository.Update(genre);
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
                unitOfWork.WriterWorkUoWRepository.Delete(id);
                unitOfWork.Save();
            }
        }
    }
}
