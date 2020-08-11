namespace BankApp.Libraries
{
    internal interface ICustomer
    {
        public void LogIn(string userName, string passWord);

        public void LogOut();

        public void GetMyAccountsInfo();
    }
}