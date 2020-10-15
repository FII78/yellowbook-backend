using AutoMapper;
using FindIt.Backend.Entities;
using BC = BCrypt.Net.BCrypt;
using FindIt.Backend.Helpers;
using FindIt.Backend.Models.Accounts;
using FindIt.Backend.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Api.Services.Implementations
{
    public class AccountService : IAccountService
    {

        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;

        public AccountService(
           AuthDbContext context,
           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }

      
        public async Task RegisterAsync(RegisterRequest model, string origin)
        {

            if ((_context.Accounts.FindAsync(x => x.Email == model.Email)) != null)
            {
                // send already registered error in email to prevent account enumeration
                sendAlreadyRegisteredEmail(model.Email, origin);
                return;
            }
            // map model to new account object
            var account = _mapper.Map<Account>(model);

            // first registered account is an admin
            var isFirstAccount = await _context.Accounts.EstimatedDocumentCountAsync() == 0;
            account.Role = isFirstAccount ? Role.Admin : Role.User;
            account.Created = DateTime.UtcNow;
            account.VerificationToken = randomTokenString();

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);

            // save account
            await _context.Accounts.InsertOneAsync(account);
           

            // send email
            sendVerificationEmail(account, origin);

        }


    private string randomTokenString()
    {

        using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        var randomBytes = new byte[40];
        rngCryptoServiceProvider.GetBytes(randomBytes);
        // convert random bytes to hex string
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }

    private void sendVerificationEmail(Account account, string origin)
    {
       
    }

    private void sendAlreadyRegisteredEmail(string email, string origin)
    {
        
    }

   
}
}

