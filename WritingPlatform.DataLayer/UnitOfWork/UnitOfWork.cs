using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WritingPlatform.DataLayer.Repositories;

namespace WritingPlatform.DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext db;
        private bool disposed = false;

        Repository<Roles> _roleUoWRepository;
        Repository<Users> _userUoWRepository;
        Repository<Genres> _genreBookUoWRepository;
        Repository<WritersWorks> _writerWorkUoWRepository;
        Repository<CommentsUsers> _commentUserBookUoWRepository;

        public UnitOfWork()
        {
            db = new Model1();
        }
        public UnitOfWork(Model1 db)
        {
            this.db = db;
            db.Database.CommandTimeout = 180;
        }

        public Repository<Roles> RoleUoWRepository
        {
            get
            {
                return _roleUoWRepository == null ? new Repository<Roles>(db) : _roleUoWRepository;
            }
        }

        public Repository<Users> UserUoWRepository
        {
            get
            {
                return _userUoWRepository == null ? new Repository<Users>(db) : _userUoWRepository;
            }
        }

        public Repository<Genres> GenreUoWRepository
        {
            get
            {
                return _genreBookUoWRepository == null ? new Repository<Genres>(db) : _genreBookUoWRepository;
            }
        }

        public Repository<WritersWorks> WriterWorkUoWRepository
        {
            get
            {
                return _writerWorkUoWRepository == null ? new Repository<WritersWorks>(db) : _writerWorkUoWRepository;
            }
        }

        public Repository<CommentsUsers> CommentUserUoWRepository
        {
            get
            {
                return _commentUserBookUoWRepository == null ? new Repository<CommentsUsers>(db) : _commentUserBookUoWRepository;
            }
        }

        public void BeginTransaction()
        {
            db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            db.Database.CurrentTransaction.Commit();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                if (disposing)
                {
                    if (db != null)
                    {
                        db.Dispose();
                    }
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }   
    }
}
