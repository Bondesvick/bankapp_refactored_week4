using BankApp.BankData;
using BankApp.Libraries;
using NUnit.Framework;
using System;

namespace Test
{
    [TestFixture]
    internal class RegisterTest
    {
        private Customer _mine;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo", "ugoo");
        }

        [Test]
        public void When_Username_Entered_Already_Exist()
        {
            Assert.That(() => new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo", "ugoo"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_The_New_Customer_Was_Actually_Added_To_Banks_Customer_List()
        {
            //Arrange
            //Act
            bool confirm = Bank.Customers.Contains(_mine);

            //Assert
            //Assert.True(confirm);
            //Assert.That(Instance, Is.SameAs(AnotherInstance));
            //Assert.That(Instance, Is.Not.SameAs(AnotherInstance));
            Assert.That(confirm, Is.True);
        }

        [Test]
        public void When_Initial_Amount_Does_Not_meet_the_criteria_for_A_Current_Account()
        {
            //Act
            _mine.LogIn("ugoo", "ugoo");

            //Assert
            Assert.That(() => new Account(_mine, _mine.FullName, _mine.CustomerId, "new current account with amount below required", DateTime.Now, AccountType.Current, 500), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void When_Initial_Amount_Does_Not_meet_the_criteria_for_A_Savings_Account()
        {
            //Act
            _mine.LogIn("ugoo", "ugoo");

            //Assert
            Assert.That(() => new Account(_mine, _mine.FullName, _mine.CustomerId, "new savings account with amount below required", DateTime.Now, AccountType.Savings, 70), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_Account_Was_added_To_Banks_Account_List()
        {
            //Arrange
            _mine.LogIn("ugoo", "ugoo");
            Account newOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "new savings account", DateTime.Now, AccountType.Savings, 7000);

            //Act
            var confirm = Bank.Accounts.Contains(newOne);

            //Assert
            Assert.That(confirm, Is.True);
        }

        [Test]
        public void Check_If_Account_Was_added_To_Customer_Account_List()
        {
            //Arrange
            _mine.LogIn("ugoo", "ugoo");
            Account newOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "new savings account", DateTime.Now, AccountType.Savings, 7000);

            //Act
            var confirm = _mine.GetAccounts().Contains(newOne);

            //Assert
            Assert.That(confirm, Is.True);
        }
    }
}