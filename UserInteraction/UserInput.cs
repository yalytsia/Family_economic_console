using DataLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserInteraction
{
    public class UserInput
    {
        public static bool InputDate(Expenses expenses)
        {
            bool isInputFieldFinished;
            Console.WriteLine();
            Console.WriteLine("Введите дату покупки (Enter - текущая дата):");
            isInputFieldFinished = false;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                DateTime userInput;
                if (inputLine == string.Empty)
                {
                    expenses.Date = DateTime.Today;
                    isInputFieldFinished = true;
                }
                else if (DateTime.TryParse(inputLine, out userInput))
                {
                    expenses.Date = userInput;
                    isInputFieldFinished = true;
                }
                else
                {
                    Console.WriteLine("Введите дату в формате (как-то).");
                }
            }
            return isInputFieldFinished;
        }

        public static bool InputQuantity(Expenses expenses)
        {
            bool isInputFieldFinished;
            Console.WriteLine();
            Console.WriteLine("Введите количество:");
            isInputFieldFinished = false;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                float userInput = 0;
                if (float.TryParse(inputLine, out userInput) && userInput > 0)
                {
                    expenses.Quantity = userInput;
                    isInputFieldFinished = true;
                }
                else
                {
                    Console.WriteLine("Используйте только цифры больше 0 и десятичные дроби для ввода количества.");
                }
            }

            return isInputFieldFinished;
        }

        public static bool InputPrice(Expenses expenses)
        {
            bool isInputFieldFinished;
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

            return isInputFieldFinished;
        }

        public static bool InputUnit(Expenses expenses)
        {
            bool isInputFieldFinished;
            Console.WriteLine();
            Console.WriteLine("Введите единицы измерения товара:");
            isInputFieldFinished = false;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (inputLine.Length == 0)
                {
                    continue;
                }
                int userInput = 0;
                string filePath = CatalogType.Unit + ".csv";
                List<Catalog> catalogs = Data.GetList(filePath);
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
                    Catalog catalog = catalogs.FirstOrDefault(x => inputLine.Length > 0 && x.Name.ToLower().StartsWith(inputLine.ToLower()));
                    if (catalog == null)
                    {
                        Console.WriteLine("Eдиницa измерения с данным именем не существует. Добавить?");
                        Menu menu = new Menu();
                        int toAdd = menu.AskAddRecord();
                        if (toAdd == 1)
                        {
                            expenses.UnitId = AddRecord(inputLine, filePath, catalogs);
                            isInputFieldFinished = true;
                        }
                        else if (toAdd == 2)
                        {
                            InputUnit(expenses);

                            isInputFieldFinished = true;
                        }

                    }
                    else
                    {
                        expenses.UnitId = catalog.Id;
                        isInputFieldFinished = true;
                    }
                }
            }

            return isInputFieldFinished;
        }

        private static int AddRecord(string inputLine, string filePath, List<Catalog> catalogs)
        {
            string line = ValidateName(catalogs, inputLine);
            int id = catalogs.LastOrDefault().Id + 1;
            string allLines = Environment.NewLine + id.ToString() + Constant.Delimiter + line;
            Data.SaveData(filePath, allLines);
            return id;
        }

        public static bool InputCategory(Expenses expenses)
        {
            bool isInputFieldFinished = false;
            Console.WriteLine();
            Console.WriteLine("Введите категорию:");
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (inputLine.Length == 0)
                {
                    continue;
                }
                int userInput = 0;
                string filePath = CatalogType.GoodsCategory + ".csv";
                List<Catalog> catalogs = Data.GetList(filePath);
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
                    Catalog catalog = catalogs.FirstOrDefault(x => inputLine.Length > 0 && x.Name.ToLower().StartsWith(inputLine.ToLower()));
                    if (catalog == null)
                    {
                        Console.WriteLine("Категория с данным именем не существует. Добавить?");
                        Menu menu = new Menu();
                        int toAdd = menu.AskAddRecord();
                        if (toAdd == 1)
                        {
                            expenses.CategoryId = AddRecord(inputLine, filePath, catalogs);
                            isInputFieldFinished = true;
                        }
                        else if (toAdd == 2)
                        {
                            InputCategory(expenses);

                            isInputFieldFinished = true;
                        }
                    }
                    else
                    {
                        expenses.CategoryId = catalog.Id;
                        isInputFieldFinished = true;
                    }
                }
            }

            return isInputFieldFinished;
        }

        public static bool InputName(Expenses expenses)
        {
            Console.WriteLine();
            Console.WriteLine("Введите наименование товара:");
            bool isInputFieldFinished = false;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (inputLine.Length == 0)
                {
                    continue;
                }

                int userInput = 0;
                string filePath = CatalogType.Goods + ".csv";
                List<Catalog> catalogs = Data.GetList(filePath);
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
                    Catalog catalog = catalogs.FirstOrDefault(x => inputLine.Length > 0 && x.Name.ToLower().StartsWith(inputLine.ToLower()));
                    if (catalog == null)
                    {
                        Console.WriteLine("Товар с данным именем не существует. Добавить?");
                        Menu menu = new Menu();
                        int toAdd = menu.AskAddRecord();
                        if (toAdd == 1)
                        {
                            expenses.GoodsId = AddRecord(inputLine, filePath, catalogs);
                            isInputFieldFinished = true;
                        }
                        else if (toAdd == 2)
                        {
                            InputName(expenses);

                            isInputFieldFinished = true;
                        }
                    }
                    else
                    {
                        expenses.GoodsId = catalog.Id;
                        isInputFieldFinished = true;
                    }
                }
            }

            return isInputFieldFinished;
        }
        public static string ProcessUserInput(int id, CatalogType catalogies)
        {
            string allLines = String.Empty;
            string inputLine;
            int counter = 0;
            List<Catalog> catalogList = Data.GetList(catalogies + ".csv");
            do
            {

                inputLine = Console.ReadLine();
                if (inputLine != String.Empty && !int.TryParse(inputLine, out _))
                {
                   inputLine = ValidateName(catalogList, inputLine);
                   id++;
                    if (counter == 0 && id > 1)
                    {
                        allLines = Environment.NewLine;
                    }
                    string[] lines = allLines.Split(Environment.NewLine);
                    if (lines.FirstOrDefault(x => x.ToLower() == inputLine.ToLower()) == null)
                    {
                        allLines = allLines + id.ToString() + Constant.Delimiter + inputLine + Environment.NewLine;
                    }
                }
                counter++;
            } while (inputLine != String.Empty);
            allLines = allLines.TrimEnd(Environment.NewLine.ToCharArray());
            return allLines;
        }
        public static void AddRecord(int menuCatalog)
        {
            Console.WriteLine("Добавить:");
            CatalogType catalogies = (CatalogType)menuCatalog;
            string filePath = Data.CreateFile(catalogies.ToString());
            int id = Data.GetMaxId(filePath);
            string allLines = UserInput.ProcessUserInput(id, catalogies);
            Data.SaveData(filePath, allLines);
        }
        public static void DeleteRecord(int menuCatalog)
        {
            Console.WriteLine("Введите ID удаляемой записи:");
            bool isInputFieldFinished = false;
            CatalogType catalogies = (CatalogType)menuCatalog;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (inputLine.Length == 0)
                {
                    continue;
                }
                int userInput = 0;
                string filePath = catalogies + ".csv";
                List<Catalog> catalogs = Data.GetList(filePath);
                if (int.TryParse(inputLine, out userInput))
                {
                    Catalog catalog = catalogs.FirstOrDefault(x => x.Id == userInput);
                    if (catalog == null)
                    {
                        Console.WriteLine("Запись с данным ID не существует.");
                    }
                    else
                    {
                        Expenses expenses = null;
                        if (catalogies == CatalogType.GoodsCategory)
                        {
                            expenses = Data.GetExpenses().FirstOrDefault(x => x.CategoryId == userInput);
                        }
                        else if (catalogies == CatalogType.Goods)
                        {
                            expenses = Data.GetExpenses().FirstOrDefault(x => x.GoodsId == userInput);
                        }
                        else if (catalogies == CatalogType.Unit)
                        {
                            expenses = Data.GetExpenses().FirstOrDefault(x => x.UnitId == userInput);
                        }

                        if (expenses != null)
                        {
                            Console.WriteLine("Запись с данным ID используется в покупках. Не может быть удалена.");
                        }
                        else
                        {
                            catalogs.Remove(catalog);
                            string lines = Catalog.ListToCsv(catalogs);
                            Data.SaveAllData(filePath, lines);
                            isInputFieldFinished = true;
                        }


                    }
                }

            }
        }
        public static void DeletePurchase()
        {
            Console.WriteLine("Введите ID удаляемой покупки:");
            bool isInputFieldFinished = false;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (inputLine.Length == 0)
                {
                    continue;
                }
                int userInput = 0;
                string filePath = "Expenses.csv";
                List<Expenses> expenses = Data.GetExpenses();
                if (int.TryParse(inputLine, out userInput))
                {
                    Expenses expense = expenses.FirstOrDefault(x => x.Id == userInput);
                    if (expense == null)
                    {
                        Console.WriteLine("Запись с данным ID не существует.");
                    }
                    else
                    {
                        expenses.Remove(expense);
                        string lines = Expenses.ListToCsv(expenses);
                        Data.SaveAllData(filePath, lines);
                        isInputFieldFinished = true;
                    }
                }
            }
        }
        public static void EditRecord(int menuCatalog)
        {
            Console.WriteLine("Введите ID редактируемой записи:");
            bool isInputFieldFinished = false;
            CatalogType catalogies = (CatalogType)menuCatalog;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (inputLine.Trim().Length == 0)
                {
                    continue;
                }
                int userInput = 0;
                string filePath = catalogies + ".csv";
                List<Catalog> catalogs = Data.GetList(filePath);
                if (int.TryParse(inputLine, out userInput))
                {
                    Catalog catalog = catalogs.FirstOrDefault(x => x.Id == userInput);
                    if (catalog == null)
                    {
                        Console.WriteLine("Запись с данным ID не существует.");
                    }
                    else
                    {
                        string editedName = Console.ReadLine();
                        catalog.Name = ValidateName(catalogs, editedName);

                        string lines = Catalog.ListToCsv(catalogs);
                        Data.SaveAllData(filePath, lines);
                        isInputFieldFinished = true;
                    }
                }
            }
        }

        private static string ValidateName(List<Catalog> catalogs, string editedName)
        {
            string inputLine = editedName;
            while (inputLine.Trim().Length == 0 ||
                                        (!string.IsNullOrEmpty(inputLine) &&
                                        !char.IsDigit(inputLine.ToCharArray()[0]) &&
                                        !inputLine.Contains(Constant.Delimiter) &&
                                        catalogs.FirstOrDefault(x => x.Name.ToLower() == inputLine.ToLower()) != null))                                 
            {
                Console.WriteLine($"Имя должно начинаться с буквы, не содержать {Constant.Delimiter} и быть уникальным.");
                inputLine = Console.ReadLine();
            }       
            return inputLine;

        }
    }
}
