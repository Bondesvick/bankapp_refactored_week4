using BankApp.BankData;
using System;

namespace BankApp.Libraries
{
    public interface IAccount
    {
        public Transaction MakeDeposit(decimal amount, DateTime date, string note);

        public Transaction MakeWithdrawal(decimal amount, DateTime date, string note);

        public void TransferMoney(Account destination, decimal amount, DateTime date, string note);

        public void GetAccountHistory();
    }
}