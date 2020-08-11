using BankApp.BankData;
using BankApp.Libraries;
using NUnit.Framework;
using System;
using System.Linq;

namespace Test
{
    [TestFixture]
    internal class TransactionTest
    {
        private readonly Customer _mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo44", "ugoo44");

        [Test]
        public void Check_If_Amount_Is_Same_As_That_In_Transaction_List()
        {
            //Arrange
            _mine.LogIn("ugoo44", "ugoo44");
            Account newAccount = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 30000);
            decimal amount = 40000;
            newAccount.MakeDeposit(amount, DateTime.Now, "making deposit");

            //Act
            bool confirm = newAccount.GeTransactions()[^1].Amount == amount;

            //Assert
            //Assert.True(confirm);
            Assert.That(confirm, Is.EqualTo(true));
        }

        [Test]
        public void Check_If_The_Transaction_Actually_Occured()
        {
            //Arrange
            _mine.LogIn("ugoo44", "ugoo44");
            Account newAccount = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 30000);

            //Act
            int countBefore = newAccount.GeTransactions().Count;
            decimal amount = 40000;
            newAccount.MakeDeposit(amount, DateTime.Now, "making deposit");
            int countAfter = newAccount.GeTransactions().Count;

            //Assert
            Assert.That(countBefore, Is.EqualTo(countAfter - countBefore));
        }

        [Test]
        public void Check_If_The_Transaction_Was_Actually_In_The_Account_Transaction_List()
        {
            //Arrange
            _mine.LogIn("ugoo44", "ugoo44");
            Account newAccount = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 30000);
            decimal amount = 40000;
            Transaction theTransaction = newAccount.MakeDeposit(amount, DateTime.Now, "making deposit");

            //Act
            bool confirm = newAccount.GeTransactions().Any(transaction => transaction == theTransaction);

            //Assert
            //Assert.True(confirm);
            Assert.That(confirm, Is.EqualTo(true));
        }
    }
}