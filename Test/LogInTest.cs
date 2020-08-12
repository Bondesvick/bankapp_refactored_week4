using BankApp.Helpers;
using BankApp.Libraries;
using NUnit.Framework;
using System;

namespace Test
{
    [TestFixture]
    public class LogInTests
    {
        private Customer _mine;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mine = new Customer("ugo", "nwike", "creating new customer profile", "ugo33", "ugo33");
        }

        [Test]
        public void Check_If_Actually_Logged_In()
        {
            //Arrange
            _mine.LogIn("ugo33", "ugo33");

            //Act
            bool confirm = _mine.IsLoggedIn;

            //Assert
            Assert.That(confirm, Is.True);
        }

        [Test]
        public void Check_If_Actually_Logged_out()
        {
            _mine.LogOut();

            //Act
            bool confirm = _mine.IsLoggedIn;

            Assert.That(confirm, Is.False);
        }

        [Test]
        public void Attempting_To_LogIn_A_Customer_That_Does_Not_Exist()
        {
            Assert.That(() => BankOperations.GetCustomer("gh").LogIn("gh", "rt"), Throws.TypeOf<Exception>());
        }
    }
}