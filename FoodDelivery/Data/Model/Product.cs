using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    [Serializable]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Grams { get; set; }
    }
}
