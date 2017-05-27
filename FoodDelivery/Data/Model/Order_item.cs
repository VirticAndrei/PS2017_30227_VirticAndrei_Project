using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    [Serializable]
    public class Order_item
    {
        public int Id { get; set; }
        public int Product_id { get; set; }
        public int Order_id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
