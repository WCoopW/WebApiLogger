using System.Text.Json.Serialization;

namespace WebApiRis.Model.DTO.Auth
{
    public class LoginResponseDTO
    {
        [JsonIgnore]
        public AirUser AirUser { get; set; }
        public string Token { get; set; }
    }
}
