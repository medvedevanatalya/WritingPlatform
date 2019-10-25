using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingPlatform.DataLayer
{
    public class WritingPlatformInitializer : CreateDatabaseIfNotExists<Model1>
    {
        protected override void Seed(Model1 context)
        {
            List<Roles> roles = new List<Roles>();
            roles.Add(new Roles { Id = 1, NameRole = "admin" });
            roles.Add(new Roles { Id = 2, NameRole = "user" });
            context.Roles.AddRange(roles);

            //Roles roleAdmin = new Roles { Id = 1, NameRole = "admin" };
            //Roles roleUser = new Roles { Id = 2, NameRole = "user" };
            //context.Roles.AddOrUpdate(roleAdmin);
            //context.Roles.AddOrUpdate(roleUser);


            List<Users> users = new List<Users>();
            users.Add(new Users { Id = 1, EmailUser = "admin@mail.ru", LoginUser = "admin", PasswordUser = "admin", IsDelete = false, RoleId = 1 });
            users.Add(new Users { Id = 2, EmailUser = "user@mail.ru", LoginUser = "user", PasswordUser = "user", IsDelete = false, RoleId = 2 });
            context.Users.AddRange(users);
            //Users userAdmin = new Users { Id = 1, EmailUser = "admin@mail.ru", LoginUser = "admin", PasswordUser = "admin", IsDelete = false, RoleId = roleAdmin.Id };
            //Users user = new Users { Id = 2, EmailUser = "user@mail.ru", LoginUser = "user", PasswordUser = "user", IsDelete = false, RoleId = roleUser.Id };
            //context.Users.AddOrUpdate(userAdmin);
        

            //List<Genres> genres = new List<Genres>();
            //genres.Add(new Genres { Id = 1, NameGenre = "genre1" });
            //genres.Add(new Genres { Id = 2, NameGenre = "genre2" });
            //context.Genres.AddRange(genres);

            //List<WritersWorks> books = new List<WritersWorks>();
            //books.Add(new WritersWorks { UserId = user.Id, GenreId = 1, TitleWork = "WritersWorks1" });
            //books.Add(new WritersWorks { UserId = 2, GenreId = 2, TitleWork = "WritersWorks2" });
            //context.WritersWorks.AddRange(books);

            base.Seed(context);
        }
    }
}
