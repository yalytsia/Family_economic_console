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
        public float Quantity { get; set; }
        public DateTime Date { get; set; }
        public string ToCsv(int id)
        {
            return id.ToString() + Constant.Delimiter + CategoryId
                + Constant.Delimiter + GoodsId + Constant.Delimiter + UnitId
                + Constant.Delimiter + Price
                + Constant.Delimiter + Quantity
                + Constant.Delimiter + Date + Environment.NewLine;
        }
    }
}
