using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NugetUnbox
{
    class Program
    {
        public static readonly string inputKey = "-input";
        public static readonly string outputKey = "-output";
        public static readonly string regexKey = "-regex";

        static void Main(string[] args)
        {
            if (args.Length % 2 != 0) { Console.WriteLine("Args values is missing"); return; }

            Dictionary<string, string> config = ReadKeys(args);

            IEnumerable<string> filePaths = MatchFiles(config[inputKey], config[regexKey]);

            ExportFiles(filePaths, config[outputKey]);

            Console.WriteLine("Hello World!, I'm done");
            Console.ReadLine();
        }
        private static Dictionary<string, string> ReadKeys(string[] args)
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i += 2)
            {
                config.Add(args[i], args[i + 1]);
            }
            return config;
        }

        private static IEnumerable<string> MatchFiles(string workingFolder, string pattern)
        {
            IEnumerable<string> filePaths = Directory.GetFiles(workingFolder, "*.*", SearchOption.AllDirectories);
            return filePaths.Where(p => Regex.IsMatch(p, pattern));
        }
        private static void ExportFiles(IEnumerable<string> files, string outputPath)
        {
            Directory.CreateDirectory(outputPath);

            foreach (string file in files)
            {
                File.Copy(file, Path.Combine(outputPath, Path.GetFileName(file)));
            }
        }
    }

}
