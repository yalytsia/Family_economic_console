
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
            while (true)
            {
                Menu menu = new Menu();
                int menuItem = menu.AdminMenuI();
                if (menuItem == 2)
                {
                    int menuCatalog = menu.СatalogsMenu();
                    if (menuCatalog >= 1 && menuCatalog <= 3)
                    {
                       Catalogies catalogies = (Catalogies) menuCatalog;
                       AddRecord(catalogies.ToString());
                    }
                }
                else if (menuItem == 0)
                {
                    Environment.Exit(0);
                }
            }
        }

        private static void AddRecord(string filePath)
        {
            
            Console.WriteLine("Добавить:");
            CreateFile(filePath);
            int id = GetId(filePath);
            string allLines = ProcessUserInput(id);
            SaveData(filePath, allLines);
        }

        private static void SaveData(string filePath, string allLines)
        {
            byte[] csvLineBytes = Encoding.Default.GetBytes(allLines);
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

        private static string ProcessUserInput(int id)
        {
            string allLines = String.Empty;
            string inputLine;
            int counter = 0;
            do
            {
                inputLine = Console.ReadLine();
                if (inputLine != String.Empty)
                {
                    id++;
                    if (counter == 0 && id > 1)
                    {
                        allLines = Environment.NewLine;
                    }
                    allLines = allLines + id.ToString() + "," + inputLine + Environment.NewLine;
                }
                counter++;
            } while (inputLine != String.Empty);
            allLines = allLines.TrimEnd(Environment.NewLine.ToCharArray());
            return allLines;
        }

        private static int GetId(string filePath)
        {
            string lastLine = File.ReadLines(filePath).LastOrDefault(x => x.Length > 0);
            int id = lastLine == null ? 0 : int.Parse(lastLine.Split(',')[0]);
            return id;
        }

        private static void CreateFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }
    }
}
