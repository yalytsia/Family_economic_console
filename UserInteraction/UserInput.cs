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
        private static int AddRecord(string inputLine, string filePath, List<Catalog> catalogs)
        {
            string line = ValidateName(catalogs, inputLine);
            int id = catalogs.Count == 0 ? 1 : catalogs.LastOrDefault().Id + 1;
            string allLines = (id == 1 ? String.Empty : Environment.NewLine) + id.ToString() + Constant.Delimiter + line;
            Data.SaveData(filePath, allLines);
            return id;
        }
        public static int InputCatalog(string input, CatalogType catalogType, string error)
        {
            Console.WriteLine($"\n Введите имя {input} или ID:");
            bool isInputFieldFinished = false;
            int catalogID = -1;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (inputLine.Length == 0)
                {
                    continue;
                }
                string filePath = Data.CreateFile(catalogType.ToString());
                List<Catalog> catalogs = Data.GetList(filePath);
                if (int.TryParse(inputLine, out catalogID))
                {
                    Catalog catalog = catalogs.FirstOrDefault(x => x.Id == catalogID);
                    if (catalog == null)
                    {
                        Console.WriteLine($" {error} с данным ID не существует.");
                    }
                    else
                    {
                        return catalogID;
                    }
                }
                else
                {
                    Catalog catalog = catalogs.FirstOrDefault(x => inputLine.Length > 0 && x.Name.ToLower().StartsWith(inputLine.ToLower()));
                    if (catalog == null)
                    {
                        Console.WriteLine($" {error} с данным именем не существует. Добавить?");
                        Menu menu = new Menu();
                        int toAdd = menu.AskAddRecord();
                        while (toAdd != 1 && toAdd != 2)
                        {
                            toAdd = menu.AskAddRecord();
                        }
                        if (toAdd == 1)
                        {
                            catalogID = AddRecord(inputLine, filePath, catalogs);
                            return catalogID;
                        }
                        else if (toAdd == 2)
                        {
                            InputCatalog(input, catalogType, error);
                            isInputFieldFinished = true;
                        }
                    }
                    else
                    {
                        return catalog.Id;
                    }
                }
            }
            return catalogID;
        }
        public static decimal InputNumber(string input, string error)
        {
            bool isInputFieldFinished;
            Console.WriteLine($" Введите {input}:");
            isInputFieldFinished = false;
            decimal inputNumber = -1;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (decimal.TryParse(inputLine, out inputNumber) && inputNumber > 0)
                {
                    isInputFieldFinished = true;
                    return inputNumber;
                }
                else
                {
                    Console.WriteLine($" Используйте только числа больше 0 и десятичные дроби для ввода {error}.");
                }
            }
            return inputNumber;
        }
        public static bool InputDate(Expenses expenses)
        {
            bool isInputFieldFinished;
            Console.WriteLine("\n Введите дату покупки (Enter - текущая дата):");
            isInputFieldFinished = false;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (inputLine == string.Empty)
                {
                    expenses.Date = DateTime.Today;
                    isInputFieldFinished = true;
                }
                else if (DateTime.TryParse(inputLine, out DateTime userInput))
                {
                    expenses.Date = userInput;
                    isInputFieldFinished = true;
                }
                else
                {
                    Console.WriteLine(" Введите дату цифрами через разделитель или пробел.");
                }
            }
            Console.Clear();
            return isInputFieldFinished;
        }
        public static string ProcessUserInput(int id, CatalogType catalogies)
        {
            string allLines = String.Empty;
            string checkLines = String.Empty;
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
                    string[] lines = checkLines.Split(Environment.NewLine);

                    if (lines.FirstOrDefault(x => x.ToLower() == inputLine.ToLower()) == null)
                    {
                        allLines = allLines + id.ToString() + Constant.Delimiter + inputLine + Environment.NewLine;
                        checkLines = checkLines + inputLine + Environment.NewLine;
                    }
                }
                counter++;
            } while (inputLine != String.Empty);
            allLines = allLines.TrimEnd(Environment.NewLine.ToCharArray());
            return allLines;
        }
        public static void AddRecord(int menuCatalog)
        {
            Console.WriteLine("\n Добавить:");
            CatalogType catalogies = (CatalogType)menuCatalog;
            string filePath = Data.CreateFile(catalogies.ToString());
            int id = Data.GetMaxId(filePath);
            string allLines = UserInput.ProcessUserInput(id, catalogies);
            Data.SaveData(filePath, allLines);
        }
        public static void DeleteRecord(int menuCatalog)
        {
            Console.WriteLine(" Введите ID удаляемой записи:");
            bool isInputFieldFinished = false;
            CatalogType catalogies = (CatalogType)menuCatalog;
            while (!isInputFieldFinished)
            {

                string inputLine = Console.ReadLine();
                if (inputLine.Length == 0)
                {
                    continue;
                }
                string filePath = Data.CreateFile(catalogies.ToString());
                List<Catalog> catalogs = Data.GetList(filePath);
                if (int.TryParse(inputLine, out int userInput))
                {
                    if (userInput == 0)
                    {
                        Console.Clear();
                        isInputFieldFinished = true;
                        break;
                    }
                    Catalog catalog = catalogs.FirstOrDefault(x => x.Id == userInput);
                    if (catalog == null)
                    {
                        Console.WriteLine(" Запись с данным ID не существует.");
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
            Console.WriteLine(" Введите ID удаляемой покупки:");
            bool isInputFieldFinished = false;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (inputLine.Length == 0)
                {
                    continue;
                }

                string filePath = Data.CreateFile("Expenses");
                List<Expenses> expenses = Data.GetExpenses();
                if (int.TryParse(inputLine, out int userInput))
                {
                    if (userInput == 0)
                    {
                        Console.Clear();
                        isInputFieldFinished = true;
                        break;
                    }
                    Expenses expense = expenses.FirstOrDefault(x => x.Id == userInput);
                    if (expense == null)
                    {
                        Console.WriteLine(" Запись с данным ID не существует.");
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
            Console.WriteLine("\n Введите ID редактируемой записи:");
            bool isInputFieldFinished = false;
            CatalogType catalogies = (CatalogType)menuCatalog;
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                if (inputLine.Trim().Length == 0)
                {
                    continue;
                }
                string filePath = Data.CreateFile(catalogies.ToString());
                List<Catalog> catalogs = Data.GetList(filePath);
                if (int.TryParse(inputLine, out int userInput))
                {
                    if (userInput == 0)
                    {
                        Console.Clear();
                        isInputFieldFinished = true;
                        break;
                    }
                    Catalog catalog = catalogs.FirstOrDefault(x => x.Id == userInput);
                    if (catalog == null)
                    {
                        //Console.Clear();
                        Console.WriteLine(" Запись с данным ID не существует.");
                    }
                    else
                    {
                        Console.WriteLine(" Запишите новое имя.");
                        string editedName = Console.ReadLine();
                        catalog.Name = ValidateName(catalogs, editedName);
                        string lines = Catalog.ListToCsv(catalogs);
                        Data.SaveAllData(filePath, lines);
                        isInputFieldFinished = true;
                        Console.Clear();
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
                Console.WriteLine($" Имя должно начинаться с буквы, не содержать {Constant.Delimiter} и быть уникальным.");
                inputLine = Console.ReadLine();
            }
            return inputLine;

        }

    }
}
