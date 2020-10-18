using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindIt.Backend.Models.Accounts
{
    public class AuthenticateResult
    {
        public string Token { get; set; }
        public DateTime? Expiration { get; set; } 
        public string Email { get; set; }
        
    }
}
