
using DataLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UserInteraction;

namespace Test_1
{
    class Program
    {
        static int pageCounter = 1;
        static readonly int pageSize = 7;
        static void Main(string[] args)
        {
            while (true)
            {
                Menu globalMenu = new Menu();
                int menuItem = globalMenu.AdminMenuI();
                if (menuItem == 0)
                {
                    Environment.Exit(0);
                }
                else if (menuItem == 1)
                {
                    Expenses expenses = new Expenses();
                    bool isInputFinished = false;
                    Console.Clear();

                    while (!isInputFinished)
                    {
                        expenses.CategoryId = UserInput.InputCatalog("категории", CatalogType.GoodsCategory, "Категория");
                        expenses.GoodsId = UserInput.InputCatalog("товара", CatalogType.Goods, "Товар");
                        expenses.UnitId = UserInput.InputCatalog("единицы измерения", CatalogType.Unit, "Единица измерения");
                        expenses.Price = UserInput.InputNumber("цену товара", "цены");
                        expenses.Quantity = UserInput.InputNumber("количество", "количества");
                        UserInput.InputDate(expenses);
                        isInputFinished = true;
                    }
                    Data.AddExpense("Expenses", expenses);
                }
                else if (menuItem == 2)
                {
                    bool isInputFinished = false;
                    Console.Clear();
                    while (!isInputFinished)
                    {
                        int menuCatalog = new Menu().СatalogsMenu();
                        if (menuCatalog >= 1 && menuCatalog <= 3)
                        {
                            UserInput.AddRecord(menuCatalog);
                            isInputFinished = true;
                            Console.Clear();
                        }
                        else if (menuCatalog == 0)
                        {
                            isInputFinished = true;
                            Console.Clear();
                        }
                    }
                }
                else if (menuItem == 3)
                {
                    bool isInputFinished = false;
                    Console.Clear();
                    while (!isInputFinished)
                    {
                        int deleteMenu = new Menu().DeleteMenu();
                        if (deleteMenu >= 1 && deleteMenu <= 3)
                        {
                            UserInput.DeleteRecord(deleteMenu);
                            isInputFinished = true;
                        }
                        else if (deleteMenu == 4)
                        {
                            UserInput.DeletePurchase();
                            isInputFinished = true;
                        }
                        else if (deleteMenu == 0)
                        {
                            isInputFinished = true;
                            Console.Clear();
                        }
                    }
                }
                else if (menuItem == 4)
                {
                    bool isInputFinished = false;
                    Console.Clear();
                    while (!isInputFinished)
                    {
                        int viewRecordMenu = new Menu().ViewRecordMenu();
                        if (viewRecordMenu >= 1 && viewRecordMenu <= 4)
                        {
                            ConsoleKeyInfo userChoose = Console.ReadKey();
                            if (viewRecordMenu == 4)
                            {
                                UserOutput.TableExpenses(((pageCounter - 1) * pageSize), pageCounter * pageSize);
                                int maxId = Data.GetMaxId("Expenses.csv");
                                while (userChoose.Key != ConsoleKey.D0)
                                {
                                    UserOutput.TableExpenses(((pageCounter - 1) * pageSize), pageCounter * pageSize);
                                    if (userChoose.Key == ConsoleKey.PageDown ||
                                        userChoose.Key == ConsoleKey.DownArrow ||
                                        userChoose.Key == ConsoleKey.RightArrow)
                                    {
                                        if (pageCounter * pageSize <= maxId)
                                        {
                                            pageCounter += 1;
                                        }
                                    }
                                    if (userChoose.Key == ConsoleKey.PageUp ||
                                        userChoose.Key == ConsoleKey.UpArrow ||
                                        userChoose.Key == ConsoleKey.LeftArrow)
                                    {
                                        if (pageCounter > 1)
                                        {
                                            pageCounter -= 1;
                                        }
                                    }
                                    userChoose = Console.ReadKey();
                                }
                                pageCounter = 1;
                            }
                            else
                            {
                                CatalogType catalog = (CatalogType)viewRecordMenu;
                                UserOutput.TableCatalogs(catalog, ((pageCounter - 1) * pageSize), pageCounter * pageSize);
                                int maxId = Data.GetMaxId(catalog + ".csv");
                                while (userChoose.Key != ConsoleKey.D0)
                                {
                                    UserOutput.TableCatalogs(catalog, ((pageCounter - 1) * pageSize), pageCounter * pageSize);
                                    if (userChoose.Key == ConsoleKey.PageDown ||
                                        userChoose.Key == ConsoleKey.DownArrow ||
                                        userChoose.Key == ConsoleKey.RightArrow)
                                    {
                                        if (pageCounter * pageSize <= maxId)
                                        {
                                            pageCounter += 1;
                                        }
                                    }
                                    if (userChoose.Key == ConsoleKey.PageUp ||
                                        userChoose.Key == ConsoleKey.UpArrow ||
                                        userChoose.Key == ConsoleKey.LeftArrow)
                                    {

                                        if (pageCounter > 1)
                                        {
                                            pageCounter -= 1;
                                        }
                                    }
                                    userChoose = Console.ReadKey();
                                }
                            }
                            pageCounter = 1;
                            Console.ReadKey();
                        }
                        else if (viewRecordMenu == 0)
                        {
                            isInputFinished = true;
                            Console.Clear();
                        }
                    }
                }
                else if (menuItem == 5)
                {
                    bool isInputFinished = false;
                    Console.Clear();
                    while (!isInputFinished)
                    {
                        int editMenu = new Menu().EditMenu();
                        if (editMenu >= 1 && editMenu <= 3)
                        {
                            UserInput.EditRecord(editMenu);
                            isInputFinished = true;
                        }
                        else if (editMenu == 0)
                        {
                            isInputFinished = true;
                            Console.Clear();
                        }
                    }
                }
            }
        }
    }
}