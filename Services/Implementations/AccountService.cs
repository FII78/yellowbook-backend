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
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace FindIt.Backend.Services.Implementations
{
    public class AccountService : IAccountService
    {

        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AccountService(AuthDbContext context)
        {
            _context = context;
        }

        public AccountService(
           
           IMapper mapper,
           IOptions<Settings> settings,
            IConfiguration config)
        {
            _context = new AuthDbContext(settings);
            _mapper = mapper;
            _config = config;
        }

        
        
        public async Task RegisterAsync(RegisterRequest model)
        {

            // validation

            if (string.IsNullOrWhiteSpace(model.Password))
                throw new AppException("Password is required");

            var collection = _context.Account.AsQueryable();

            var result = (from item in collection
                          select item)
                         .Any(item => item.Email == model.Email);

            if (result)
                throw new AppException("Username \"" + model.Email + "\" is already taken");

                       
            // map model to new account object

            var account = new Account()
            {
                Title=model.Title,
                FirstName=model.FirstName,
                LastName=model.LastName,
                Email=model.Email,
                AcceptTerms=model.AcceptTerms
            };

            // first registered account is an admin
            var isFirstAccount = await _context.Account.EstimatedDocumentCountAsync() == 0;
            account.Role = isFirstAccount ? Role.Admin : Role.User;
            account.Created = DateTime.UtcNow;

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);

            // save account
            await _context.Account.InsertOneAsync(account);

        }

        public AuthenticateResponse Authenticate(string email, string password)
        { 
           
            var builder = Builders<Account>.Filter;
            var encryptedPassword = BC.HashPassword(password);
     
            var model =   _context.Account.Find(s => s.Email == email).FirstOrDefault();
            bool checker = BC.Verify(password,model.PasswordHash);
               
            if (checker)
            {
                var response = new AuthenticateResponse()
                {
                    Id = model.Id,
                    Title = model.Title,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Role = model.Role.ToString(),
                    Created = model.Created,
                    Updated = model.Updated
                };

                return response;
            }
            else return null;

        }

        public AuthenticateResult GetToken(Account user)
        {
            var key = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (_config["Jwt:Site"],
                _config["Jwt:Audience"],
                null,
             expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:ExpiryInMinutes"])),
             signingCredentials: creds);

            return new AuthenticateResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Email = user.Email
            };
        }






    }
}

