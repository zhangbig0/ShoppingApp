using System.ComponentModel;

namespace ShoppingApp.Share.Dto
{
    public class RegisterDto
    {
        [DisplayName("账户")] public string Account { get; set; }
        [DisplayName("密码")] public string Password { get; set; }
    }
}