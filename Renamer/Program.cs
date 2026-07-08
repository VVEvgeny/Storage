
var hs = new HashSet<string>
{
    "[SilaSliva.biz] ",
    "[SuperSliv.biz] ",
    "[SlivHQ.com] ",
    "[SSL.BAND] ",
    " [SSL.BAND]",
    "[Илья Варламов] " ,
    " [Илья Варламов]" ,
    "[Boominfo.CO] ",
    "[Supersliv.biz] ",
    "[DMC.RIP] ",
    "[BOOMINFO.RU] ",
    "[n4e.ru] ",
    "[SLIV.ONE] ",
    "[Инфосклад.РФ] ",
};

var hsToRemove = new HashSet<string>
{
    "0d5d34cfa626bb37343109fc3f9760d44ddbc376f02c69ae51c206ad22be55c8",
    "f9d432870db41a34cd22e039620373dee60063564e546fb844143bf57979c132",
    "2b2a32dcc67bf92657ec3b8fefa634a4c00fbb8d1330c7f368a5218006a3b712",
    "dcaf8feb9499a5ef38033e75401c997918e03a94fc9efd12b5f78c523e0be585",
    "a025fd15ec3508437d2bd1c561c39926a0acc3eaa02bf5ee167efd81795b1259",
    "7a6aa79564fc13c37de81e8e29cc087d5778eaac4f26e21218151629b34e8f91",
    "985c5c8006fb27adece438456ee854c5c12e659c75c0733db78c98101cf90838",
};


bool removeEnabled = true;
int limitMBToCalcHash = 40;

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
int renamedFiles = 0, renamedDirs = 0, removedFiles = 0;
recRenameFiles(initialCatalog);
recRenameDirs(initialCatalog);
Console.WriteLine($"Finished. Renamed files: {renamedFiles}, Renamed directories: {renamedDirs}, Removed files: {removedFiles}");


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
        if(removeEnabled 
        && file.Length <= limitMBToCalcHash * 1024 * 1024 
        && hsToRemove.Contains(GetFileHash(file.FullName)))
        {
            try
            {
                file.Delete();
                Console.WriteLine("Removed file:"+ file.FullName);
                removedFiles++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to remove {file.FullName}: {ex.Message}");
                continue;
            }

            continue;
        }

        if(containsAny(file.Name))
        {
            var newName = getNewName(file.Name);
            Console.Write("Found file:"+ file.Name);
            file.MoveTo(Path.Combine(dirPath, newName));
            Console.Write(" Renamed to:"+ newName);
            Console.Write(Environment.NewLine);
            renamedFiles++;
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
        Console.Write("Found dir:"+ directoryInfo.Name);
        directoryInfo.MoveTo(Path.Combine(directoryInfo.Parent!.FullName, newName));
        Console.Write(" Renamed to:"+ newName);
        Console.Write(Environment.NewLine);
        renamedDirs++;
    }
}
