using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace WebApiRis.Model
{
	public class RSAKey
	{
        [Key]
        public Guid Id { get; set; }
        public string? sharedParametrs { get; set; }
        public Guid AirUserId { get; set; }
        [JsonIgnore]
        public AirUser? AirUser { get; set; }
    }
}
