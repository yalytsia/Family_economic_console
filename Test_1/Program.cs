
using Entities;
using System;
using System.Collections.Generic;
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
                        CatalogType catalogies = (CatalogType)menuCatalog;
                        AddRecord(catalogies.ToString());
                    }
                }
                else if (menuItem == 0)
                {
                    Environment.Exit(0);
                }
                else if (menuItem == 1)
                {
                    Expenses expenses = new Expenses();
                    bool isInputFinished = false;
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Введите категорию:");
                    while (!isInputFinished)
                    {
                        bool isInputFieldFinished = false;
                        while (!isInputFieldFinished)
                        {
                            string inputLine = Console.ReadLine();
                            int userInput = 0;
                            List<Catalog> catalogs = GetList(CatalogType.GoodsCategory + ".csv");
                            if (int.TryParse(inputLine, out userInput))
                            {
                                Catalog catalog = catalogs.FirstOrDefault(x => x.Id == userInput);
                                if (catalog == null)
                                {
                                    Console.WriteLine("Категория с данным ID не существует.");
                                }
                                else
                                {
                                    expenses.CategoryId = userInput;
                                    isInputFieldFinished = true;
                                }
                            }
                            else
                            {
                                Catalog catalog = catalogs.FirstOrDefault(x => x.Name.StartsWith (inputLine));
                                if (catalog == null)
                                {
                                    Console.WriteLine("Категория с данным именем не существует.");
                                }
                                else
                                {
                                    expenses.CategoryId = catalog.Id;
                                    isInputFieldFinished = true;
                                }
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Введите наименование товара:");
                        isInputFieldFinished = false;
                        while (!isInputFieldFinished)
                        {
                            string inputLine = Console.ReadLine();
                            int userInput = 0;
                            List<Catalog> catalogs = GetList(CatalogType.Goods + ".csv");
                            if (int.TryParse(inputLine, out userInput))
                            {
                                Catalog catalog = catalogs.FirstOrDefault(x => x.Id == userInput);
                                if (catalog == null)
                                {
                                    Console.WriteLine("Товар с данным ID не существует.");
                                }
                                else
                                {
                                    expenses.GoodsId = userInput;
                                    isInputFieldFinished = true;
                                }
                            }
                            else
                            {
                                Catalog catalog = catalogs.FirstOrDefault(x => x.Name.StartsWith(inputLine));
                                if (catalog == null)
                                {
                                    Console.WriteLine("Товар с данным именем не существует.");
                                }
                                else
                                {
                                    expenses.GoodsId = catalog.Id;
                                    isInputFieldFinished = true;
                                }
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Введите единицы измерения товара:");
                        isInputFieldFinished = false;
                        while (!isInputFieldFinished)
                        {
                            string inputLine = Console.ReadLine();
                            int userInput = 0;
                            List<Catalog> catalogs = GetList(CatalogType.Unit + ".csv");
                            if (int.TryParse(inputLine, out userInput))
                            {
                                Catalog catalog = catalogs.FirstOrDefault(x => x.Id == userInput);
                                if (catalog == null)
                                {
                                    Console.WriteLine("Eдиницa измерения с данным ID не существует.");
                                }
                                else
                                {
                                    expenses.UnitId = userInput;
                                    isInputFieldFinished = true;
                                }
                            }
                            else
                            {
                                Catalog catalog = catalogs.FirstOrDefault(x => x.Name.StartsWith(inputLine));
                                if (catalog == null)
                                {
                                    Console.WriteLine("Eдиницa измерения с данным именем не существует.");
                                }
                                else
                                {
                                    expenses.UnitId = catalog.Id;
                                    isInputFieldFinished = true;
                                }
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Введите цену товара:");
                        isInputFieldFinished = false;
                        while (!isInputFieldFinished)
                        {
                            string inputLine = Console.ReadLine();
                            decimal userInput = 0;
                            if (decimal.TryParse(inputLine, out userInput))
                            {                                
                                    expenses.Price = userInput;
                                    isInputFieldFinished = true;                                
                            }
                            else
                            {
                                Console.WriteLine("Используйте только цифры для ввода цены.");
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Введите количество:");
                        isInputFieldFinished = false;
                        while (!isInputFieldFinished)
                        {
                            string inputLine = Console.ReadLine();
                            int userInput = 0;
                            if (int.TryParse(inputLine, out userInput))
                            {
                                expenses.Quantity = userInput;
                                isInputFieldFinished = true;
                            }
                            else
                            {
                                Console.WriteLine("Используйте только цифры для ввода количества.");
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Введите дату покупки:");
                        isInputFieldFinished = false;
                        while (!isInputFieldFinished)
                        {
                            string inputLine = Console.ReadLine();
                            DateTime userInput;
                            if (DateTime.TryParse(inputLine, out userInput))
                            {
                                expenses.Date = userInput;
                                isInputFieldFinished = true;
                            }
                            else
                            {
                                Console.WriteLine("Введите дату в формате (как-то).");
                            }
                        }
                        isInputFinished = true;
                    }
                }
            }
        }

        private static List<Catalog> GetList(string filePath)
        {
            List<Catalog> catalogs = new List<Catalog>();
            using (var reader = new StreamReader(filePath))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(Constant.Delimiter);
                    Catalog catalog = new Catalog();
                    catalog.Id = int.Parse(values[0]);
                    catalog.Name = values[1];
                    catalogs.Add(catalog);
                }
            }
            return catalogs;
        }

        private static void AddRecord(string fileName)
        {

            Console.WriteLine("Добавить:");
            string filePath = CreateFile(fileName);
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
                    allLines = allLines + id.ToString() + Constant.Delimiter + inputLine + Environment.NewLine;
                }
                counter++;
            } while (inputLine != String.Empty);
            allLines = allLines.TrimEnd(Environment.NewLine.ToCharArray());
            return allLines;
        }

        private static int GetId(string filePath)
        {
            string lastLine = File.ReadLines(filePath).LastOrDefault(x => x.Length > 0);
            int id = lastLine == null ? 0 : int.Parse(lastLine.Split(Constant.Delimiter)[0]);
            return id;
        }

        private static string CreateFile(string fileName)
        {
            string filePath = fileName + ".csv";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            return filePath;
        }
    }
}
