using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApi.DAL;
using BankingApi.Model;

namespace BankingApi.Services.Interfaces
{
    public interface IUserService
    {

        AccountResponse RegestireNewUser(User Userrequest);

        String AccountNumberGeneration();

        bool UpdateProfile(UserModel Updateprofile);

        Account GetByAccountNumber(string AccountNumber);



    }
}
