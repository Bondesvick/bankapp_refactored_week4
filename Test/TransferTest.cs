using BankApp.Libraries;
using NUnit.Framework;
using System;

namespace Test
{
    [TestFixture]
    internal class TransferTest
    {
        private Customer _mine;
        private Account creditor;
        private Account beneficiary;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo11", "ugoo11");
            _mine.LogIn("ugoo11", "ugoo11");
            creditor = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Current, 300000);
            beneficiary = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 1000);
        }

        [Test]
        public void Attempting_To_Transfer_Without_logging_In()
        {
            _mine.LogOut();

            //Assert
            Assert.That(() => creditor.TransferMoney(beneficiary, 25000, DateTime.Now, "attempting to transfer without logging in"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Attempting_To_Transfer_A_Negative_Amount()
        {
            _mine.LogIn("ugoo11", "ugoo11");

            //Assert
            Assert.That(() => creditor.TransferMoney(beneficiary, -25000, DateTime.Now, "attempting to transfer negative amount"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Attempting_To_Transfer_An_amount_Greater_Than_The_Actual_Balance()
        {
            _mine.LogIn("ugoo11", "ugoo11");

            //Assert
            Assert.That(() => creditor.TransferMoney(beneficiary, 250000000, DateTime.Now, "attempting to overdraft "), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_Amount_Was_Actually_Debited_From_Creditors_Account()
        {
            //Arrange
            _mine.LogIn("ugoo11", "ugoo11");

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
            //Arrange
            _mine.LogIn("ugoo11", "ugoo11");

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
            //Arrange
            _mine.LogIn("ugoo11", "ugoo11");

            //Act
            int initialNumTransactions = creditor.GeTransactions().Count;
            decimal amount = 25000;
            creditor.TransferMoney(beneficiary, amount, DateTime.Now, "transferring funds");
            int currentNumTransactions = creditor.GeTransactions().Count;

            //Assert
            Assert.That(currentNumTransactions, Is.EqualTo(initialNumTransactions + 1));
        }

        [Test]
        public void Check_If_Transaction_Was_Added_To_Beneficiary_Transaction_List()
        {
            //Arrange
            _mine.LogIn("ugoo11", "ugoo11");

            //Act
            int initialNumTransactions = beneficiary.GeTransactions().Count;
            decimal amount = 25000;
            creditor.TransferMoney(beneficiary, amount, DateTime.Now, "transferring funds");
            int currentNumTransactions = beneficiary.GeTransactions().Count;

            //Assert
            Assert.That(currentNumTransactions, Is.EqualTo(initialNumTransactions + 1));
        }
    }
}