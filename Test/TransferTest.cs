using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Libraries;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    internal class TransferTest
    {
        private readonly Customer _mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo", "ugoo");

        [Test]
        public void Attempting_To_Transfer_Without_logging_In()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account creditor = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Current, 300000);
            Account beneficiary = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 1000);
            _mine.LogOut();

            Assert.That(() => creditor.TransferMoney(beneficiary, 25000, DateTime.Now, "attempting to transfer without logging in"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Attempting_To_Transfer_A_Negative_Amount()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account creditor = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Current, 300000);
            Account beneficiary = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 1000);

            Assert.That(() => creditor.TransferMoney(beneficiary, -25000, DateTime.Now, "attempting to transfer negative amount"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Attempting_To_Transfer_An_amount_Greater_Than_The_Actual_Balance()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account creditor = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Current, 3000);
            Account beneficiary = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 1000);

            Assert.That(() => creditor.TransferMoney(beneficiary, 25000, DateTime.Now, "attempting to overdraft "), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_Amount_Was_Actually_Debited_From_Creditors_Account()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account creditor = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Current, 300000);
            Account beneficiary = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 1000);

            //Act
            decimal creditorsInitialBal = creditor.Balance;
            decimal amount = 25000;
            creditor.TransferMoney(beneficiary, amount, DateTime.Now, "transferring funds ");
            decimal creditorsCurrentBal = creditor.Balance;

            //Assert
            Assert.That(creditorsCurrentBal, Is.EqualTo(creditorsInitialBal - amount));
        }

        [Test]
        public void Check_If_Amount_Was_Actually_Credited_To_Beneficiary_Account()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account creditor = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Current, 300000);
            Account beneficiary = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 1000);

            //Act
            decimal beneficiaryInitialBal = beneficiary.Balance;
            decimal amount = 25000;
            creditor.TransferMoney(beneficiary, amount, DateTime.Now, "transferring funds ");
            decimal beneficiaryCurrentBal = beneficiary.Balance;

            //Assert
            Assert.That(beneficiaryCurrentBal, Is.EqualTo(beneficiaryInitialBal + amount));
        }

        [Test]
        public void Check_If_Transaction_Was_Added_To_Creditors_Transaction_List()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account creditor = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Current, 300000);
            Account beneficiary = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 1000);

            //Act
            int initialNumTransactions = creditor.GeTransactions().Count;
            decimal amount = 25000;
            creditor.TransferMoney(beneficiary, amount, DateTime.Now, "transferring funds");
            int currentNumTransactions = creditor.GeTransactions().Count;

            Assert.That(currentNumTransactions, Is.EqualTo(initialNumTransactions + 1));
        }

        [Test]
        public void Check_If_Transaction_Was_Added_To_Beneficiary_Transaction_List()
        {
            _mine.LogIn("ugoo", "ugoo");
            Account creditor = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Current, 300000);
            Account beneficiary = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 1000);

            //Act
            int initialNumTransactions = beneficiary.GeTransactions().Count;
            decimal amount = 25000;
            creditor.TransferMoney(beneficiary, amount, DateTime.Now, "transferring funds");
            int currentNumTransactions = beneficiary.GeTransactions().Count;

            Assert.That(currentNumTransactions, Is.EqualTo(initialNumTransactions + 1));
        }
    }
}