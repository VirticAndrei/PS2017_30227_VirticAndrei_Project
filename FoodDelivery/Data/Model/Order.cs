using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    [Serializable]
    public class Order
    {
        public int Id { get; set; }
        public int Client_id { get; set; }
        public double Total { get; set; }
        public string Order_date { get; set; }
        public string Order_status { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
