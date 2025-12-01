using BSEB_CoreAPI.Model;

namespace BSEB_CoreAPI.Services
{
    public interface ILoginService
    {
        Task<LoginResult> LoginUserAsync(string username, string password);
    }
}
