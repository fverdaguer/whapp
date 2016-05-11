using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManyToMany.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string FacebookUrl { get; set; }
        public string SoundcloudUrl { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Instrument> Instruments { get; set; }
    }
}