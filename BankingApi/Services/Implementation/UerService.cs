using BankingApi.DAL;
using BankingApi.Model;
using BankingApi.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Services.Implementation
{
    public class UerService: IUserService
    {
        private readonly BankContext _dbcontext;

        public UerService(BankContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public string AccountNumberGeneration()
        {
            Random rand = new Random();
            string accountNumber = Convert.ToString((long)Math.Floor((rand.NextDouble() * 9_000_000_000L) + 1_000_000_000L));

            return accountNumber;
        }

        public Account GetByAccountNumber(string AccountNumber)
        {
            var account = _dbcontext.Account.Where(x => x.AccountNumberGenerated == AccountNumber).SingleOrDefault();
            if (account == null)
            {
                return null;
            }

            return account;
        }

        public AccountResponse RegestireNewUser(User Userrequest)
        {
            AccountResponse accResp = null;
            try
            {
                string accountNumber = "";
                accResp = new AccountResponse();
                 Account accModel = new Account();
                ICollection<Account> lAccount = new List<Account>();
                accModel = Userrequest.Account.OfType<Account>().FirstOrDefault();
                accountNumber = AccountNumberGeneration();
                accModel.AccountNumberGenerated = accountNumber;
                lAccount.Add(accModel);
                Userrequest.Account = JsonConvert.DeserializeObject<ICollection<Account>>(JsonConvert.SerializeObject(lAccount));
                _dbcontext.User.Add(Userrequest);
                _dbcontext.SaveChanges();

                accResp.AccountNumberGenerated = accountNumber;
                accResp.CustomerId = Userrequest.CustomerId;

                return accResp;
            }
            catch(Exception ex)
            {
                return null;
            }
           
        }

        public bool UpdateProfile(UserModel request)
        {
            try
            {
                User _updateUser = new User();
                _updateUser = JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(request));

                _dbcontext.User.Update(_updateUser);
                //.Property(x=>x.UserId).IsModified=false;

                _dbcontext.Entry<User>(_updateUser).Property(x => x.UserId).IsModified = false;
                _dbcontext.Entry<User>(_updateUser).Property(x => x.UserName).IsModified = false;
                _dbcontext.Entry<User>(_updateUser).Property(x => x.Password).IsModified = false;
                _dbcontext.Entry<User>(_updateUser).Property(x => x.OldPassword).IsModified = false;
                var result = _dbcontext.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}
