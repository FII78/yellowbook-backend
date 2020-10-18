using AutoMapper;
using FindIt.Backend.Models.Accounts;
using FindIt.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using FindIt.Backend.Entities;

namespace FindIt.Backend.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(
            IAccountService accountService,
            IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        


        [HttpPost("register")]
        
        public ActionResult RegisterAsync(RegisterRequest model)
        {
             _accountService.RegisterAsync(model);
            return Ok(new { message = "Registration successful" });
        }

        [AllowAnonymous]
        [HttpPost("login")]

        public ActionResult<AuthenticateResult> Authenticate([FromBody] AuthenticateRequest authenticateVm)
        {

            var authUser = _accountService.Authenticate(authenticateVm.Email, authenticateVm.Password);

            if (authUser == null) throw new Exception("User is unauthorized or credential does not match");

            var user = new Account()
            {

                FirstName = authUser.FirstName,
                LastName = authUser.LastName,
                Email = authUser.Email,
                Id = authUser.Id,
            };
            
            return new JsonResult(_accountService.GetToken(user));
        }
    }
}
