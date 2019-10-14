﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Unity;
using WritingPlatform.DataLayer; 
using WritingPlatform.DataLayer.UnitOfWork;
using Roles = WritingPlatform.DataLayer.Roles;

namespace WritingPlatform.BusinessLayer.BusinessObjects
{
    public class RoleBO : BusinessObjectBase
    {
        private readonly IUnityContainer unityContainer;

        public int Id { get; set; }
        public string NameRole { get; set; }

        public RoleBO(IMapper mapper, UnitOfWorkFactory unitOfWorkFactory, IUnityContainer unityContainer)
            : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public RoleBO GetRoleById(int? id)
        {
            RoleBO genre;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genre = unitOfWork.RoleUoWRepository.GetAll().Where(a => a.Id == id).Select(item => mapper.Map<RoleBO>(item)).FirstOrDefault();
            }
            return genre;
        }

        public List<RoleBO> GetRolesList()
        {
            List<RoleBO> genre = new List<RoleBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                genre = unitOfWork.RoleUoWRepository.GetAll().Select(item => mapper.Map<RoleBO>(item)).ToList();
            }
            return genre;
        }

        void Create(IUnitOfWork unitOfWork)
        {
            var role = mapper.Map<Roles>(this);
            unitOfWork.RoleUoWRepository.Create(role);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork unitOfWork)
        {
            var genre = mapper.Map<Roles>(this);
            unitOfWork.RoleUoWRepository.Update(genre);
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
                unitOfWork.RoleUoWRepository.Delete(id);
                unitOfWork.Save();
            }
        }
    }
}
