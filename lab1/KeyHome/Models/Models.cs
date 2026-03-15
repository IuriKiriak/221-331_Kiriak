using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KeyHome.Models
{
    public class CredentialList
    {
        [JsonPropertyName("credentials")]
        public List<Credential> Credentials { get; set; } = new List<Credential>();
    }
}