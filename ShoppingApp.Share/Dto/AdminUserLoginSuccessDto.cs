using System;
using System.ComponentModel;

namespace ShoppingApp.Share.Dto
{
    public class AdminUserLoginSuccess
    {
        public Guid Id { get; set; }
        [DisplayName("账户")] public string Account { get; set; }
        [DisplayName("密码")] public string Password { get; set; }
        public DateTime Created { get; set; }
        public string Role { get; set; }
    }
}