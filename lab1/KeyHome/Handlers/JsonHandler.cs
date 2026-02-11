using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using KeyHome.Models;

namespace KeyHome.Handlers
{
    public class JsonHandler
    {
        private readonly string _filePath;

        public JsonHandler(string filePath)
        {
            _filePath = filePath;
        }

        public List<Credential> LoadCredentials()
        {
            if (!File.Exists(_filePath))
                return new List<Credential>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Credential>>(json) ?? new List<Credential>();
        }
    }
}
