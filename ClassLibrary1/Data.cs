using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class Data
    {
        public static int GetMaxId(string filePath)
        {
            
            string lastLine = File.ReadLines(filePath).LastOrDefault(x => x.Length > 0);
            int id = lastLine == null ? 0 : int.Parse(lastLine.Split(Constant.Delimiter)[0]);
            return id;
        }
        public static string CreateFile(string fileName)
        {
            string filePath = fileName + ".csv";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            return filePath;
        }
        public static void SaveData(string filePath, string allLines)
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
        public static void SaveAllData(string filePath, string allLines)
        {
            byte[] csvLineBytes = Encoding.Default.GetBytes(allLines);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(csvLineBytes, 0, csvLineBytes.Length);

                using (FileStream file = new FileStream(filePath, FileMode.Truncate))
                {
                    ms.WriteTo(file);
                }
            }
        }
        public static void AddExpense(string fileName, Expenses expenses)
        {
            string filePath = CreateFile(fileName);
            int id = GetMaxId(filePath) + 1;
            string allFields = expenses.ToCsv(id);
            SaveData(filePath, allFields);
        }
        public static List<Catalog> GetList(string filePath)
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
        public static List<Expenses> GetExpenses()
        {
            List<Expenses> expensesList = new List<Expenses>();
            using (var reader = new StreamReader("Expenses.csv"))
            {

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(Constant.Delimiter);
                    Expenses expense = new Expenses();
                    expense.Id = int.Parse(values[0]);
                    expense.CategoryId = int.Parse(values[1]);
                    expense.GoodsId = int.Parse(values[2]);
                    expense.UnitId = int.Parse(values[3]);
                    expense.Price = decimal.Parse(values[4]);
                    expense.Quantity = float.Parse(values[5]);
                    expense.Date = DateTime.Parse(values[6]);
                    expensesList.Add(expense);
                }
            }
            return expensesList;
        }
    }
}
