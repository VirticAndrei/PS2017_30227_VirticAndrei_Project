﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    [Serializable]
    public class Address
    {
        public int Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
    }
}
