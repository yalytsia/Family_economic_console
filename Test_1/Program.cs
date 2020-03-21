
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
                int menuCatalog = menu.СatalogMenu();
                AddRecord(menuCatalog.ToString());
            }
        }

        private static void AddRecord(string filePath)
        {
            Console.Clear();
            Console.WriteLine("Добавить запись:");
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            string lastLine = File.ReadLines(filePath).Where(x => x.Length > 0).LastOrDefault();
            int id = lastLine == null ? 0 : int.Parse(lastLine.Split(',')[0]);
            string inputLine = String.Empty;
            string csvLine;
            int counter = 0;
            do
            {
                csvLine = Console.ReadLine();
                if (csvLine != String.Empty)
                {
                    id++;
                    if (counter == 0 && id > 1)
                    {
                        inputLine = Environment.NewLine;
                    }
                    inputLine = inputLine + id.ToString() + "," + csvLine + Environment.NewLine;
                }
                counter++;
            } while (csvLine != String.Empty);
            inputLine = inputLine.TrimEnd(Environment.NewLine.ToCharArray());
            byte[] csvLineBytes = Encoding.Default.GetBytes(inputLine);
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
