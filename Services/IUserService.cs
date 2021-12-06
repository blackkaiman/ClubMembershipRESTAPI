using ProiectPractica.Models.Authentication;
namespace ProiectPractica.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
