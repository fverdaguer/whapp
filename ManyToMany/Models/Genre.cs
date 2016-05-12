namespace Whapp.Models
{
    using System.Collections.Generic;

    public class Genre
    {
        public int GenreID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}