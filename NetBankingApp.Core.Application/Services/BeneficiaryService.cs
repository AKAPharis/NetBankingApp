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
        private readonly ISavingAccountService _savingAccountService;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IUserService userService, IMapper mapper, ISavingAccountService savingAccountService)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _userService = userService;
            _mapper = mapper;
            _savingAccountService = savingAccountService;
        }

        public async Task<SaveBeneficiaryViewModel> CreateBeneficiary(SaveBeneficiaryViewModel vm)
        {
            var account = await _savingAccountService.GetByGuid(vm.BeneficiaryAccountGuid);
            var beneficiary = await _userService.GetByIdAsync(account.IdCustomer);
            vm.IdBeneficiary = beneficiary.Id;
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
        public async Task<BeneficiaryViewModel> GetBeneficiary(string idUser, string idBeneficiary)
        {
            return _mapper.Map<BeneficiaryViewModel>(await _beneficiaryRepository.GetBeneficiary(idUser,idBeneficiary));
        }
        public async Task<SaveBeneficiaryViewModel> GetBeneficiarySaveViewModel(string idUser, string idBeneficiary)
        {
            return _mapper.Map<SaveBeneficiaryViewModel>(await _beneficiaryRepository.GetBeneficiary(idUser, idBeneficiary));
        }
    }
}
