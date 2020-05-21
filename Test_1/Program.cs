
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
        static int pageSize = 7;
        static void Main(string[] args)
        {
          
            while (true)
            {
                Menu menu = new Menu();
                 
                int menuItem = menu.AdminMenuI();


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
                        UserInput.InputCategory(expenses);
                        UserInput.InputName(expenses);
                        UserInput.InputUnit(expenses);
                        UserInput.InputPrice(expenses);
                        UserInput.InputQuantity(expenses);
                        UserInput.InputDate(expenses);
                        isInputFinished = true;
                    }
                    Data.AddExpense("Expenses", expenses);
                }
                else if (menuItem == 2)
                {
                    int menuCatalog = menu.СatalogsMenu();
                    if (menuCatalog >= 1 && menuCatalog <= 3)
                    {
                        UserInput.AddRecord(menuCatalog);
                    }
                }
                else if (menuItem == 3)
                {
                    int deleteMenu = menu.DeleteMenu();
                    if (deleteMenu >= 1 && deleteMenu <= 3)
                    {
                        UserInput.DeleteRecord(deleteMenu);
                    }
                    else if (deleteMenu == 4)
                    { 
                        
                    }
                }
                else if (menuItem == 4)
                {
                    int viewRecordMenu = menu.ViewRecordMenu();
                    if (viewRecordMenu >= 1 && viewRecordMenu <= 4)
                    {
                        CatalogType catalogies = (CatalogType)viewRecordMenu;
                        ConsoleKeyInfo userChoose = Console.ReadKey();

                        if (viewRecordMenu == 4)
                        {
                            UserOutput.TableExpenses(((pageCounter - 1) * pageSize), pageCounter * pageSize);

                            int maxId = Data.GetMaxId("Expenses.csv");

                            while (userChoose.Key != ConsoleKey.Escape)
                            {

                                UserOutput.TableExpenses(((pageCounter - 1) * pageSize), pageCounter * pageSize);

                                if (userChoose.Key == ConsoleKey.PageDown ||
                                    userChoose.Key == ConsoleKey.DownArrow ||
                                    userChoose.Key == ConsoleKey.RightArrow)
                                {
                                    if (pageCounter * pageSize <= maxId)
                                    {
                                        pageCounter = pageCounter + 1;
                                    }
                                }
                                if (userChoose.Key == ConsoleKey.PageUp ||
                                    userChoose.Key == ConsoleKey.UpArrow ||
                                    userChoose.Key == ConsoleKey.LeftArrow)
                                {

                                    if (pageCounter > 1)
                                    {
                                        pageCounter = pageCounter - 1;
                                    }
                                }
                                userChoose = Console.ReadKey();
                            }
                        }

                        else
                        {

                            CatalogType catalog = (CatalogType)viewRecordMenu;
                            UserOutput.TableCatalogs(catalog, ((pageCounter - 1) * pageSize), pageCounter * pageSize);
                            int maxId = Data.GetMaxId(catalog + ".csv");
                            while (userChoose.Key != ConsoleKey.Escape)
                            {
                                UserOutput.TableCatalogs(catalog, ((pageCounter - 1) * pageSize), pageCounter * pageSize);


                                if (userChoose.Key == ConsoleKey.PageDown ||
                                    userChoose.Key == ConsoleKey.DownArrow ||
                                    userChoose.Key == ConsoleKey.RightArrow)
                                {
                                    if (pageCounter * pageSize <= maxId)
                                    {
                                        pageCounter = pageCounter + 1;
                                    }
                                }
                                if (userChoose.Key == ConsoleKey.PageUp ||
                                    userChoose.Key == ConsoleKey.UpArrow ||
                                    userChoose.Key == ConsoleKey.LeftArrow)
                                {

                                    if (pageCounter > 1)
                                    {
                                        pageCounter = pageCounter - 1;
                                    }
                                }
                                userChoose = Console.ReadKey();
                            }

                        }
                        Console.ReadKey();
                    }

                }
            }
            
        }

        











    }

}
