using System.Text.Json.Serialization;

namespace Models;

// сериализатор JSON 
public class Credentials
{
    int Id { get; set; }
    string Url {get; set; }
    string Login { get; set; }
    string Password { get; set; }
}
