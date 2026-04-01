using System;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;

#pragma warning disable

class FileTimestampChanger
{
    static DateTime ParseDateFromPath(string path)
    {
        var dateParts = Path.GetRelativePath(Environment.CurrentDirectory, path).Split(Path.DirectorySeparatorChar);
        var dtFromDir = dateParts[dateParts.Length - 2];
        if (dtFromDir.Split(".").Length != 3)
        {
            Console.WriteLine("Неверный формат даты в пути. Ожидается: гггг.мм.дд");
            return DateTime.MinValue;
        }
        if (dtFromDir.EndsWith(".00"))
        {
            dtFromDir = dtFromDir.Replace(".00", ".01");
        }
        if (dtFromDir.Contains(".00."))
        {
            dtFromDir = dtFromDir.Replace(".00.", ".01.");
        }

        if (!DateTime.TryParseExact(dtFromDir, "yyyy.MM.dd", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateTime dateParsed))
        {
            Console.WriteLine("Неверный формат даты в пути. Ожидается: гггг.мм.дд");
            return DateTime.MinValue;
        }

        Console.WriteLine($"{dtFromDir} -> {dateParsed:dd/MM/yyyy}");
        return dateParsed;
    }

    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Использование: program.exe <путь_к_файлу_или_каталогу> <дата>");
            Console.WriteLine("Пример для файла: program.exe C:\\file.txt \"15/06/2023\"");
            Console.WriteLine("Пример для каталога: program.exe C:\\MyFolder \"15/06/2023\"");
            return;
        }

        string path = args[0];

        DateTime dateParsed = DateTime.MinValue;
        if(args.Length == 1)
        {
            var dtFromDir = ParseDateFromPath(path);
            Console.WriteLine(""+dtFromDir+" -> "+dateParsed.ToString("dd/MM/yyyy"));
        }

        if(args.Length == 2)
        if (!DateTime.TryParse(args[1], CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out dateParsed))
        {
            Console.WriteLine("Неверный формат даты. Используйте: ДД/MM/ГГГГ");
            return;
        }

        if(dateParsed == DateTime.MinValue)
        {
            var dateParts2 = Path.GetRelativePath(Environment.CurrentDirectory, args[0]).Split(Path.DirectorySeparatorChar);
            var dtFromDir2 = dateParts2[dateParts2.Length - 2];
            if(dtFromDir2.Length == 4 && int.TryParse(dtFromDir2, out int year) && year > 1900 && year < 2100)
            {
                string rootPath = args[0];
                if (!Directory.Exists(rootPath))
                {
                    Console.WriteLine($"Каталог не найден: {rootPath}");
                    return;
                }

                var directories = Directory.GetDirectories(rootPath);
                Parallel.ForEach(directories, d =>
                {
                    var dtFromDir = ParseDateFromPath(d + Path.DirectorySeparatorChar);
                    if (dtFromDir != DateTime.MinValue)
                    {
                        ProcessDirectory(d, dtFromDir);
                    }
                });

                Console.WriteLine($"\nИтого файлов{ignoredCount+successCount+errorCount}: успешно изменено {successCount} файлов, ошибок: {errorCount}, пропущено: {ignoredCount}");
                return;
            }
            Console.WriteLine("Дата не указана или не распознана. Используйте: ДД/MM/ГГГГ");
            return;
        }

        // Установить время на 12:00:00
        DateTime newDate = new DateTime(dateParsed.Year, dateParsed.Month, dateParsed.Day, 12, 0, 0);

        // Проверка, является ли путь каталогом
        if (Directory.Exists(path))
        {
            ProcessDirectory(path, newDate);
            Console.WriteLine($"\nИтого файлов {ignoredCount+successCount+errorCount}: успешно изменено {successCount} файлов, ошибок: {errorCount}, пропущено: {ignoredCount}");
            return;
        }

        // Проверка, является ли путь файлом
        if (File.Exists(path))
        {
            ProcessFile(path, newDate);
            return;
        }

        Console.WriteLine($"Путь не найден: {path}");
    }

    static void ProcessFile(string filePath, DateTime newDate)
    {
        try
        {
            // поменять даты
            File.SetCreationTime(filePath, newDate);
            File.SetLastWriteTime(filePath, newDate);

            // переименовывание закомментировано по просьбе пользователя
            /*
            // переименовать файл в формат гггг.мм.дд
            string directory = Path.GetDirectoryName(filePath) ?? string.Empty;
            string ext = Path.GetExtension(filePath);
            string baseName = $"{newDate:yyyy.MM.dd}";
            string newName = baseName + ext;
            string newPath = Path.Combine(directory, newName);
            int counter = 2;
            while (File.Exists(newPath) && !string.Equals(newPath, filePath, StringComparison.OrdinalIgnoreCase))
            {
                newName = $"{baseName}-{counter}{ext}";
                newPath = Path.Combine(directory, newName);
                counter++;
            }

            if (!string.Equals(filePath, newPath, StringComparison.OrdinalIgnoreCase))
            {
                File.Move(filePath, newPath);
                Console.WriteLine($"Файл переименован: {Path.GetFileName(newPath)}");
            }
            */

            Console.WriteLine($"Дата файла успешно изменена на: {newDate}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Ошибка: нет прав доступа к файлу.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static int successCount = 0;
    static int errorCount = 0;
    static int ignoredCount = 0;

    static void ProcessDirectory(string directoryPath, DateTime newDate)
    {
        try
        {
            string[] files = Directory.GetFiles(directoryPath);
            
            if (files.Length == 0)
            {
                Console.WriteLine($"В каталоге нет файлов: {directoryPath}");
                return;
            }

            foreach (string filePath in files)
            {
                try
                {
                    bool procesed = false;
                    var originalCreationTime = File.GetCreationTime(filePath);
                    if (originalCreationTime.Year != newDate.Year)
                    {
                        File.SetCreationTime(filePath, newDate);
                        procesed = true;
                    }
                    var originalLastWriteTime = File.GetLastWriteTime(filePath);
                    if (originalLastWriteTime.Year != newDate.Year)
                    {
                        File.SetLastWriteTime(filePath, newDate);
                        procesed = true;
                    }

                    {
                        //exif
                        try
                        {
                            byte[] fileBytes = File.ReadAllBytes(filePath);
                            using (var inMs = new MemoryStream(fileBytes))
                            using (var image = new Bitmap(inMs))
                            {
                                PropertyItem[] propItems = image.PropertyItems;
                                bool found = false;
                                foreach (PropertyItem propItem in propItems)
                                {
                                    if (propItem.Id == 0x9003 || propItem.Id == 0x9004 || propItem.Id == 306)
                                    {
                                        string existingDate = System.Text.Encoding.ASCII.GetString(propItem.Value).TrimEnd('\0');
                                        //Console.WriteLine($"Found EXIF time property {propItem.Id}: {existingDate}");
                                        if (DateTime.TryParse(existingDate, out DateTime dt) )
                                        {
                                            if(dt.Year == newDate.Year)
                                            {
                                                continue; // уже совпадает по году, пропускаем
                                            }
                                            else
                                            {
                                                newDate = new DateTime(newDate.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                                            }
                                        }
                                        string dateStr = newDate.ToString("yyyy:MM:dd HH:mm:ss");
                                        byte[] dateBytes = System.Text.Encoding.ASCII.GetBytes(dateStr + '\0');
                                        propItem.Value = dateBytes;
                                        image.SetPropertyItem(propItem);
                                        found = true;
                                    }
                                }
                                if (found)
                                {
                                    using (var outMs = new MemoryStream())
                                    {
                                        image.Save(outMs, image.RawFormat);
                                        File.WriteAllBytes(filePath, outMs.ToArray());
                                    }
                                    procesed = true;
                                }
                            }
                        }
                        catch(Exception ex )
                        {
                            // Игнорировать ошибки при работе с EXIF, так как не все файлы поддерживают эти данные
                            Console.WriteLine($"⚠ {Path.GetFileName(filePath)} - не удалось изменить EXIF данные (возможно, не изображение) - {ex.Message}");
                            errorCount++;
                            if(procesed)
                            {
                                successCount--;
                            }
                            else
                            {
                                ignoredCount--;
                            }
                        }
                    }
                    Console.WriteLine($"✓ {Path.GetFileName(filePath)} - {(procesed ? "дата изменена" : "уже соответствует новой дате, пропущено")}");
                    if(procesed)
                    {
                        successCount++;
                    }
                    else
                    {
                        ignoredCount++;
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"✗ {Path.GetFileName(filePath)} - нет прав доступа");
                    errorCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ {Path.GetFileName(filePath)} - {ex.Message}");
                    errorCount++;
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Ошибка: нет прав доступа к каталогу.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке каталога: {ex.Message}");
        }
    }
}
