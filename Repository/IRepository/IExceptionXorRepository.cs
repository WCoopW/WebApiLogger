using WebApiRis.Model.DTO.Exception;
using WebApiRis.Model;
using WebApiRis.Model.DTO.ExceptionXor;

namespace WebApiRis.Repository.IRepository
{
	public interface IExceptionXorRepository
	{
		Task<AirUserExceptions> SetException(ExceptionXorRequestDTO exRequestDTO, Guid userId);
		Task<List<AirUserExceptions>> GetAllException();
	}
}
