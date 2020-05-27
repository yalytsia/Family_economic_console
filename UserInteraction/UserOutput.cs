using DataLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UserInteraction
{
    public class UserOutput
    {
        public static void TableExpenses(int from, int to)
        {
            List<Expenses> expensesList = Data.GetExpenses().Where(x => x.Id > from && x.Id <= to).ToList();
            List<Catalog> categories = Data.GetList(CatalogType.GoodsCategory + ".csv");
            List<Catalog> goods = Data.GetList(CatalogType.Goods + ".csv");
            List<Catalog> units = Data.GetList(CatalogType.Unit + ".csv");



            Console.Clear();
            Console.WriteLine(" .____________________________________________________________________________________________________________________.");
            Console.WriteLine(" |                                                                                                                    |");
            Console.WriteLine(" |                                                 ПОКУПКИ                                                            |");
            Console.WriteLine(" |____________________________________________________________________________________________________________________|");
            Console.WriteLine(" |     |                     |                               |                    |            |   Кол-  |            |");
            Console.WriteLine(" | ID  |      Категория      |         Наименование          |     Ед. измер.     |    Цена    |   ство  |    Дата    |");
            Console.WriteLine(" |_____|_____________________|_______________________________|____________________|____________|_________|____________|");
            for (int i = 0; i < expensesList.Count; i++)
            {
                Console.WriteLine(" |     |                     |                               |                    |            |         |            |");
                Console.WriteLine(" | {0}| {1}| {2}| {3}| {4}| {5}| {6} |",
                    expensesList[i].Id + new string(' ', Constant.IdColumnLength - expensesList[i].Id.ToString().Length),
                    FormatName(categories.FirstOrDefault(x => x.Id == expensesList[i].CategoryId).Name, Constant.CategoryColumnLength),
                    FormatName(goods.FirstOrDefault(x => x.Id == expensesList[i].GoodsId).Name, Constant.NameColumnLength),
                    FormatName(units.FirstOrDefault(x => x.Id == expensesList[i].UnitId).Name, Constant.UnitColumnLength),
                    expensesList[i].Price + new string(' ', Constant.PriceColumnLength - expensesList[i].Price.ToString().Length),
                    expensesList[i].Quantity + new string(' ', Constant.QuantityColumnLength - expensesList[i].Quantity.ToString().Length),
                    expensesList[i].Date.ToShortDateString());
                Console.WriteLine(" |_____|_____________________|_______________________________|____________________|____________|_________|____________|");
            }
        }

        private static string FormatName(string name, int columnLength)
        {
            return columnLength - name.Length >= 0 ?
                                 name + new string(' ', columnLength - name.Length)
                                 : name.Substring(0, columnLength);
        }

        public static void TableCatalogs(CatalogType catalogType, int from, int to)
        {
            List<Catalog> catalog = Data.GetList(catalogType + ".csv").Where(x => x.Id > from && x.Id <= to).ToList();

            Console.Clear();
            Console.WriteLine(" .____________________________________________________________________________________________________________________.");
            Console.WriteLine(" |                                                                                                                    |");
            Console.WriteLine(" |                  {0}  |", catalogType + new string(' ', 96 - catalogType.ToString().Length));
            Console.WriteLine(" |____________________________________________________________________________________________________________________|");
            Console.WriteLine(" |       |                                                                                                            |");
            Console.WriteLine(" | ID    |     Наименование                                                                                           |");
            Console.WriteLine(" |_______|____________________________________________________________________________________________________________|");
            for (int i = 0; i < catalog.Count; i++)
            {
                Console.WriteLine(" |       |                                                                                                            |");
                Console.WriteLine(" | {0} |   {1} |",
                    catalog[i].Id + new string(' ', 5 - catalog[i].Id.ToString().Length),
                    catalog[i].Name + new string(' ', 104 - catalog[i].Name.Length));


                Console.WriteLine(" |_______|____________________________________________________________________________________________________________|");
            }
        }
    }
}
