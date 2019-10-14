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
            WriterWorkBO writerWork;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                writerWork = unitOfWork.WriterWorkUoWRepository.GetAll().Where(a => a.Id == id).Select(item => mapper.Map<WriterWorkBO>(item)).FirstOrDefault();
            }
            return writerWork;
        }

        public List<WriterWorkBO> GetWritersWorksList()
        {
            List<WriterWorkBO> writersWorks = new List<WriterWorkBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                writersWorks = unitOfWork.WriterWorkUoWRepository.GetAll().Select(item => mapper.Map<WriterWorkBO>(item)).ToList();
            }
            return writersWorks;
        }

        void Create(IUnitOfWork unitOfWork)
        {
            var writerWork = mapper.Map<WritersWorks>(this);
            writerWork.PublicationDate = DateTime.Now;
            unitOfWork.WriterWorkUoWRepository.Create(writerWork);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork unitOfWork)
        {
            var writerWork = mapper.Map<WritersWorks>(this);
            writerWork.PublicationDate = this.PublicationDate;

            unitOfWork.WriterWorkUoWRepository.Update(writerWork);
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
