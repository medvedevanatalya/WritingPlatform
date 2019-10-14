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
    public class UserBO : BusinessObjectBase
    {
        private readonly IUnityContainer unityContainer;

        public int Id { get; set; }
        public string LoginUser { get; set; }
        public string EmailUser { get; set; }
        public string PasswordUser { get; set; } 
        public int RoleId { get; set; }  
        public bool? IsDelete { get; set; }

        public UserBO(IMapper mapper, UnitOfWorkFactory unitOfWorkFactory, IUnityContainer unityContainer)
        : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public UserBO GetUserById(int? id)
        {
            UserBO genre;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genre = unitOfWork.UserUoWRepository.GetAll().Where(a => a.Id == id).Select(item => mapper.Map<UserBO>(item)).FirstOrDefault();
            }
            return genre;
        }

        public List<UserBO> GetUsersList()
        {
            List<UserBO> genre = new List<UserBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genre = unitOfWork.UserUoWRepository.GetAll().Select(item => mapper.Map<UserBO>(item)).ToList();
            }
            return genre;
        }

        void Create(IUnitOfWork unitOfWork)
        {
            var role = mapper.Map<Users>(this);
            unitOfWork.UserUoWRepository.Create(role);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork unitOfWork)
        {
            var genre = mapper.Map<Users>(this);
            unitOfWork.UserUoWRepository.Update(genre);
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
                unitOfWork.UserUoWRepository.Delete(id);
                unitOfWork.Save();
            }
        }
    }
}
