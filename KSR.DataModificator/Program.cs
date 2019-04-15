using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.DataModificator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose a directory to modify: ");
            string directoryPath = Console.ReadLine();
            var getAllFiles = Directory.EnumerateFiles(directoryPath ?? throw new InvalidOperationException("No such file or a category."), "*.sgm", SearchOption.TopDirectoryOnly);
            foreach (var file in getAllFiles)
            {
                var text = File.ReadAllLines(file);
                using (var writer = new StreamWriter(new FileStream(file, FileMode.Open)))
                {
                    writer.WriteLine(text[0]);
                    writer.WriteLine("<ROOT>");
                    for (int i = 1; i < text.Length; i++)
                    {
                        writer.WriteLine(text[i]);
                    }
                    writer.WriteLine("</ROOT>");
                }
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
