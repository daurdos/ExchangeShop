using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeShop.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Currency> Currencies { get; set; }
    }
}