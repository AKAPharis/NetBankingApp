using NetBankingApp.Core.Application.Enums;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NetBankingApp.Core.Application.Helpers
{
    public static class GuidHelper
    {
        public static string Guid(int type) 
        {
            string guid;
            switch (type)
            {
                case (int)Products.CreditCard:
                    guid = "003";
                    break;
                case (int)Products.SavingAccount:
                    guid = "004";

                    break;
                case (int)Products.Loan:
                    guid = "005";

                    break;
                default:
                    return null;

            }
            StringBuilder sb = new StringBuilder(guid);
            Random rnd = new Random();
            sb.Append(rnd.Next(100000, 999999).ToString());

            return sb.ToString();
        }

    }
}
