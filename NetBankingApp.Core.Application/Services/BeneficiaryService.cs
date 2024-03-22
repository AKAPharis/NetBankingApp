using AutoMapper;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Beneficiary;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IUserService userService, IMapper mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<SaveBeneficiaryViewModel> CreateBeneficiary(SaveBeneficiaryViewModel vm)
        {
            return _mapper.Map<SaveBeneficiaryViewModel>(await _beneficiaryRepository.CreateAsync(_mapper.Map<Beneficiary>(vm)));
        }

        public async Task Delete(SaveBeneficiaryViewModel vm)
        {
            await _beneficiaryRepository.DeleteAsync(await _beneficiaryRepository.GetBeneficiary(vm.IdUser, vm.IdBeneficiary));
        }

        public async Task<List<BeneficiaryViewModel>> GetBenficiaries(string idUser)
        {
            var beneficiaries = _mapper.Map<List<BeneficiaryViewModel>>(await _beneficiaryRepository.GetBeneficiaries(idUser));
            beneficiaries.ForEach(async beneficiary => {
                var user = await _userService.GetByIdAsync(beneficiary.IdBeneficiary);
                beneficiary.BeneficiaryLastName = user.LastName;
                beneficiary.BeneficiaryFirstName = user.FirstName;
                });
            return beneficiaries;
        }
    }
}
