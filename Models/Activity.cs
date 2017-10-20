using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace dojoActivity_project.Models
{
    public class Activity
    {
        public int UserID { get; set; }
        public string Coordinator { get; set; }
        public int ActivityID { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }
        public string Duration { get; set; }

        [Required]
        [MyDate]
        public DateTime Date { get; set; }

        [Required]
        public string Time { get; set; }
        public int Participants { get; set; }


        public List<Guest> Guest { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public Activity(){

            Guest = new List<Guest>();
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
}