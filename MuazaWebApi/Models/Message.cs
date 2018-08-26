using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MuazaAngular.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int phone { get; set; }
        [Required(ErrorMessage ="Please ensure you have included a valid email!")]
        [MaxLength(500)]
        public string email { get; set; }
        [Required(ErrorMessage ="please ensure you have included a message!")]
        [MaxLength(500)]
        
        public string content { get; set; }
    }
}
