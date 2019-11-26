namespace AddressBook.Migrations
{
    using AddressBook.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AddressBook.DAL.AddressContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AddressBook.DAL.AddressContext context)
        {
            var person = new List<Person>
            {
                new Person { FirstName = "Will",   LastName = "Strong"},
                new Person { FirstName = "Mike",   LastName = "Anderson" },
                new Person { FirstName = "Stephanie",   LastName = "Lee" },
                new Person { FirstName = "Linda",   LastName = "Zacharius" }

            };
            person.ForEach(s => context.Person.AddOrUpdate(p => p.LastName));
            context.SaveChanges();
        }
    }
}
