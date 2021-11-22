using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaMVC.Models
{
    public class Login
    {
        [Required]
        public string LoginEmail { get; set; }
        [Required]
        public string LoginPassword { get; set; }
    }
}
