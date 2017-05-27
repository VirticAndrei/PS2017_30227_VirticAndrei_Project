using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    [Serializable]
    public class Cart
    {
        public int Id { get; set; }
        public int Client_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
}
