using System;
using System.IO;
using System.Linq;

// Get directory path from command line or use current directory
string directory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

if (!Directory.Exists(directory))
{
    Console.WriteLine($"Directory not found: {directory}");
    return;
}

// Define file extensions to search for
var extensions = new[] {".ovl", ".hnd" };

var libToFind = new[]
{
   "libxdoc",
   "libaccount", 
   "librefinfo",
   "libmessages",
   "liboption",
   "libgenuid",
   "libatools",
   "libisocket",
   "libqoom",
   "libappa",
   "libquest",
   "libfury",
   "libcoom",
   "libboom",
   "libtypes",
};

// Get all files with specified extensions
var files = extensions
    .SelectMany(ext => Directory.GetFiles(directory, $"*{ext}"))
    .OrderBy(f => f)
    .ToList();


if (files.Count == 0)
{
    Console.WriteLine("No files found with extensions: .so, .ovl, .hnd");
    return;
}

string getVersion(string file)
{
    try
    {
        // Read file content
        string fileContent = File.ReadAllText(file);
        
        // Extract last 9 symbols from file content
        string version = fileContent.Length >= 9 
            ? fileContent.Substring(fileContent.Length - 9) 
            : fileContent;
        
        return version.Replace("\0", "").Trim(); // Remove null characters and trim whitespace
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{file} - Error reading file: {ex.Message}");
    }
    return "";
}

// Process and display results
foreach (var file in files)
{
    var fileName = Path.GetFileNameWithoutExtension(file);
    
    try
    {
        // Read file content
        string fileContent = File.ReadAllText(file);

        Console.WriteLine($"{fileName} ({getVersion(file)}): ");
        
        // Find required libraries and their versions in file content
        foreach (var lib in libToFind)
        {
            int libIndex = fileContent.IndexOf(lib, StringComparison.OrdinalIgnoreCase);
            if (libIndex >= 0)
            {
                // Extract numbers following the library name
                int startIndex = libIndex + lib.Length;
                string remainingContent = fileContent.Substring(startIndex);
                string numbers = new string(remainingContent.TakeWhile(char.IsDigit).ToArray());
                
                if (!string.IsNullOrEmpty(numbers))
                {
                    Console.WriteLine($"\t {lib}{numbers}.so ("+getVersion(Path.Combine(directory, $"{lib}{numbers}.so")) + ")");
                }
            }
        }
        //Console.Write(Environment.NewLine);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{fileName} - Error reading file: {ex.Message}");
    }
}
