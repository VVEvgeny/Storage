Console.WriteLine("start");

const string workDir = @"c:\_Cources\";
string[] templates = new string[] {"[InfoViruS.BiZ] ","[SuperSliv.biz] ","[M1.Boominfo.ORG] ","[BOOMINFO.ORG] ","[SW.BAND] ","[InfoRai.NET] ","[InfoVolna.com] ",
"[BOOMINFO.RU] ","[InfoBank.me] ","[SLIV.SITE] ","[SuperSliv.biz]_"};

for(int i=0;i< templates.Length;i++)
{
    templates[i] = templates[i].ToLower();
}

Counter files=new(),dirs=new();
Rename(workDir);
System.Console.WriteLine($"Dirs changed: {dirs.Changed}/{dirs.Total}");
System.Console.WriteLine($"Files changed: {files.Changed}/{files.Total}");

void Rename(string path)
{
    dirs.Total++;
    var d = new DirectoryInfo(path);
    foreach(var dir in d.GetDirectories())
    {
        Rename(dir.FullName);
    }
    
    foreach(var file in d.GetFiles())
    {
        files.Total++;
        if(file.Name.TryStarstWith(out var t, templates))
        {
            System.Console.WriteLine("rename file:"+file.Name+" remove:"+t);
            File.Move(file.FullName, file.FullName.Replace(file.Name, file.Name.Remove(0, t.Length)));
            files.Changed++;
        }
    }

    foreach(var dir in d.GetDirectories())
    {
        if(dir.Name.TryStarstWith(out var t, templates))
        {
            System.Console.WriteLine("rename dir:"+dir.Name+" remove:"+t);
            Directory.Move(dir.FullName, dir.FullName.Replace(dir.Name, dir.Name.Remove(0, t.Length)));
            dirs.Changed++;
        }
    }
}

struct Counter 
{
    public Counter()
    {
        Total=0;
        Changed=0;
    }
    public int Total;
    public int Changed;
}

public static class NameExt
{
    public static bool TryStarstWith(this string name, out string template, params string[] templates)
    {
        foreach(var t in templates)
        {
            if(name.ToLower().StartsWith(t))
            {
                template = t;
                return true;
            }
        }
        template = string.Empty;
        return false;
    }
}