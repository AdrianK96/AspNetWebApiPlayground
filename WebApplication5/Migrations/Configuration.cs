namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication5.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication5.Models.WebApplication5Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication5.Models.WebApplication5Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Owners.AddOrUpdate(o => o.Id,
                new Owner() { Id = 1, Name = "John Smith" },
                new Owner() { Id = 2, Name = "Paul Tysen" }
                );

            context.TodoItems.AddOrUpdate(t => t.Id,
                new TodoItem() { Id = 1, Name = "Dispose burnable garbage", CreatedDateTime = new DateTime(2017,1,3,13,21,44), IsCompleted=true, OwnerId=1},
                new TodoItem() { Id = 1, Name = "Buy Fuji apples", CreatedDateTime = new DateTime(2017, 1, 6, 09, 00, 21), IsCompleted = false, OwnerId = 2 }
            );
        }
    }
}
