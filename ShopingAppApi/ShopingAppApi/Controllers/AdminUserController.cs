using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShoppingApp.Share.Dto;
using ShoppingAppApi.Entity;
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
        private readonly IMapper _mapper;

        public AdminUserController(IAdminUserRepository adminUserRepository, AppDbContext context, IMapper mapper)
        {
            _adminUserRepository = adminUserRepository;
            _context = context;
            _mapper = mapper;
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
                    return _mapper.Map<AdminUserLoginSuccessDto>(adminUser);
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
                return _mapper.Map<AdminUserLoginSuccessDto>(adminUser);
            }
            else
            {
                return BadRequest("用户不存在");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoodsDto>>> Index()
        {
            var adminUsers = await _context.AdminUser.ToListAsync();
            return _mapper.Map<List<GoodsDto>>(adminUsers);
        }

        [HttpPost]
        public async Task<ActionResult<AdminUserDto>> Add(AdminUserAddOrUpdateDto adminUserAddOrUpdateDto)
        {
            var adminUser = _mapper.Map<AdminUserAddOrUpdateDto, AdminUser>(adminUserAddOrUpdateDto);
            adminUser.Id = Guid.NewGuid();

            var entry = await _context.AdminUser.AddAsync(adminUser);
            if (entry != null)
            {
                return _mapper.Map<AdminUser, AdminUserDto>(adminUser);
            }

            return BadRequest("用户不存在");
        }

        [HttpPost("{adminUserId}")]
        public async Task<ActionResult<AdminUserDto>> Update(Guid adminUserId,
            AdminUserAddOrUpdateDto adminUserAddOrUpdateDto)
        {
            var adminUser = _mapper.Map<AdminUserAddOrUpdateDto, AdminUser>(adminUserAddOrUpdateDto);
            adminUser.Id = adminUserId;

            var entry = _context.AdminUser.Update(adminUser);
            if (entry != null)
            {
                return _mapper.Map<AdminUser, AdminUserDto>(adminUser);
            }

            return BadRequest("用户不存在");
        }

        [HttpPost]
        public async Task<ActionResult<AdminUserDto>> Delete([FromBody] IEnumerable<Guid> adminUserId)
        {
            var adminUser = await _context.AdminUser.FindAsync(adminUserId);
            if (adminUser != null)
            {
                return _mapper.Map<AdminUser, AdminUserDto>(adminUser);
            }

            return BadRequest("用户不存在");
        }
    }
}