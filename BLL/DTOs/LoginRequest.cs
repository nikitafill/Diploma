using System.Text.Json.Serialization;

namespace DiplomaProject.BLL.DTOs
{
    public class LoginRequest
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
