using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApi.Authentication;
using BankingApi.DAL;
using BankingApi.Model;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BankingApi.Services.Interfaces;
using BankingApi.Services.Implementation;
using Microsoft.AspNetCore.Authorization;

namespace BankingApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private  ITransaction _transactionService;
        public TransactionController(ITransaction TransactionService)
        {
           _transactionService = TransactionService;
        }
        
        [HttpPost]
        [Route("Deposit_Amount")]
        public IActionResult SelfDeposit([FromBody] Withdrawal Depositerequst)
        {
            if (!ModelState.IsValid) return BadRequest(Depositerequst);
            var AccountNumber = Depositerequst.AccounNumber;
            var Amount = Depositerequst.Amount;
            decimal _customerID = Depositerequst.CustomerId;
            
            var result = _transactionService.MakeDeposit(AccountNumber, (decimal)Amount, _customerID);
            return Ok(result);
        }

        [HttpPost]
        [Route("Account_Transfer")]
        public IActionResult FundTransfer([FromBody] AccountTransfer FundTransferRequst)
        {
            if (!ModelState.IsValid) return BadRequest(FundTransferRequst);
            string _sourceAccount  = FundTransferRequst.FromAccount;
            string _destinationAccount = FundTransferRequst.ToAccount;
            var Amount = FundTransferRequst.Amount;
            decimal _customerID = FundTransferRequst.CustomerId;

            var result = _transactionService.MakeFundsTransfer(_sourceAccount,_destinationAccount, (decimal)Amount, _customerID);
            return Ok(result);
        }

        [HttpPost]
        [Route("Withdrawal_Funds")]
        public IActionResult Withdrawal([FromBody] Withdrawal WithdrawalRequst)
        {
            if (!ModelState.IsValid) return BadRequest(WithdrawalRequst);
            string _AccountNumber = WithdrawalRequst.AccounNumber;
            decimal Amount = (decimal)WithdrawalRequst.Amount;
            decimal _customerID = WithdrawalRequst.CustomerId;

            var result = _transactionService.MakeWithdrawal(_AccountNumber, Amount, _customerID);
            return Ok(result);
        }


    }
}
