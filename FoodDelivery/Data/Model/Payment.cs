using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    [Serializable]
    public class Payment
    {
        public int Id { get; set; }
        public string Card_number { get; set; }
        public string Holder_name { get; set; }
        public string Exp_date { get; set; }
        public int Security_code { get; set; }
    }
}
