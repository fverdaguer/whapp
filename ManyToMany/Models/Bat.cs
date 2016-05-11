using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManyToMany.Models
{
    public class Bat
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "You must enter the name of the Bat.")]
        [Display(Name = "Name of Bat")]
        [StringLength(50, ErrorMessage = "The Bat's name cannot be more than 50 characters.")]
        public string BatName { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}