using System.ComponentModel.DataAnnotations;

namespace WebApiRis.Model.DTO.Exception
{
	public class ExceptionResponseDTO
	{
        public List<UserExceptions> Exceptions { get; set; }
    }
}
