using BankApp.BankData;
using BankApp.Libraries;
using NUnit.Framework;
using System;

namespace Test
{
    [TestFixture]
    internal class WithdrawalTest//: //IDisposable
    {
        private Customer _mine;
        private Account _accountOne;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo00", "ugoo00");
            _mine.LogIn("ugoo00", "ugoo00");
            _accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 300000);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            //_mine.Dispose();
        }

        [Test]
        public void Attempting_To_withdraw_Without_Logging_In()
        {
            //Arrange
            _mine.LogOut();

            //Assert
            //making withdrawal
            Assert.That(() => _accountOne.MakeWithdrawal(3000, DateTime.Now, "attempting withdrawal without logging in"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Attempting_To_Debit_A_Negative_Value()
        {
            _mine.LogIn("ugoo00", "ugoo00");

            Assert.That(() => _accountOne.MakeWithdrawal(-1000, DateTime.Now, "attempting withdrawal a negative amount"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void When_Account_Has_Insufficient_Funds()
        {
            _mine.LogIn("ugoo00", "ugoo00");

            Assert.That(() => _accountOne.MakeWithdrawal(2000000000, DateTime.Now, "attempting withdrawal a negative amount"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_Amount_Was_Actually_Debited_From_The_Account()
        {
            //Arrange
            _mine.LogIn("ugoo00", "ugoo00");

            //Act
            decimal balanceB4Withdrawal = _accountOne.Balance;
            _accountOne.MakeWithdrawal(2000, DateTime.Now, "debiting my account");
            decimal balanceAfterWithdrawal = _accountOne.Balance;

            //Assert
            Assert.That(balanceB4Withdrawal, Is.GreaterThan(balanceAfterWithdrawal));
        }

        [Test]
        public void Check_If_Transaction_Was_Added_To_Accounts_Transaction_List()
        {
            //Arrange
            _mine.LogIn("ugoo00", "ugoo00");

            //Act
            Transaction newOne = _accountOne.MakeWithdrawal(2000, DateTime.Now, "debiting my account");
            bool confirm = _accountOne.GeTransactions().Contains(newOne);

            //Assert
            Assert.That(confirm, Is.EqualTo(true));
        }
    }
}