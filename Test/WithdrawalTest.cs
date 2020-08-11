using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Libraries;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    internal class WithdrawalTest
    {
        private readonly Customer _mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo", "ugoo");

        [Test]
        public void Attempting_To_withdraw_Without_Logging_In()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 300000);
            _mine.LogOut();

            Assert.That(() => accountOne.MakeWithdrawal(3000, DateTime.Now, "attempting withdrawal without logging in"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Attempting_To_Debit_A_Negative_Value()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 300000);

            Assert.That(() => accountOne.MakeWithdrawal(-1000, DateTime.Now, "attempting withdrawal a negative amount"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void When_Account_Has_Insufficient_Funds()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 30000);

            Assert.That(() => accountOne.MakeWithdrawal(200000, DateTime.Now, "attempting withdrawal a negative amount"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_Amount_Was_Actually_Debited_From_The_Account()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account accountOne = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new current account", DateTime.Now, AccountType.Current, 30000);
        }

        [Test]
        public void Check_If_Transaction_Was_Added_To_Accounts_Transaction_List()
        {
        }
    }
}