using Microsoft.AspNetCore.Identity;

namespace NetBankingApp.Infrastucture.Identity.Models
{
    public class BankingUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentId { get; set; }
        public bool IsActived { get; set; }

    }
}
