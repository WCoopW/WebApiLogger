using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiRis.DB;
using WebApiRis.Model.DTO.Exception;
using WebApiRis.Model.DTO.ExceptionXor;
using WebApiRis.Repository.IRepository;

namespace WebApiRis.Controllers
{
	[Route("api/xor/")]
	[ApiController]
	public class ExceptionXorController : ControllerBase
	{
		private readonly AppDbContext _db;
		private readonly IExceptionXorRepository _exRepo;

		public ExceptionXorController(AppDbContext db, IExceptionXorRepository repo)
		{
			_db = db;
			_exRepo = repo;
		}

		[Route("new")]
		[HttpPost]
		public async Task<IActionResult> SetException([FromBody] ExceptionXorRequestDTO model)
		{
			var id = User.Identity.Name;
			var exceptionRequest = await _exRepo.SetException(model, Guid.Parse(id));
			if (exceptionRequest != null)
			{
				return Ok(exceptionRequest);
			}
			else
			{
				return BadRequest(new { message = "Ошибка при добавлении исключения" });
			}
		}
		[Route("get")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var exceptions = await _exRepo.GetAllException();
			if (exceptions != null)
			{
				return Ok(exceptions);
			}
			else
			{
				return BadRequest(new { message = "Ошибка при получении исключений" });
			}

		}
	}
}
