using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppApi.Entity
{
    public abstract class User
    {
        [Key] public Guid Id { get; set; }
        [DisplayName("账户")] public string Account { get; set; }
        [DisplayName("密码")] public string Password { get; set; }
        public DateTime Created { get; set; }
    }
}