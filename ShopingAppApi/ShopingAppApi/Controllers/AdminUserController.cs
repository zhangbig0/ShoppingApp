using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using ShoppingApp.Share.Dto;
using ShoppingAppApi.Infrastructure;
using ShoppingAppApi.Services;

namespace ShoppingAppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly AppDbContext _context;

        public AdminUserController(IAdminUserRepository adminUserRepository, AppDbContext context)
        {
            _adminUserRepository = adminUserRepository;
            _context = context;
        }

        [HttpPost]
        public ActionResult<AdminUserLoginSuccessDto> Login([FromBody] AdminUserLoginDto adminUserLoginDto)
        {
            if (string.IsNullOrEmpty(adminUserLoginDto.Account) || string.IsNullOrEmpty(adminUserLoginDto.Password))
            {
                return BadRequest("用户名或密码为空");
            }

            try
            {
                var adminUser =
                    _adminUserRepository.LoginAdminUser(adminUserLoginDto.Account, adminUserLoginDto.Password);
                if (adminUser != null)
                {
                    var md5 = MD5.Create(adminUser.Id + adminUser.Account
                                                      + adminUser.Password);
                    return new AdminUserLoginSuccessDto
                    {
                        // SessionKey = md5.Hash,
                        Account = adminUser.Account,
                        Created = adminUser.Created,
                        Id = adminUser.Id,
                        Password = adminUser.Password,
                        Role = adminUser.Role
                    };
                }
                else
                {
                    return BadRequest("注册管理员用户失败");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        [HttpPost]
        public ActionResult<AdminUserLoginSuccessDto> Register([FromBody] RegisterDto registerDto)
        {
            var hasSameAccount = _context.AdminUser.ToList().Any(x => x.Account == registerDto.Account);
            if (hasSameAccount)
            {
                return BadRequest("账户已存在");
            }
            var adminUser =
                _adminUserRepository.RegisterAdminUser(registerDto.Account, registerDto.Password);
            if (adminUser != null)
            {
                var md5 = MD5.Create(adminUser.Id + adminUser.Account
                                                  + adminUser.Password);
                return new AdminUserLoginSuccessDto
                {
                    // SessionKey = md5.Hash,
                    Account = adminUser.Account,
                    Created = adminUser.Created,
                    Id = adminUser.Id,
                    Password = adminUser.Password,
                    Role = adminUser.Role
                };
            }
            else
            {
                return null;
            }
        }
    }
}