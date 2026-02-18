using FileHandler;
using Models;

namespace JsonHandler;

// интерфейсы
public interface ICredentialJsonHandler
{
    IFileHandler File { get; set; }
}

public class CredentialJsonHandler : ICredentialJsonHandler
{
    public IFileHandler File { get; set;}
    
    public CredentialJsonHandler(string fullPath = "Data/credentials.json")
    {
        File = CredentialJsonFileHandler.getInstance(fullPath);
    }



}
