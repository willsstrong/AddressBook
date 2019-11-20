using System;
using System.Collections.Generic;
using AddressBook.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AddressBook.DAL
{
    public class AddressContext : DbContext
    {
        public  AddressContext() : base("AddressContext")
        {
        }

        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)  
        {
            //Cusomized conventions can be configured here

        }

    }
}