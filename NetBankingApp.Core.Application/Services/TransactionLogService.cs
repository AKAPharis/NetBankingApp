using AutoMapper;
using NetBankingApp.Core.Application.Dtos.Logs;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.Services
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IMapper _mapper;

        public TransactionLogService(ITransactionLogRepository transactionLogRepository, IMapper mapper)
        {
            _transactionLogRepository = transactionLogRepository;
            _mapper = mapper;
        }

        public async Task<CreateTransactionLogDTO> AddLog(CreateTransactionLogDTO logDTO)
        {
            return _mapper.Map<CreatePaymentLogDTO>(await _transactionLogRepository.CreateAsync(_mapper.Map<PaymentLog>(logDTO)));
        }

        public async Task<int> DailyTotal()
        {
            return await _transactionLogRepository.DailyTotal();
        }

        public async Task<int> Total()
        {
            return await _transactionLogRepository.Total();
        }
       
    }
}
