using NetBankingApp.Core.Application.Dtos.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface ITransactionLogService
    {
        Task<int> Total();
        Task<int> DailyTotal();
        Task<CreateTransactionLogDTO> AddLog(CreateTransactionLogDTO logDTO);
    }
}
