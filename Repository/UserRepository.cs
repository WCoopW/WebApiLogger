using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebApiRis.DB;
using WebApiRis.Model;
using WebApiRis.Model.DTO.Auth;
using WebApiRis.Repository.IRepository;

namespace WebApiRis.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private IConfiguration _configuration;

        public UserRepository(AppDbContext db, IConfiguration configuration)
        {
            _configuration = configuration;
            _db = db;
        }

        public bool IsUniqueUser(string login, string email)
        {
            var user = _db.AirUsers.FirstOrDefault(x => x.Login == login || x.Email == email);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.AirUsers.FirstOrDefault(x =>
                x.Login == loginRequestDTO.Login 
                && x.PasswordHash == loginRequestDTO.Password
            );
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    AirUser = null
                };
            }
            var issuer = _configuration.GetValue<string>("Jwt:Issuer");
            var audience = _configuration.GetValue<string>("Jwt:Audience");
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                       
                    }
                ),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            var loginResponseDTO = new LoginResponseDTO() { AirUser = user, Token = stringToken };
            return loginResponseDTO;
        }

        public async Task<AirUser> Register(RegisterRequestDTO registerRequestDTO)
        {
            AirUser airUser =
                new()
                {
                    Email = registerRequestDTO.Email,
                    PasswordHash = registerRequestDTO.Password,
                    Login = registerRequestDTO.Login
                };
            var key = new RSAKey();
            RSA rsa = RSA.Create();

			_db.Add(airUser);

            var param = rsa.ExportParameters(false);

			var jsonParam = JsonConvert.SerializeObject(param, formatting: Formatting.Indented);
            await Console.Out.WriteLineAsync(jsonParam);
            key.sharedParametrs = jsonParam;
            key.AirUser = airUser;
            _db.Add(key);
            await _db.SaveChangesAsync();
			airUser.PasswordHash = "";
			return airUser;
		}
    }
}
