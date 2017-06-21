using System.ComponentModel.DataAnnotations;
using System;
namespace wedding_planner.Models
{
    public class Guest: BaseEntity
    {   
        public int GuestId {get; set;}
        public int WeddingId {get;set;}
        public Wedding Wedding {get; set;}
        public int UserId {get;set;}
        public User User {get; set;}
     }
}
