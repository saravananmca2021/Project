using System;
using System.Collections.Generic;

namespace BankingApi.DAL
{
    public partial class Transactions
    {
        public Transactions()
        {
            TransactionUniqueReference = $"{Guid.NewGuid().ToString().Replace("-", "").Substring(1, 17)}";
        }
        
        public int Id { get; set; }
        public string TransactionUniqueReference { get; set; }
        public decimal? TransactionAmount { get; set; }
        public string TranStatus { get; set; }
        public decimal? TransactionSourceAccount { get; set; }
        public decimal? TransactionDestinationAccount { get; set; }
        public decimal CustomerId { get; set; }
        public string TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
    }

   
}
