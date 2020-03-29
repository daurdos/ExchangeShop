using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeShop.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("IdentityDb") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Cost> Costs { get; set; }

    }
}