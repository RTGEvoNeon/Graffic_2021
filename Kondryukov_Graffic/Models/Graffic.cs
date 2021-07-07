using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Practice.Models
{
    public class Graffic
    {
        public class Rootobject
        {
            public Datum[] data { get; set; }
            public string isSuccess { get; set; }
            public string errorMessage { get; set; }
            public string errorCode { get; set; }
        }

        public class Datum
        {
            public string accountId { get; set; }
            public Detail[] details { get; set; }
        }

        public class Detail
        {
            public DateTime date { get; set; }
            public double factValue { get; set; }
            public double planValue { get; set; }

            //public string notComittedCount { get; set; }
            //public string factValueInUserCurrency { get; set; }
            //public string planValueInUserCurrency { get; set; }
            //public string accountCurrencyUserCurrencyRate { get; set; }
        }
    }
}
