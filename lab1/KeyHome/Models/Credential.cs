using System.Text.Json.Serialization;

namespace KeyHome.Models
{
    public class Credential
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        public string LoginMasked => new string('*', 5);
        public string PasswordMasked => new string('*', 5);
    }
}
