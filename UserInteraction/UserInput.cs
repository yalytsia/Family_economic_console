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
                int userInput = 0;
                List<Catalog> catalogs = Data.GetList(CatalogType.Unit + ".csv");
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
                    Catalog catalog = catalogs.FirstOrDefault(x => inputLine.Length > 0 && x.Name.StartsWith(inputLine));
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

            return isInputFieldFinished;
        }

        public static bool InputCategory(Expenses expenses)
        {
            bool isInputFieldFinished = false;
            Console.WriteLine();
            Console.WriteLine("Введите категорию:");
            while (!isInputFieldFinished)
            {
                string inputLine = Console.ReadLine();
                int userInput = 0;
                List<Catalog> catalogs = Data.GetList(CatalogType.GoodsCategory + ".csv");
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
                    Catalog catalog = catalogs.FirstOrDefault(x => inputLine.Length > 0 && x.Name.StartsWith(inputLine));
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
                int userInput = 0;
                List<Catalog> catalogs = Data.GetList(CatalogType.Goods + ".csv");
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
                    Catalog catalog = catalogs.FirstOrDefault(x => inputLine.Length > 0 && x.Name.StartsWith(inputLine));
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

            return isInputFieldFinished;
        }
        public static string ProcessUserInput(int id)
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
    }
}
