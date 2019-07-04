using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NugetUnbox
{
    class Program
    {
        static void Main(string[] args)
        {
            string startFolder = @"C:\Users\Артём\Downloads\stamdart";

            string[] packages = Directory.GetDirectories(startFolder);

            List<string> libs = new List<string>();
            foreach (string package in packages)
            {
                libs.AddRange(Directory.GetDirectories(package));
            }
            libs = libs.Where(str => str.EndsWith("lib")).ToList();

            List<string> dllPaths = new List<string>(libs.Count);

            foreach (string lib in libs)
            {
                string standartPath = Path.Combine(lib, "netstandard2.0");
                dllPaths.AddRange(Directory.GetFiles(standartPath));
            }
            dllPaths = dllPaths.Where(dll => dll.EndsWith("dll")).ToList();

            foreach (string dllPath in dllPaths)
            {
                File.Copy(dllPath, Path.Combine(dllPath, startFolder, Path.GetFileName(dllPath)));
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
