﻿using System.ComponentModel.DataAnnotations;

namespace NetBankingApp.Core.Application.ViewModels.Loan
{
    public class SaveLoanViewModel
    {
        //public int? Id { get; set; }
        public string? Guid { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "The amount entered is not even greater than 0.")]
        public double LoanAmount { get; set; }

        public double Debt {  get; set; }
        public string IdCustomer { get; set; }
    }
}
