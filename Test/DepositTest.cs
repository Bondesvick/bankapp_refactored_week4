using BankApp.Libraries;
using NUnit.Framework;
using System;

namespace Test
{
    [TestFixture]
    internal class DepositTest
    {
        private readonly Customer _mine = new Customer("ugo", "nwike", "creating new customer profile", "ugo", "ugo");

        [Test]
        public void Attempting_To_Deposit_a_Negative_Value()
        {
            _mine.LogIn("ugo", "ugo");
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 300000);

            //Assert
            Assert.That(() => accountOne.MakeDeposit(-2000, DateTime.Now, "attempting to deposit negative value"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Check_If_Amount_Was_Actually_Deposited()
        {
            //Arrange
            _mine.LogIn("ugo", "ugo");
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 300000);

            //Act
            decimal balB4Deposit = accountOne.Balance;
            decimal amount = 2000;
            accountOne.MakeDeposit(amount, DateTime.Now, "attempting to deposit negative value");
            decimal balAfterDeposit = accountOne.Balance;

            Assert.That(balB4Deposit, Is.LessThan(balAfterDeposit));
        }
    }
}