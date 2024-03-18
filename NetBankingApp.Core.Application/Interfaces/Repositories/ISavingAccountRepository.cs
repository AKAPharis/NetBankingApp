﻿using NetBankingApp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.Interfaces.Repositories
{
    public interface ISavingAccountRepository : IGenericRepository<SavingAccount>
    {
        Task<int> Total();
        Task<int> DailyTotal();
    }
}
