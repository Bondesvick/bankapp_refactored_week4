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
        private Customer _mine;
        private Account _newAccount;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo44", "ugoo44");
            _mine.LogIn("ugoo44", "ugoo44");
            _newAccount = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 30000);
        }

        [Test]
        public void Check_If_Amount_Is_Same_As_That_In_Transaction_List()
        {
            //Arrange
            _mine.LogIn("ugoo44", "ugoo44");
            decimal amount = 40000;
            _newAccount.MakeDeposit(amount, DateTime.Now, "making deposit");

            //Act
            bool confirm = _newAccount.GeTransactions()[^1].Amount == amount;

            //Assert
            //Assert.True(confirm);
            Assert.That(confirm, Is.True);
        }

        [Test]
        public void Check_If_The_Transaction_Actually_Occured()
        {
            //Arrange
            _mine.LogIn("ugoo44", "ugoo44");
            int countBefore = _newAccount.GeTransactions().Count;
            decimal amount = 40000;
            _newAccount.MakeDeposit(amount, DateTime.Now, "making deposit");
            int countAfter = _newAccount.GeTransactions().Count;

            //Act
            int expected = countAfter - 1;

            //Assert
            Assert.That(countBefore, Is.EqualTo(expected));
        }

        [Test]
        public void Check_If_The_Transaction_Was_Actually_In_The_Account_Transaction_List()
        {
            //Arrange
            _mine.LogIn("ugoo44", "ugoo44");
            decimal amount = 40000;
            Transaction theTransaction = _newAccount.MakeDeposit(amount, DateTime.Now, "making deposit");

            //Act
            bool confirm = _newAccount.GeTransactions().Any(transaction => transaction == theTransaction);

            //Assert
            //Assert.True(confirm);
            Assert.That(confirm, Is.True);
        }
    }
}