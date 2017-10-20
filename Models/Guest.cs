using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace dojoActivity_project.Models
{
    public class Guest
    {
        public int GuestID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int ActivityID { get; set; }
        public Activity Activity { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        public Guest(){

            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
}