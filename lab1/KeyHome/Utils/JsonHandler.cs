using FileHandler;
using System.IO;

namespace JsonHandler;

// интерфейсы
public interface ICredentialJsonHandler
{
    IFileHandler File { get; set; }
}

public interface IParcerLogPas
{
    string Login { get; set; }
    string Password { get; set; }

    Dictionary<string, string> ParsingLogPas(); // не придумал че в него идти должно 
    
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
    public string Login { get; set; }
    public string Password { get; set; }

    protected BaseParcerLogPas() {} // по идеи отправляем сюда модель json

    public abstract Dictionary<string, string> ParsingLogPas();
}

public abstract class ParcerLogPas : BaseParcerLogPas
{
    public string Login { get; set; }
    public string Password { get; set; }

    private ParcerLogPas() : base() {} // по идеи отправляем сюда модель json

    public override Dictionary<string, string> ParsingLogPas()
    {
        Dictionary<string, string> res = new Dictionary<string, string>();
        return res;
    }
}