namespace FileHandler;



public interface IFileReader
{
    string FileRead();
}

public interface IFileWritter
{
    void FileWritte(string text);
}

public interface IFileHandler
{
    string FullPath { get; set; }
    string FileName { get; set; }
    IFileReader FileReader { get; set; }
    IFileWritter FileWritter { get; set; }

    void FileInfo();
}


public abstract class BaseFileHandler : IFileHandler
{
    public string FullPath { get; set; }
    public string FileName { get; set; }
    public IFileReader FileReader { get; set; }
    public IFileWritter FileWritter { get; set; }
    protected BaseFileHandler(string fullPath,IFileReader reader, IFileWritter writter)
    {

        FullPath = fullPath;
        FileName = Path.GetFileName(fullPath);
        FileReader = reader;
        FileWritter = writter;
    }
    public abstract void FileInfo();
}

public abstract class BaseFileReader : IFileReader
{
    public string FullPath = "";

    public BaseFileReader(string fullPath)
    {
        this.FullPath = fullPath;
    }

    public abstract string FileRead();
}

public abstract class BaseFileWritter : IFileWritter
{
    public string FullPath = "";

    public BaseFileWritter(string fullPath)
    {
        this.FullPath = fullPath;
    }
    public abstract void FileWritte(string fullPath);
}

public class CredentialJsonFileHandler : BaseFileHandler
{
    private static CredentialJsonFileHandler? _instance = null!;
    private static object _syncBoot = new object();

    private CredentialJsonFileHandler(string fullPath) : base(fullPath,new FileReader(fullPath), new FileWritter(fullPath))
    {
        
    }

    public static CredentialJsonFileHandler getInstance(string fullPath)
    {
        if (_instance == null)
        {
            lock (_syncBoot)
            {
                if (_instance == null)
                {
                    _instance = new CredentialJsonFileHandler(fullPath);
                }
            }
        }
        return _instance;
    }

    public override void FileInfo()
    {
        Console.WriteLine($"File Path: {FullPath}");
        Console.WriteLine($"File Name: {FileName}");
    }
}


public class FileReader : BaseFileReader
{
    public FileReader(string fullPath) : base(fullPath)
    {
        this.FullPath = fullPath;
    }
    public override string FileRead()
    {
        return File.ReadAllText(FullPath);
    }
}


public class FileWritter : BaseFileWritter
{
    public FileWritter(string fullPath) : base(fullPath)
    {
        this.FullPath = fullPath;
    }

    public override void FileWritte(string text)
    {
        File.WriteAllText(FullPath, text);
    }
}


