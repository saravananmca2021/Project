using System;
using System.Collections.Generic;

namespace BankingApi.DAL
{
    public partial class User
    {
        public User()
        {
            Account = new HashSet<Account>();
            Address = new HashSet<Address>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal? Mobile { get; set; }
        public decimal CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }

        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
