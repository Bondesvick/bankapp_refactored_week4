using BankApp.Libraries;
using NUnit.Framework;
using System;

namespace Test
{
    [TestFixture]
    internal class DepositTest
    {
        private Customer _mine;
        private Account _accountOne;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mine = new Customer("ugo", "nwike", "creating new customer profile", "ugo", "ugo");
            _mine.LogIn("ugo", "ugo");

            _accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 300000);
        }

        [Test]
        public void Attempting_To_Deposit_a_Negative_Value()
        {
            _mine.LogIn("ugo", "ugo");

            //Assert
            Assert.That(() => _accountOne.MakeDeposit(-2000, DateTime.Now, "attempting to deposit negative value"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Check_If_Amount_Was_Actually_Deposited()
        {
            //Arrange
            _mine.LogIn("ugo", "ugo");

            //Act
            decimal balB4Deposit = _accountOne.Balance;
            decimal amount = 2000;
            _accountOne.MakeDeposit(amount, DateTime.Now, "attempting to deposit negative value");
            decimal balAfterDeposit = _accountOne.Balance;

            Assert.That(balB4Deposit, Is.LessThan(balAfterDeposit));
        }
    }
}