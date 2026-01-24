
var hs = new HashSet<string>
{
    "[SilaSliva.biz] ",
    "[SuperSliv.biz] ",
    "[SlivHQ.com] ",
    "[SSL.BAND] ",
    " [SSL.BAND]",
    "[Илья Варламов] " ,
    " [Илья Варламов]" ,
};

var hsToRemove = new HashSet<string>
{
    "0d5d34cfa626bb37343109fc3f9760d44ddbc376f02c69ae51c206ad22be55c8",
    "f9d432870db41a34cd22e039620373dee60063564e546fb844143bf57979c132",
    "2b2a32dcc67bf92657ec3b8fefa634a4c00fbb8d1330c7f368a5218006a3b712"
};

bool removeEnabled = true;

bool containsAny(string name)
{
    foreach (var prefix in hs)
    {
        if (name.Contains(prefix))
            return true;
    }
    return false;
}

string getNewName(string oldName)
{
    foreach (var prefix in hs)
    {
        if (oldName.Contains(prefix))
            return oldName.Replace(prefix, "");
    }
    return oldName;
}

Console.Write("Select option:\n\t1-Work\n\t2-Get hash:");

if(Console.ReadLine() == "2")
{
    Console.Write("file path:");
    string filePath = Console.ReadLine() ?? "";
    var file = new FileInfo(filePath);
    if (file.Exists)
    {
        Console.WriteLine("File hash: " + GetFileHash(file.FullName));
    }
    else
    {
        Console.WriteLine("File does not exist.");
    }
    return;
}

Console.Write("Catalog:");
string initialCatalog = Console.ReadLine() ?? "";

var dir = new DirectoryInfo(initialCatalog);
if (!dir.Exists)
{
    Console.WriteLine("The specified directory does not exist.");
    return;
}

recRenameFiles(initialCatalog);
recRenameDirs(initialCatalog);



string GetFileHash(string filePath)
{
    using var sha256 = System.Security.Cryptography.SHA256.Create();
    using var stream = File.OpenRead(filePath);
    var hashBytes = sha256.ComputeHash(stream);
    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
}

void recRenameFiles(string dirPath)
{
    var directoryInfo = new DirectoryInfo(dirPath);

    foreach (var directory in directoryInfo.GetDirectories())
        recRenameFiles(directory.FullName);

    foreach (var file in directoryInfo.GetFiles())
    {
        if(removeEnabled && hsToRemove.Contains(GetFileHash(file.FullName)))
        {
            Console.WriteLine("Removed file:"+ file.FullName);
            file.Delete();
            continue;
        }

        if(containsAny(file.Name))
        {
            var newName = getNewName(file.Name);
            Console.Write("Found file:"+ file.FullName);
            file.MoveTo(Path.Combine(dirPath, newName));
            Console.Write(" Renamed to:"+ newName);
            Console.Write(Environment.NewLine);
        }
    }
}

void recRenameDirs(string dirPath)
{
    var directoryInfo = new DirectoryInfo(dirPath);

    //Console.WriteLine("Look dir:"+ dirPath);

    foreach (var directory in directoryInfo.GetDirectories())
        recRenameDirs(directory.FullName);

    if(containsAny(directoryInfo.Name))
    {
        var newName = getNewName(directoryInfo.Name);
        Console.Write("Found dir:"+ directoryInfo.FullName);
        directoryInfo.MoveTo(Path.Combine(directoryInfo.Parent!.FullName, newName));
        Console.Write(" Renamed to:"+ newName);
        Console.Write(Environment.NewLine);
    }
}
