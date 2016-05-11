using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManyToMany.Models
{
    public class Player
    {
        public int ID { get; set; }

        [Display(Name = "Player ")]
        public string FullName
        {
            get
            {
                return PlayerFName + " " + PlayerLName;
            }
        }
        [Required(ErrorMessage="You must enter the player's first name.")]
        [Display(Name="First Name")]
        [StringLength(30,ErrorMessage="First name cannot be more than 30 characters.")]
        public string PlayerFName { get; set; }

        [Required(ErrorMessage = "You must enter the player's last name.")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters.")]
        public string PlayerLName { get; set; }

        [Required(ErrorMessage = "You must enter the player's date of birth.")]
        [DataType(DataType.Date,ErrorMessage="DOB must be a valid date.")]
        public DateTime DOB { get; set; }

        public int TeamID { get; set; }

        public Team Team { get; set; }

        public virtual ICollection<Bat> Bats { get; set; }

    }
}