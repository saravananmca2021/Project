using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal? Mobile { get; set; }
        public decimal CustomerId { get; set; }

       // public virtual ICollection<AddressModel> Address { get; set; }
    }
}
