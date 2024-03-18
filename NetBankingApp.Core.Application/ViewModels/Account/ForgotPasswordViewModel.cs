using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please fill the username input")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
