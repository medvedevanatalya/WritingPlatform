using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WritingPlatform.DataLayer.Repositories;

namespace WritingPlatform.DataLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Repository<Roles> RoleUoWRepository { get; }
        Repository<Users> UserUoWRepository { get; }
        Repository<Genres> GenreUoWRepository { get; }
        Repository<WritersWorks> WriterWorkUoWRepository { get; }
        Repository<CommentsUsers> CommentUserUoWRepository { get; }

        void Save();
        void BeginTransaction();
        void CommitTransaction();
    }
}
