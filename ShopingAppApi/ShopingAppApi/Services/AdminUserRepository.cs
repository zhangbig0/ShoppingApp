using System;
using System.Collections.Generic;
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

        public IEnumerable<AdminUser> GetAdminUser()
        {
            return _context.AdminUser.AsEnumerable();
        }

        public AdminUser LoginAdminUser(string account, string password)
        {
            var adminUser = _context.AdminUser.Single(x => x.Account == account && x.Password == password);
            if (adminUser != null)
            {
                return adminUser;
            }
            else
            {
                return null;
            }
        }

        public AdminUser RegisterAdminUser(string account, string password)
        {
            var adminUser = new AdminUser
            {
                Account = account,
                Password = password,
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                Role = "管理员"
            };
            _context.AdminUser.Add(adminUser);
            _context.SaveChanges();
            return adminUser;
        }
    }
}