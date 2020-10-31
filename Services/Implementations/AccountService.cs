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
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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
            IConfiguration config
            )
        {
            _context = new AuthDbContext(settings);
            _mapper = mapper;
            _config = config;
           
        }
       

        public async Task RegisterAsync(RegisterRequest model, int pageId=1)
        {

         

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
                AcceptTerms=model.AcceptTerms,
               
            };

            if (pageId == 1) { account.Role = "User"; }
            else if (pageId == 2) {account.Role = "Admin"; }

            // first registered account is an admin
            // var isFirstAccount = await _context.Account.EstimatedDocumentCountAsync() == 0;
            // account.Role = isFirstAccount ? Role.Admin : Role.User;
            // account.Role = (Role)(account.Role != null ? 0 : 1);
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
            if (model==null){
                return null;
            }
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
        public async Task<IList<Account>> GetAllAsync()
        {
            return await _context.Account.Find(Account => true).ToListAsync();
        }

        public async Task<Account> GetAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return null;
            }

            var filterId = Builders<Account>.Filter.Eq("_id", objectId);
            var model = await _context.Account.Find(filterId).FirstOrDefaultAsync();

            return model;

        }

        public AuthenticateResult GetToken(Account user)
        {


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            //hard code the setting
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(120);
           
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
                      );

            return new AuthenticateResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Email = user.Email
            };
           
        }






    }
}

