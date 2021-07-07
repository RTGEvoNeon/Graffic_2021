using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice.Models
{
    public class Total
    {
        public DateTime date { get; set; }
        public double Sberbank { get; set; }
        public double Tinkoff { get; set; }
        public double CentralBank { get; set; }
        public double Cash { get; set; }
        public double allMoney { get; set; }
        public Total()
        {
            Cash = 0;
            Sberbank = 0;
            Tinkoff = 0;
            allMoney = 0;
            CentralBank = 0;
        }
        public Total(Total a)
        {
            date = a.date.AddDays(1);
            allMoney = a.allMoney;
            Cash = a.Cash;
            CentralBank = a.CentralBank;
            Tinkoff = a.Tinkoff;
            Sberbank = a.Sberbank;
        }
    }
}
