using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace wedding_planner.Models

{
    public class User : BaseEntity
    {
        public int UserId {get;set;}
        public string Fname {get;set;}

        public string Lname {get;set;}
        public string Email {get;set;}
        public string Password {get; set;}

        public DateTime CreatedAt {get; set;}

        public DateTime UpdatedAt {get; set;}
        public List<Guest> Guests {get; set;}
        public List<Wedding> Weddings {get; set;}
        public User()
        {
            Guests = new List<Guest>();
            Weddings = new List<Wedding>();
        }

    }
}