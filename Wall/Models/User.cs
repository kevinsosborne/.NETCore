using System.ComponentModel.DataAnnotations;
using System;
namespace Wall.Models
{
    
    public class User : BaseEntity
    
    {
        
        [Required] 
        [MinLength(2)]    
        public string fname { get; set; }
     
        [Required] 
        [MinLength(2)]
        public string lname { get; set; }
    

        [Required]
        [EmailAddress]
        public string email { get; set; }
        
        
        
        
        [Required] 
        [DataType(DataType.Password)]
        [MinLength(8)]

        public string password { get; set; }


        [Required] 
        [Compare("password", ErrorMessage = "Password and confirmation must match.")]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; }

    }
}

