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
    public class CommentUserBO : BusinessObjectBase
    {
        private readonly IUnityContainer unityContainer;

        public int Id { get; set; }  
        public int UserId { get; set; } 
        public int WriterWorkId { get; set; }   
        public string CommentsText { get; set; }

        public CommentUserBO(IMapper mapper, UnitOfWorkFactory unitOfWorkFactory, IUnityContainer unityContainer)
            : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public CommentUserBO GetCommentUserById(int? id)
        {
            CommentUserBO commentUser;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                commentUser = unitOfWork.CommentUserUoWRepository.GetAll().Where(a => a.Id == id).Select(item => mapper.Map<CommentUserBO>(item)).FirstOrDefault();
            }
            return commentUser;
        }

        public List<CommentUserBO> GetCommentsUsersList()
        {
            List<CommentUserBO> commentsUsers = new List<CommentUserBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                commentsUsers = unitOfWork.CommentUserUoWRepository.GetAll().Select(item => mapper.Map<CommentUserBO>(item)).ToList();
            }
            return commentsUsers;
        }

        void Create(IUnitOfWork unitOfWork)
        {
            var commentUser = mapper.Map<CommentsUsers>(this);
            unitOfWork.CommentUserUoWRepository.Create(commentUser);
            unitOfWork.Save();
        }

        ////void Update(IUnitOfWork unitOfWork)
        ////{
        ////    var commentUser = mapper.Map<CommentsUsers>(this);
        ////    unitOfWork.CommentUserUoWRepository.Update(commentUser);
        ////    unitOfWork.Save();
        ////}

        public void Save()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                //if (Id != 0)
                //{
                //    Update(unitOfWork);
                //}
                //else
                //{
                Create(unitOfWork);
                //}

            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.CommentUserUoWRepository.Delete(id);
                unitOfWork.Save();
            }
        }
    }
}
