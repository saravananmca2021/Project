using System;
using System.Collections.Generic;

namespace BankingApi.DAL
{
    public partial class Account
    {
        public int Id { get; set; }
        public string AccountNumberGenerated { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public decimal? CurrentAccountBalance { get; set; }
        public bool? Block { get; set; }
        public decimal CustomerId { get; set; }

        public virtual User Customer { get; set; }
    }
}
