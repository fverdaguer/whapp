using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManyToMany.Models
{
    public class Instrument
    {
        public int InstrumentID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}