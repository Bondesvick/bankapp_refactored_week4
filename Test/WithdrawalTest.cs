using BankApp.BankData;
using BankApp.Libraries;
using NUnit.Framework;
using System;

namespace Test
{
    [TestFixture]
    internal class WithdrawalTest
    {
        private readonly Customer _mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo00", "ugoo00");

        [Test]
        public void Attempting_To_withdraw_Without_Logging_In()
        {
            //Arrange
            //Logging in
            _mine.LogIn("ugoo00", "ugoo00");
            //Creating a bank account
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 300000);
            //logging out
            _mine.LogOut();

            //Assert
            //making withdrawal
            Assert.That(() => accountOne.MakeWithdrawal(3000, DateTime.Now, "attempting withdrawal without logging in"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Attempting_To_Debit_A_Negative_Value()
        {
            _mine.LogIn("ugoo00", "ugoo00");
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 300000);

            Assert.That(() => accountOne.MakeWithdrawal(-1000, DateTime.Now, "attempting withdrawal a negative amount"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void When_Account_Has_Insufficient_Funds()
        {
            _mine.LogIn("ugoo00", "ugoo00");
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 30000);

            Assert.That(() => accountOne.MakeWithdrawal(200000, DateTime.Now, "attempting withdrawal a negative amount"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_Amount_Was_Actually_Debited_From_The_Account()
        {
            //Arrange
            _mine.LogIn("ugoo00", "ugoo00");
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 30000);

            //Act
            decimal balanceB4Withdrawal = accountOne.Balance;
            accountOne.MakeWithdrawal(2000, DateTime.Now, "debiting my account");
            decimal balanceAfterWithdrawal = accountOne.Balance;

            //Assert
            Assert.That(balanceB4Withdrawal, Is.GreaterThan(balanceAfterWithdrawal));
        }

        [Test]
        public void Check_If_Transaction_Was_Added_To_Accounts_Transaction_List()
        {
            //Arrange
            _mine.LogIn("ugoo00", "ugoo00");
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 30000);

            //Act
            Transaction newOne = accountOne.MakeWithdrawal(2000, DateTime.Now, "debiting my account");
            bool confirm = accountOne.GeTransactions().Contains(newOne);

            //Assert
            Assert.That(confirm, Is.EqualTo(true));
        }
    }
}