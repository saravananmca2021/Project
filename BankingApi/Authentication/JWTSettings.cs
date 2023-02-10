using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Authentication
{
    public class JWTSettings
    {
        public string Seceretkey { get; set; }
        public string BankSettlementaccount { get; set; }
    }
}
