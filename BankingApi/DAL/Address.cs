using System;
using System.Collections.Generic;

namespace BankingApi.DAL
{
    public partial class Address
    {
        public int Id { get; set; }
        public decimal CustomerId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public decimal? Pincode { get; set; }

        public virtual User Customer { get; set; }
    }
}
