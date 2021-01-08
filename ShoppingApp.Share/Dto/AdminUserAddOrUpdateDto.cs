using System;
using System.ComponentModel;

namespace ShoppingApp.Share.Dto
{
    public class AdminUserAddOrUpdateDto
    {
        [DisplayName("账户")] public string Account { get; set; }
        [DisplayName("密码")] public string Password { get; set; }
        public string Role { get; set; }
    }
}