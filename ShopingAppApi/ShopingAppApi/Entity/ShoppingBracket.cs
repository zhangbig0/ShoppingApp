using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppApi.Entity
{
    public class ShoppingBracket
    {
        [Key]
        public Guid Id { get; set; }
        public IList<Goods> GoodsList { get; set; }
        public int Count { get; set; }
        public Customer Customer { get; set; }
    }
}
