using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAppApi.Entity
{
    public class Goods
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Class { get; set; }
    }
}