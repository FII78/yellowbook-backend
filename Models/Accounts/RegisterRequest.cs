using FindIt.Backend.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Backend.Models.Accounts
{
    public class RegisterRequest
    {
        [Required (ErrorMessage = "Username is required")]
        [StringLength(4)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(30, ErrorMessage ="First name can not be greater than 30 charachters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(30, ErrorMessage = "Last name can not be greater than 30 charachters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [EmailAddress(ErrorMessage ="Invalid email format")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Username is required")]
        [MinLength(6,ErrorMessage ="Password must be at least 8 charachters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Must much with your password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Username is required")]

        [Range(typeof(bool), "true", "true")]
        public bool AcceptTerms { get; set; }
    }
}
