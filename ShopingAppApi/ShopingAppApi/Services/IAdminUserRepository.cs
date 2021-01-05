using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAppApi.Entity;
using ShoppingAppApi.Infrastructure;

namespace ShoppingAppApi.Services
{
    public interface IAdminUserRepository
    {
        public AdminUser GetAdminUser(string account, string password);
        public AdminUser RegisterAdminUser(string account, string password);
    }
}
