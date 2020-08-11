using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankApp.BankData;
using BankApp.Libraries;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    internal class RegisterTest
    {
        private Customer mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo", "ugoo");

        [Test]
        public void When_Username_Entered_Already_Exist()
        {
            Assert.That(() => new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo", "ugoo"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_The_New_Customer_Was_Actually_Added_To_Banks_Customer_List()
        {
            //Arrange
            Customer mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo", "ugoo");

            //Act
            bool confirm = Bank.Customers.Any(customer => customer == mine);

            //Assert
            //Assert.True(confirm);
            //Assert.That(Instance, Is.SameAs(AnotherInstance));
            //Assert.That(Instance, Is.Not.SameAs(AnotherInstance));
            Assert.That(confirm, Is.EqualTo(true));
        }

        [Test]
        public void Check_If_Initial_Amount_meets_the_criteria_for_A_Current_Account()
        {
            mine.LogIn("ugoo", "ugoo");
            Assert.That(() => new Account(mine, mine.FullName, mine.CustomerId, "new current account with amount below required", DateTime.Now, AccountType.Current, 500), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_Initial_Amount_meets_the_criteria_for_A_Savings_Account()
        {
            mine.LogIn("ugoo", "ugoo");
            Assert.That(() => new Account(mine, mine.FullName, mine.CustomerId, "new savings account with amount below required", DateTime.Now, AccountType.Savings, 70), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_Account_Was_added_To_Banks_Account_List()
        {
        }

        [Test]
        public void Check_If_Account_Was_added_To_Customer_Account_List()
        {
        }
    }
}