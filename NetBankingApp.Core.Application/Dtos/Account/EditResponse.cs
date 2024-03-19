using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.Dtos.Account
{
    public class EditResponse
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
    }

}
