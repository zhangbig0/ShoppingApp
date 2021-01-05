using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppApi.Entity
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Accout { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
    }
}