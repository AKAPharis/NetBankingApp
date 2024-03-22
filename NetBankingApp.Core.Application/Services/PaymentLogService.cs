using AutoMapper;
using NetBankingApp.Core.Application.Dtos.Logs;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Services
{
    public class PaymentLogService : IPaymentLogService
    {
        private readonly IPaymentLogRepository _paymentLogRepository;
        private readonly IMapper _mapper;

        public PaymentLogService(IPaymentLogRepository paymentLogRepository, IMapper mapper)
        {
            _paymentLogRepository = paymentLogRepository;
            _mapper = mapper;
        }

        public async Task<CreatePaymentLogDTO> AddLog(CreatePaymentLogDTO logDTO)
        {
            return _mapper.Map<CreatePaymentLogDTO>(await _paymentLogRepository.CreateAsync(_mapper.Map<PaymentLog>(logDTO)));
        }

        public async Task<int> DailyTotal()
        {
            return await _paymentLogRepository.DailyTotal();
        }

        public async Task<int> Total()
        {
            return await _paymentLogRepository.Total();
        }
    }
}
