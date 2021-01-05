using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppApi.Services;

namespace ShoppingAppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserRepository _adminUserRepository;

        public AdminUserController(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }

        [HttpGet]
        public ActionResult<object> Login(string account, string password)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                return "账号或密码为空";
            }
            try
            {
                var adminUser = _adminUserRepository.GetAdminUser(account, password);
                if (adminUser != null)
                {
                    var md5 = MD5.Create(adminUser.Id + adminUser.Accout
                                                      + adminUser.Password);
                    return new
                    {
                        sessionKey = md5.Hash,
                        LogonUser = adminUser.Accout
                    };
                }
                else
                {
                    return "账号或密码错误";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        [HttpPost]
        public ActionResult<object> Register(string account, string password)
        {
            var adminUser = _adminUserRepository.RegisterAdminUser(account, password);
            if (adminUser != null)
            {
                return new
                {
                    code = 0,
                    adminUser
                };
            }
            else
            {
                return new
                {
                    code = 1,
                };
            }
        }
    }
}