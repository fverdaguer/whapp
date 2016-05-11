using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManyToMany.ViewModels
{
    public class ProjectGenreViewModel
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }
        public bool Assigned { get; set; }
    }
}