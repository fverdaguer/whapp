using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManyToMany.Models
{
    public class Team
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "You must enter the team's name.")]
        [Display(Name = "Team Name")]
        [StringLength(50, ErrorMessage = "Team name cannot be more than 50 characters.")]
        public string TeamName { get; set; }

        public virtual ICollection<Player> Players { get; set; }

    }
}