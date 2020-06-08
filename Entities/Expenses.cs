using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Expenses
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int GoodsId { get; set; }
        public int UnitId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
        public string ToCsv(int id)
        {
            return id.ToString() + Constant.Delimiter + CategoryId
                + Constant.Delimiter + GoodsId + Constant.Delimiter + UnitId
                + Constant.Delimiter + Price
                + Constant.Delimiter + Quantity
                + Constant.Delimiter + Date + Environment.NewLine;
        }
        public static string ListToCsv(List<Expenses> expenses)
        {
            string line = string.Empty;
            foreach (var item in expenses)
            {
                line = line + item.Id.ToString() + Constant.Delimiter + item.CategoryId
                + Constant.Delimiter + item.GoodsId + Constant.Delimiter + item.UnitId
                + Constant.Delimiter + item.Price
                + Constant.Delimiter + item.Quantity
                + Constant.Delimiter + item.Date + Environment.NewLine; ;
            }
            return line;
        }
    }
}
