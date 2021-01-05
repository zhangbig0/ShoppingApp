using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppApi.Entity
{
    public class AdminUser:User
    {   
        public string Role { get; set; }
    }
}
