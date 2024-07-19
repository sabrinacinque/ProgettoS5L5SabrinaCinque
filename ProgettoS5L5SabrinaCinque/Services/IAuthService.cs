using ProgettoS5L5SabrinaCinque.Models;

namespace ProgettoS5L5SabrinaCinque.Services
{
    public interface IAuthService
    {
        ApplicationUser Login(string username, string password);
    }
}
