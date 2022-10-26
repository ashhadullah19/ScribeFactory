using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Common
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "The First Name field is required.")]
        [StringLength(100, ErrorMessage = "First Name length can't be more than 100.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Last tName field is required.")]
        [StringLength(100, ErrorMessage = "Last Name length can't be more than 100.")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(100)]
        public string Zip { get; set; }
    }
}
