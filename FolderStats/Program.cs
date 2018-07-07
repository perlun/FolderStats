using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderStats
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var rootFolder = args.First();
                var folders = Directory.GetDirectories(rootFolder);

                foreach (var folder in folders)
                {
                    GetStatsForFolder(folder);
                }
            }
            else
            {
                Console.Error.WriteLine($"Usage: {Process.GetCurrentProcess().ProcessName} <folder>");
                Console.Error.WriteLine("Error: you must provide the folder name for which you want to run the program as the first parameter.");
            }
        }

        private static void GetStatsForFolder(string folder)
        {
            var entries = Directory.GetFileSystemEntries(folder, "*.*", SearchOption.AllDirectories);
            var countByExtension = new Dictionary<string, int>();
            var numberOfFiles = 0;
            
            foreach (var entry in entries)
            {
                if (Directory.Exists(entry))
                {
                    // Folders are not interesting in this stats.
                    continue;
                }

                var extension = Path.GetExtension(entry).ToLower();

                if (countByExtension.ContainsKey(extension))
                {
                    countByExtension[extension]++;
                }
                else
                {
                    countByExtension[extension] = 1;
                }
                numberOfFiles++;
            }

            Console.WriteLine($"Stats for {folder}");
            foreach (var extension in countByExtension.OrderByDescending(kvp => kvp.Value))
            {
                Console.WriteLine($"    {extension.Key}: {extension.Value} file(s)");
            }
            Console.WriteLine($"Total number of files: {numberOfFiles}");
            Console.WriteLine();
        }
    }
}
