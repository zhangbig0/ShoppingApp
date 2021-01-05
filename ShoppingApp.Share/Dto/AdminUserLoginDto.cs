using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Share.Dto
{
    public class LoginDto
    {
        [DisplayName("账户")] public string Account { get; set; }
        [DisplayName("密码")] public string Password { get; set; }
    }
}