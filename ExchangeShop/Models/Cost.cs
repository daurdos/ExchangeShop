using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeShop.Models
{
    public class Cost
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public DateTime? Date { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

    }
}