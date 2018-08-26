using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MuazaAngular.Models
{
    public class Project
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Please provide a name")]
        [MaxLength(100, ErrorMessage = "Max length exceeded")]
        public string name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string month { get; set; }
        [Required(ErrorMessage = "Please provide a category")]
        public string category { get; set; }
        public string location { get; set; }
        [Required(ErrorMessage ="Please provide a description")]
        [MaxLength(3000, ErrorMessage = "Max length exceeded")]
        public string description { get; set; }
        public string images { get; set; }
        public string _url { get; set; }
       
    }
    public class _Project
    {
           public Project project { get; set;}
          public List<string> images { get; set; }
        
    }

}
