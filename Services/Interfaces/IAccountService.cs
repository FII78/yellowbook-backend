﻿using FindIt.API.Models.Accounts;
using FindIt.Backend.Entities;
using FindIt.Backend.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Backend.Services.Interfaces
{
    public interface IAccountService
    {
        //
        Task RegisterAsync(RegisterRequest model , int pageId);
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest loginRequest);
        AuthenticateResult GetToken(Account user);
        Task<Account> GetAsync(string id);
        Task<IEnumerable<AccountsVM>> GetAllAsync();



    }

}
