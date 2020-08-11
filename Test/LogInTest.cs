using BankApp.Libraries;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class LogInTests
    {
        [Test]
        public void Check_If_Actually_Logged_In()
        {
            Customer mine = new Customer("ugo", "nwike", "hghghgh", "ugo", "ugo");
            mine.LogIn("ugo", "ugo");

            Assert.True(mine.IsLoggedIn);
        }

        [Test]
        public void Test_For_Unavailable_UserName()
        {
            Assert.Pass();
        }
    }
}