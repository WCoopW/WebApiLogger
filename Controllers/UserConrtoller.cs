using Microsoft.AspNetCore.Mvc;
using WebApiRis.DB;
using WebApiRis.Model.DTO.Auth;
using WebApiRis.Repository.IRepository;

namespace WebApiRis.Controllers
{
    [ApiController]
	[Route("api/")]
	public class UserConrtoller : ControllerBase
	{
		private readonly AppDbContext _db;
		private readonly IUserRepository _userRepo;

        public UserConrtoller(AppDbContext db, IUserRepository repo)
        {
            _db = db;
			_userRepo = repo;
        }

		[Route("login")]
		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
		{
			var loginResponse = await _userRepo.Login(model);
			if (loginResponse.AirUser == null || string.IsNullOrEmpty(loginResponse.Token)) 
			{
				return BadRequest(new { message = "Login or password is incorrect" });
			}
			return Ok(loginResponse);
		}

		[Route("register")]
		[HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
		{
			bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Login, model.Email);
			if (!ifUserNameUnique) 
			{
				return BadRequest(new { message = "Login or Email already exists" });
			}
			var user = await _userRepo.Register(model);
			if (user == null) 
			{
				return BadRequest(new { message = "Error while registering" });
			}
			return Ok();
		}
	}
}
