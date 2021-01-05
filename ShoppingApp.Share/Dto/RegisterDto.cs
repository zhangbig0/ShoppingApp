using System.ComponentModel;

namespace ShoppingApp.Share.Dto
{
    public class RegisterDto
    {
        [DisplayName("账户")] public string Account { get; set; }
        [DisplayName("密码")] public string Password { get; set; }
        [DisplayName("再次输入密码")] public string PasswordTwice { get; set; }
    }
}