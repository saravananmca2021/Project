using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Model
{
    public class AccountTransfer
    {
        public string ToAccount { get; set; }

        public decimal? Amount { get; set; }

        public string FromAccount { get; set; }

        public decimal CustomerId { get; set; }

    }
}
