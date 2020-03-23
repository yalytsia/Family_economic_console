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

    }
}
