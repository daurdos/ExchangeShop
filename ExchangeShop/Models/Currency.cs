using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeShop.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Cost> Costs { get; set; }


    }
}