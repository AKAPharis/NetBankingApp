using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.Dtos.Logs
{
    public class CreateTransactionLogDTO
    {
        public string GuidAccountOrigin { get; set; }
        public string GuidAccountDestination { get; set; }
        public double Amount { get; set; }
    }
}
