
using Entities;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Test_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            int menuItem = menu.AdminMenuI();
            if (menuItem == 2)
            {
                string filePath = @"test.csv";
                Console.Clear();
                Console.WriteLine("Добавить запись:");
                var lastLine = File.ReadLines("test.csv").Where(x=>x.Length>0).Last();
                var id = int.Parse(lastLine.Split(',')[0]);
                string inputLine = String.Empty;
                string csvLine;
                int counter = 0;
                do
                {
                    csvLine = Console.ReadLine();
                    if (csvLine != String.Empty)
                    {
                        id++;
                        if (counter == 0)
                        {
                            inputLine = Environment.NewLine;
                        }
                        inputLine = inputLine + id.ToString() + "," + csvLine + Environment.NewLine;
                    }
                    counter++;
                } while (csvLine != String.Empty);
                byte[] csvLineBytes = Encoding.Default.GetBytes(inputLine + Environment.NewLine);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);
                        ms.Write(bytes, 0, (int)file.Length);
                    }
                    ms.Write(csvLineBytes, 0, csvLineBytes.Length);

                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }
            }


        }
    }
}
