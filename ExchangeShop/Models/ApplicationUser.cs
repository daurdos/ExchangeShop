using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Phone { get; set; }
        public ApplicationUser()
        {
        }

    }
}