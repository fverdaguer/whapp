using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManyToMany.ViewModels
{
    public class PlayerBatVM
    {
        public int BatID { get; set; }
        public string batName { get; set; }
        public bool Assigned { get; set; }
    }
}