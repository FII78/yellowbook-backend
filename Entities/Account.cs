using FindIt.DataAccess.Entities;
using System;

namespace FindIt.Backend.Entities
{
    public class Account:BaseEntity
    {
       
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool AcceptTerms { get; set; }
        public string Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
