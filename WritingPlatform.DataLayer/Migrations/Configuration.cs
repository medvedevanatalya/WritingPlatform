namespace WritingPlatform.DataLayer.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WritingPlatform.DataLayer.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WritingPlatform.DataLayer.Model1 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            Roles roleAdmin = new Roles { Id = 1, NameRole = "admin" };
            Roles roleUser = new Roles { Id = 2, NameRole = "user" };
            context.Roles.AddOrUpdate(roleAdmin); 
            context.Roles.AddOrUpdate(roleUser);

            Users user = new Users { Id = 1, EmailUser = "admin@mail.ru", LoginUser = "admin", PasswordUser = "admin", IsDelete = false, RoleId = roleAdmin.Id };
            context.Users.AddOrUpdate(user);  
        }
    }
}
