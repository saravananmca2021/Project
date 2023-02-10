using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Model
{
    public class Withdrawal
    {
        public string AccounNumber { get; set; }
        public decimal? Amount { get; set; }

        public decimal CustomerId { get; set; }
    }

}
