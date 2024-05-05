using WebApiRis.Model.DTO.Auth;
using WebApiRis.Model;
using WebApiRis.Model.DTO.Exception;

namespace WebApiRis.Repository.IRepository
{
	public interface IExceptionRepository
	{
		Task<UserExceptions> SetException(ExceptionRequestDTO exRequestDTO, Guid userId);
		Task<List<UserExceptions>> GetAllException();
	}
}
