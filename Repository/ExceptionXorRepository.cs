using Microsoft.EntityFrameworkCore;
using WebApiRis.Cryptography;
using WebApiRis.DB;
using WebApiRis.Model;
using WebApiRis.Model.DTO.ExceptionXor;
using WebApiRis.Repository.IRepository;

namespace WebApiRis.Repository
{
	public class ExceptionXorRepository : IExceptionXorRepository
	{
		private readonly AppDbContext _db;
		private IConfiguration _configuration;

		public ExceptionXorRepository(AppDbContext db, IConfiguration configuration)
		{
			_db = db;
			_configuration = configuration;
		}
		public async Task<List<AirUserExceptions>> GetAllException()
		{
			var secretKey = _configuration.GetValue<string>("XORKey:SecretKey");
			var list = await _db.AirUserExceptions.ToListAsync();
			foreach (var exception in list) 
			{
				exception.Message = XORCipher.Encrypt(exception.Message, secretKey);
				exception.TargetSite = XORCipher.Decrypt(exception.TargetSite, secretKey);
			}
			return list;
		}

		public async Task<AirUserExceptions> SetException(ExceptionXorRequestDTO exRequestDTO, Guid userId)
		{
			var secretKey = _configuration.GetValue<string>("XORKey:SecretKey");

			AirUserExceptions exception = new()
			{
				Message = XORCipher.Encrypt(exRequestDTO.Message, secretKey),
				dateTimeExc = exRequestDTO.dateTimeExc,
				TargetSite = XORCipher.Encrypt(exRequestDTO.TargetSite, secretKey),
				IndexForm = exRequestDTO.IndexForm,
				UserId = userId,
			};
			_db.Add(exception);
			await _db.SaveChangesAsync();
			return exception;
		}
	}
}
