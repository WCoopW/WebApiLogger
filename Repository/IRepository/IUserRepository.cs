using WebApiRis.Model;
using WebApiRis.Model.DTO.Auth;

namespace WebApiRis.Repository.IRepository
{
    public interface IUserRepository
	{
		bool IsUniqueUser(string login, string email);
		Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
		Task<AirUser> Register(RegisterRequestDTO registerRequestDTO);
	}
}
