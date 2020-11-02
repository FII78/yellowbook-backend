using AutoMapper;
using FindIt.Backend.Models.Accounts;
using FindIt.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FindIt.Backend.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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

        public async Task<ActionResult> RegisterAsync(RegisterRequest model, int pageId)
        {

            await _accountService.RegisterAsync(model,pageId);

            return Ok(new { message = "Registration successful" });
        }

        [AllowAnonymous]
        [HttpPost("login")]

        public ActionResult<AuthenticateResult> Authenticate([FromBody] AuthenticateRequest authenticateVm)
        {

            var authUser = _accountService.Authenticate(authenticateVm.Email, authenticateVm.Password);

            if (authUser == null) return NotFound("user not registred");

            var user = _mapper.Map<Account>(authUser);
                
            return new JsonResult(_accountService.GetToken(user));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get()
        {
            var accounts = await _accountService.GetAllAsync();

            return Ok(accounts);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetbyId(string id)
        {
            var existingAcc = await _accountService.GetAsync(id);
            if (existingAcc == null)
                return NotFound("Account could not be found");

            return existingAcc;
        }
    }
}
