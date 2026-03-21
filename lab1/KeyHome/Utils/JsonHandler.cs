using FileHandler;
using System.Collections.Generic;
using System.IO;

namespace JsonHandler;

public interface ICredentialJsonHandler
{
    IFileHandler File { get; set; }
}

public interface IParcerLogPas
{
    string Login { get; set; }
    string Password { get; set; }

    Dictionary<string, string> ParsingLogPas(); 
    
}

public class CredentialJsonHandler : ICredentialJsonHandler
{
    public IFileHandler File { get; set;}

    public CredentialJsonHandler()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Сredentials.json");
        File = CredentialJsonFileHandler.getInstance(filePath);
    }
}

public abstract class BaseParcerLogPas : IParcerLogPas
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    protected BaseParcerLogPas() {} 

    public abstract Dictionary<string, string> ParsingLogPas();
}

public abstract class ParcerLogPas : BaseParcerLogPas
{
    private ParcerLogPas() : base() {} 

    public override Dictionary<string, string> ParsingLogPas()
    {
        Dictionary<string, string> res = new Dictionary<string, string>();
        return res;
    }
}