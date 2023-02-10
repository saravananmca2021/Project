using BankingApi.DAL;
using BankingApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Services.Interfaces
{
    public interface ITransaction
    {
        Response CreateNewTransaction(Transactions transaction);
        Response FindTransactionByDate(DateTime date);
        Response MakeDeposit(string AccountNumber, decimal Amount, decimal CustomerID);
        Response MakeWithdrawal(string AccountNumber, decimal Amoun, decimal CustomerIDt);
        Response MakeFundsTransfer(string FromAccount, string ToAccount, decimal Amount, decimal CustomerID);
    }
}
