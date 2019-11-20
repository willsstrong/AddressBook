using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AddressBook.Models;

namespace AddressBook.DAL
{
    public class AddressBookInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AddressContext>
    {
        protected override void Seed(AddressContext context)
        {
            //Fill Database with test data
            var people = new List<Person>
            {
                new Person{FirstName="Will", LastName="Strong",
                    Address="81 Legacy Blvd SE", City="Calgary", Province="AB", PostCode="T2X 2B9",
                    Email="willsstro@gmail.com", PhoneNumCell="403-973-2857"},

                new Person{FirstName="Stephanie", LastName="Strong",
                    Address="81 Legacy Blvd SE", City="Calgary", Province="AB", PostCode="T2X 2B9",
                    Email="stephgstrong@gmail.com", PhoneNumCell="403-669-0226"},

                new Person{FirstName="Will", LastName="Strong",
                    Address="81 Legacy Blvd SE", City="Calgary", Province="AB", PostCode="T2X 2B9",
                    Email="willsstro@gmail.com", PhoneNumMain="403-873-1104"},

                new Person{FirstName="Will", LastName="Strong",
                    Address="81 Legacy Blvd SE", City="Calgary", Province="AB", PostCode="T2X 2B9",
                    Email="willsstro@gmail.com", PhoneNumMain="403-873-1104"}
            };

            people.ForEach(s => context.Person.Add(s));
            context.SaveChanges();
        }
    }
}