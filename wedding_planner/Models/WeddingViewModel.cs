using System.ComponentModel.DataAnnotations;
using System;
namespace wedding_planner.Models
{
    public class WeddingViewModel: BaseEntity
    {
        [Required(ErrorMessage = "Wedder One is required")]
        [MinLength(2, ErrorMessage = "Wedder One must have two letters")]
        public string wedder_one {get;set;}
        [Required(ErrorMessage = "Wedder Two is required")]
        [MinLength(2, ErrorMessage = "Wedder Two must have two letters")]
        public string wedder_two {get;set;}
        [Required(ErrorMessage = "Date is required")]
        public DateTime date {get;set;}
        [Required (ErrorMessage = "Address is required")]
        public string address {get; set;}
    }
}