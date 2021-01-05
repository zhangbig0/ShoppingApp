using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingAppApi.Entity;
using ShoppingAppApi.Infrastructure;

namespace ShoppingAppApi.Services
{
    public class AdminUserRepository : IAdminUserRepository
    {
        readonly AppDbContext _context;

        public AdminUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public AdminUser GetAdminUser(string account, string password)
        {
            return _context.AdminUser.Single(user => user.Accout == account && user.Password == password);
        }

        public AdminUser RegisterAdminUser(string account, string password)
        {
            var AdminUser = new AdminUser
            {
                Accout = account,
                Password = password,
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
            };
            _context.AdminUser.Add(AdminUser);
            return AdminUser;
        }
    }
}