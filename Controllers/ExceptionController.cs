using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiRis.DB;
using WebApiRis.Model.DTO.Exception;
using WebApiRis.Repository.IRepository;

namespace WebApiRis.Controllers
{
	[Authorize]
	[Route("api/exception/")]
	[ApiController]
	public class ExceptionController : ControllerBase
	{
		private readonly AppDbContext _db;
		private readonly IExceptionRepository _exRepo;

        public ExceptionController(AppDbContext db, IExceptionRepository repo)
        {
			_db = db;
			_exRepo = repo;
		}

		[Route("new")]
		[HttpPost]
		public async Task<IActionResult> SetException([FromBody] ExceptionRequestDTO model)
		{
			var id = User.Identity.Name;
			var exceptionRequest = await _exRepo.SetException(model, Guid.Parse(id));
			if (exceptionRequest != null) 
			{
				return Ok(exceptionRequest);
			}
			else
			{
				return BadRequest(new { message = "Ошибка при добавлении исключения"});
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
