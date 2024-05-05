using Microsoft.EntityFrameworkCore;
using WebApiRis.Cryptography;
using WebApiRis.DB;
using WebApiRis.Model;
using WebApiRis.Model.DTO.Exception;
using WebApiRis.Repository.IRepository;

namespace WebApiRis.Repository
{
    public class ExceptionRepository : IExceptionRepository
    {
		private readonly AppDbContext _db;

        public ExceptionRepository(AppDbContext db)
        {
            _db = db;
        }
		public async Task<List<UserExceptions>> GetAllException()
		{
			var list = await _db.UserExceptions.ToListAsync();
			return list;
		}
		public async Task<UserExceptions> SetException(ExceptionRequestDTO exRequestDTO, Guid userId)
		{
			UserExceptions exception = new()
			{
				Message = exRequestDTO.Message,
				dateTimeExc = exRequestDTO.dateTimeExc,
				TargetSite = exRequestDTO.TargetSite,
				IndexForm = exRequestDTO.IndexForm,
				UserId = userId,
			};
			var rsa = _db.Keys.FirstOrDefault(x => x.AirUserId == userId);

			exception.VerifySignature = SignatureRSA.VerifySignature(
				exception.Message,
				Convert.FromBase64String(exRequestDTO.signedHash),
				rsa.sharedParametrs);

			_db.Add(exception);
			await _db.SaveChangesAsync();
			return exception;
		}
	}
}
