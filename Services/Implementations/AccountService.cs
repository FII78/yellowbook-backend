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
using FindIt.API.Helpers;
using FindIt.API.Models.Accounts;

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
           IOptions<MongoSettings> settings,
            IConfiguration config
            )
        {
            _context = new AuthDbContext(settings);
            _mapper = mapper;
            _config = config;
           
        }


        public async Task RegisterAsync(RegisterRequest model, int pageId)
        {
            if (model==null)
            {
                throw new ApiException($"Invalid request because nothing to process");
            }
            if (pageId != 1 && pageId!= 2)
            {
                throw new ApiException($"Role can not be assigned, Invalid value for pageId supplied");
            }
            var userWithSameEmail = await _context.Account.Find(user => user.Email == model.Email).CountDocumentsAsync();

            if (userWithSameEmail > 0)
            {
                throw new ApiException($"Email '{model.Email}' is already taken.");
            }
            else
            {
                var account = _mapper.Map<Account>(model);

                if (pageId == 1) { account.Role = "User"; }
                else if (pageId == 2) { account.Role = "Admin"; }

                account.Created = DateTime.UtcNow;
                account.PasswordHash = BC.HashPassword(model.Password);

                await _context.Account.InsertOneAsync(account);
            }
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest loginRequest)
        {
            if (loginRequest == null)
            {
                throw new ApiException($"Nothing found to process.");
            }
            var model = await _context.Account.Find(s => s.Email == loginRequest.Email).FirstOrDefaultAsync();

            if (model == null)
            {
                throw new ApiException($"No Accounts Registered with {loginRequest.Email}.");
            }

            bool checker = BC.Verify(loginRequest.Password, model.PasswordHash);


            if (!checker)
            {
                throw new ApiException($"Invalid Credentials for '{loginRequest.Email}'.");

            }
            else
            {
                var response = _mapper.Map<AuthenticateResponse>(model);
                return response;
            }

        }
        public async Task<IEnumerable<AccountsVM>> GetAllAsync()
        {
            var response= await _context.Account.Find(Account => true).ToListAsync();
            return response.ConvertAllToViewModels();
                  
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

