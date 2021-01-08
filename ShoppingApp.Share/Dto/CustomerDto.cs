using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Share.Dto
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        [DisplayName("账户")] public string Account { get; set; }
        [DisplayName("密码")] public string Password { get; set; }
        public string NickName { get; set; }
        public int Score { get; set; }
    }
}