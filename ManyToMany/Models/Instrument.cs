namespace Whapp.Models
{
    using System.Collections.Generic;

    public class Instrument
    {
        public int InstrumentID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}