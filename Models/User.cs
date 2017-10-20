using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace dojoActivity_project.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<Guest> Guest { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public User(){

            Guest = new List<Guest>();
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
    
}