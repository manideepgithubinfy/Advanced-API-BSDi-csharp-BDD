using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_API_BSDi_csharp_BDD.Models
{
    public class OrderRequest
    {
        public string? OrderId { get; set; }
        public Customer? Customer { get; set; }
        public List<Item>? Items { get; set; }
    }

    public class Customer
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

    public class Item
    {
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
