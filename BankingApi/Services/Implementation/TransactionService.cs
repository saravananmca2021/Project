using BankingApi.Authentication;
using BankingApi.DAL;
using BankingApi.Model;
using BankingApi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApi.Services.Implementation
{
    public class TransactionService : ITransaction
    {
        private readonly BankContext _dbContext;
        private IUserService _userService;
        private static string _bankSettlementAccount;
        private JWTSettings _settings;
        private ILogger<TransactionService> _logger;

        public TransactionService(BankContext DBcontext,IUserService userService, 
            IOptions<JWTSettings> settings,
            ILogger<TransactionService> logger)
        {
            _dbContext = DBcontext;
            _userService = userService;
            _logger = logger;
            _settings = settings.Value;
            _bankSettlementAccount = _settings.BankSettlementaccount;
        }
        public Response CreateNewTransaction(Transactions transaction)
        {
            throw new NotImplementedException();
        }

        public Response FindTransactionByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Response MakeDeposit(string AccountNumber, decimal Amount, decimal CustomerID)
        {
            _logger.LogInformation($"Deposit Intiated:{AccountNumber}");
            Response response = new Response();
            Account sourceAccount; //our Bank Settlement Account
            Account destinationAccount; //individual
            Transactions transaction = new Transactions();
            transaction.CustomerId = CustomerID;

            var authenticateUser = _dbContext.Account.Where(item=>item.AccountNumberGenerated == AccountNumber);
            if (authenticateUser == null)
            {
                throw new ApplicationException("Invalid Account details Provided");
            }

            try
            {
                sourceAccount = _userService.GetByAccountNumber(_bankSettlementAccount);
                destinationAccount = _userService.GetByAccountNumber(AccountNumber);

                sourceAccount.CurrentAccountBalance -= Amount;
                destinationAccount.CurrentAccountBalance += Amount;

                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) && (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    //sso there was an update
                    transaction.TranStatus = "Success";
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Successful!";
                    response.Data = null;

                }
                else
                {
                    transaction.TranStatus ="Failed";
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Failed!";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"ERROR OCCURRED => MESSAGE: {ex.Message}");
            }

            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = "Deposit";
            transaction.TransactionAmount = Amount;
            transaction.TransactionSourceAccount = Convert.ToDecimal(_bankSettlementAccount);
            transaction.TransactionDestinationAccount = Convert.ToDecimal(AccountNumber);
            //transaction.TransactionParticulars = $"NEW Transaction FROM SOURCE {JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} TO DESTINATION => {JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} ON {transaction.TransactionDate} TRAN_TYPE =>  {transaction.TransactionType} TRAN_STATUS => {transaction.TransactionStatus}";

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();


            return response;

        }

        public Response MakeFundsTransfer(string FromAccount, string ToAccount, decimal Amount, decimal CustomerID)
        {

            _logger.LogInformation($"Fund Transfer Intiated Source Account{FromAccount} To Account:{ToAccount}");
            Response response = new Response();
            Account sourceAccount; //our Bank Settlement Account
            Account destinationAccount; //individual
            Transactions transaction = new Transactions();
            transaction.CustomerId = CustomerID;

            var _validateFromAccount = _dbContext.Account.Where(item => item.AccountNumberGenerated == FromAccount);
            if (_validateFromAccount == null)
            {
                throw new ApplicationException("Invalid Account details Provided");
            }

            try
            {
                sourceAccount = _userService.GetByAccountNumber(FromAccount);
                destinationAccount = _userService.GetByAccountNumber(ToAccount);

                sourceAccount.CurrentAccountBalance -= Amount;
                destinationAccount.CurrentAccountBalance += Amount;

                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) && (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    //sso there was an update
                    transaction.TranStatus = "Success";
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Successful!";
                    response.Data = null;

                }
                else
                {
                    transaction.TranStatus = "Failed";
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Failed!";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"ERROR OCCURRED => MESSAGE: {ex.Message}");
            }

            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = "Transfer";
            transaction.TransactionAmount = Amount;
            transaction.TransactionSourceAccount = Convert.ToDecimal(FromAccount);
            transaction.TransactionDestinationAccount = Convert.ToDecimal(ToAccount);
            //transaction.TransactionParticulars = $"NEW Transaction FROM SOURCE {JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} TO DESTINATION => {JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} ON {transaction.TransactionDate} TRAN_TYPE =>  {transaction.TransactionType} TRAN_STATUS => {transaction.TransactionStatus}";

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();


            return response;
        }

        public Response MakeWithdrawal(string AccountNumber, decimal Amount, decimal CustomerID)
        {
            Response response = new Response();
            Account sourceAccount; //individual
            Account destinationAccount; //our Bank Settlement Account
            Transactions transaction = new Transactions();

            var _validateFromAccount = _dbContext.Account.Where(item => item.AccountNumberGenerated == AccountNumber);
            if (_validateFromAccount == null)
            {
                throw new ApplicationException("Invalid Account details Provided");
            }

            try
            {
                sourceAccount = _userService.GetByAccountNumber(AccountNumber);
                destinationAccount = _userService.GetByAccountNumber(_bankSettlementAccount);

                sourceAccount.CurrentAccountBalance -= Amount; //remove the tranamount from the customer's balance
                destinationAccount.CurrentAccountBalance += Amount; //add tranamount to our bankSettlement...

                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) && (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    //so there was an update in the context State
                    transaction.TranStatus = "Success";
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Successful!";
                    response.Data = null;

                }
                else
                {
                    transaction.TranStatus = "Failed";
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Failed!";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"AN ERROR OCCURRED => MESSAGE: {ex.Message}");
            }

            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = "Withdrawal";
            transaction.TransactionAmount = Amount;
            transaction.TransactionSourceAccount =Convert.ToDecimal(_bankSettlementAccount);
            transaction.TransactionDestinationAccount =Convert.ToDecimal(AccountNumber);
            //transaction.TransactionParticulars = $"NEW Transaction FROM SOURCE {JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} TO DESTINATION => {JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} ON {transaction.TransactionDate} TRAN_TYPE =>  {transaction.TransactionType} TRAN_STATUS => {transaction.TransactionStatus}";

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();


            return response;
        }
    }
}
