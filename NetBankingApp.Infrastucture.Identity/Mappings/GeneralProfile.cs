﻿using AutoMapper;
using NetBankingApp.Core.Application.ViewModels.Account;
using NetBankingApp.Infrastucture.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Infrastucture.Identity.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {


            #region User

            CreateMap<BankingUser, UserViewModel>()
                .ReverseMap();
            CreateMap<BankingUser, SaveUserViewModel>()
                .ReverseMap();



            #endregion
        }
    }
}
