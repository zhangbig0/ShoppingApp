using System.Threading.Tasks;

namespace BootstrapBlazorApp.Shared.Services
{
    public interface IAuthentication
    {
        public Task AuthenticateValidate();
    }
}