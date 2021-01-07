using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppApi.Entity
{
    public class Customer:User
    {
        public string NickName { get; set; }
        public int Score { get; set; }
        public Guid ShoppingBracketId { get; set; }
        public ShoppingBracket ShoppingBracket { get; set; }
    }
}
