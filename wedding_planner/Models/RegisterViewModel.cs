using System.ComponentModel.DataAnnotations;
using System;
namespace wedding_planner.Models
{
    public class RegisterViewModel: BaseEntity
    {
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must be all letters")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 letters")]
        public string fname {get;set;}
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name must be all letters")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 letters")]
        public string lname {get;set;}
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email {get;set;}
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [DataType(DataType.Password)]
        public string password {get; set;}
        [Required(ErrorMessage = "Confirmation password is required")]
        [Compare("password", ErrorMessage = "The password and confirmation must match!")]
        public string confirm_password {get; set;}
    }
}