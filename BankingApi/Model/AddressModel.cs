using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Model
{
    public class AddressModel
    {
        public int Id { get; set; }
        public decimal CustomerId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public decimal? Mobile { get; set; }

       
    }
}
