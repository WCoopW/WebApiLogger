using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiRis.Model
{
	public class AirUser
	{
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
		public string? PasswordHash { get; set; }
        [JsonIgnore]
        public RSAKey? RSAKey { get; set; } = null!;
    }
}
