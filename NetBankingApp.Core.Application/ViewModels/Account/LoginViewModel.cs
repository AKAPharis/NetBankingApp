﻿using System.ComponentModel.DataAnnotations;

namespace NetBankingApp.Core.Application.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please fill the username input")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please fill the password input")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
