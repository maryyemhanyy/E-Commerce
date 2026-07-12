using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.Authentication
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = default!;
        [Required ,MinLength(5)]
        public string Password { get; set; } = default!;
        [Required]
        public string Username { get; set; } = default!;
        [Required]
        public string DisplayName { get; set; } = default!;
        [Phone]
        public string? PhoneNumber { get; set; } 


    }
}
