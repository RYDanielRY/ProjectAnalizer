using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProjectAnalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string mainPath = @"C:\Users\Daniil\source\repos\ProjectAnalizer";
            countObject("ProjectAnalizer",  mainPath);
        }
        static void countObject(string name, string path)
        {
            int countClass = (from cal in Assembly.GetExecutingAssembly().GetTypes()
                       where cal.Namespace == name && cal.IsClass
                       select cal).ToList().Count();
            var countInterfaces = (from cal in Assembly.GetExecutingAssembly().GetTypes()
                       where cal.Namespace == name && cal.IsInterface
                       select cal).ToList().Count();
            var countEnums = (from cal in Assembly.GetExecutingAssembly().GetTypes()
                       where cal.Namespace == name && cal.IsEnum
                       select cal).ToList().Count();
            Console.WriteLine($"Folder: {name}");
            Console.WriteLine($"Classes: {countClass}; interfaces: {countInterfaces}; enum: {countEnums}");
            Console.WriteLine();

            string[] allfolders = Directory.GetDirectories(path);

            foreach (var item in allfolders)
            {
                var newName = item.Replace(path+@"\", "");
                countObject(name+"."+newName, item);
            }
        }
    }
}
