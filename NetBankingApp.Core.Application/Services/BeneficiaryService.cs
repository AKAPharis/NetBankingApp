﻿using AutoMapper;
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
            if (account == null)
            {
                vm.Error = $"There's no an account with the guid {vm.BeneficiaryAccountGuid}";
                vm.HasError = true;
                return vm;
            }
            var beneficiary = await _userService.GetByIdAsync(account.IdCustomer);
            if(beneficiary == null)
            {
                vm.Error = $"We couldn't found the beneficiary";
                vm.HasError = true;
                return vm;
            }
            if (beneficiary.Id == vm.IdUser)
            {
                vm.Error = $"You cannot be a beneficiary of yourself";
                vm.HasError = true;
                return vm;
            }
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
            beneficiaries.ForEach( beneficiary => {
                var user =  _userService.GetByIdAsync(beneficiary.IdBeneficiary);
                beneficiary.BeneficiaryLastName = user.Result.LastName;
                beneficiary.BeneficiaryFirstName = user.Result.FirstName;
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
