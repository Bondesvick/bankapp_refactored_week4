using BankApp.Libraries;
using NUnit.Framework;
using System;

namespace Test
{
    [TestFixture]
    internal class TransferTest
    {
        private Customer _mine;
        private Account _creditor;
        private Account _beneficiary;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mine = new Customer("victor", "Nwike", "bondesvick@gmail.com", "ugoo11", "ugoo11");
            _mine.LogIn("ugoo11", "ugoo11");
            _creditor = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Current, 300_000);
            _beneficiary = new Account(_mine, _mine.FullName, _mine.CustomerId, "creating a new savings account", DateTime.Now, AccountType.Savings, 1_000);
        }

        [Test]
        public void Attempting_To_Transfer_Without_logging_In()
        {
            _mine.LogOut();

            //Assert
            Assert.That(() => _creditor.TransferMoney(_beneficiary, 25_000, DateTime.Now, "attempting to transfer without logging in"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Attempting_To_Transfer_A_Negative_Amount()
        {
            _mine.LogIn("ugoo11", "ugoo11");

            //Assert
            Assert.That(() => _creditor.TransferMoney(_beneficiary, -25_000, DateTime.Now, "attempting to transfer negative amount"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Attempting_To_Transfer_An_amount_Greater_Than_The_Actual_Balance()
        {
            _mine.LogIn("ugoo11", "ugoo11");

            //Assert
            Assert.That(() => _creditor.TransferMoney(_beneficiary, 250_000_000, DateTime.Now, "attempting to overdraft "), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Check_If_Amount_Was_Actually_Debited_From_Creditors_Account()
        {
            //Arrange
            _mine.LogIn("ugoo11", "ugoo11");

            //Act
            decimal creditorsInitialBal = _creditor.Balance;
            decimal amount = 25000;
            _creditor.TransferMoney(_beneficiary, amount, DateTime.Now, "transferring funds ");
            decimal creditorsCurrentBal = _creditor.Balance;

            //Assert
            Assert.That(creditorsCurrentBal, Is.EqualTo(creditorsInitialBal - amount));
        }

        [Test]
        public void Check_If_Amount_Was_Actually_Credited_To_Beneficiary_Account()
        {
            //Arrange
            _mine.LogIn("ugoo11", "ugoo11");
            decimal beneficiaryInitialBal = _beneficiary.Balance;
            decimal amount = 25000;
            _creditor.TransferMoney(_beneficiary, amount, DateTime.Now, "transferring funds ");
            decimal beneficiaryCurrentBal = _beneficiary.Balance;

            //Act
            decimal expected = beneficiaryInitialBal + amount;

            //Assert
            Assert.That(beneficiaryCurrentBal, Is.EqualTo(expected));
        }

        [Test]
        public void Check_If_Transaction_Was_Added_To_Creditors_Transaction_List()
        {
            //Arrange
            _mine.LogIn("ugoo11", "ugoo11");
            int initialNumTransactions = _creditor.GeTransactions().Count;
            decimal amount = 25000;
            _creditor.TransferMoney(_beneficiary, amount, DateTime.Now, "transferring funds");
            int currentNumTransactions = _creditor.GeTransactions().Count;

            //Act
            int expected = initialNumTransactions + 1;

            //Assert
            Assert.That(currentNumTransactions, Is.EqualTo(expected));
        }

        [Test]
        public void Check_If_Transaction_Was_Added_To_Beneficiary_Transaction_List()
        {
            //Arrange
            _mine.LogIn("ugoo11", "ugoo11");
            int initialNumTransactions = _beneficiary.GeTransactions().Count;
            decimal amount = 25000;
            _creditor.TransferMoney(_beneficiary, amount, DateTime.Now, "transferring funds");
            int currentNumTransactions = _beneficiary.GeTransactions().Count;

            //Act
            int expected = initialNumTransactions + 1;

            //Assert
            Assert.That(currentNumTransactions, Is.EqualTo(expected));
        }
    }
}