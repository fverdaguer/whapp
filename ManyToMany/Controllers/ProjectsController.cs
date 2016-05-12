using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManyToMany.Models;
using ManyToMany.DAL;
using ManyToMany.ViewModels;
using System.Data.Entity.Infrastructure;

namespace ManyToMany.Controllers
{
    public class ProjectsController : Controller
    {
        private MtoMEntities db = new MtoMEntities();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            var project = new Project();

            project.Genres = new List<Genre>();
            project.Instruments = new List<Instrument>();

            PopulateAssignedGenreData(project);
            PopulateAssignedInstrumentData(project);

            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,Title,FacebookUrl,SoundcloudUrl,LocationName,LocationGooglePlaceId,LocationLatitude,LocationLongitude")] Project project, string[] selectedOptions, string[] selectedOptionsInst)
        {
            if (selectedOptions != null)
            {
                project.Genres = new List<Genre>();
                foreach (var genre in selectedOptions)
                {
                    var genreToAdd = db.Genres.Find(int.Parse(genre));
                    project.Genres.Add(genreToAdd);
                }
            }

            if (selectedOptionsInst != null)
            {
                project.Instruments = new List<Instrument>();
                foreach (var instrument in selectedOptionsInst)
                {
                    var instrumentToAdd = db.Instruments.Find(int.Parse(instrument));
                    project.Instruments.Add(instrumentToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignedGenreData(project);
            PopulateAssignedInstrumentData(project);

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Project project = db.Projects.Find(id);
            Project project = db.Projects
                                    .Include(p => p.Genres)
                                    .Include(p => p.Instruments)
                                    .Where(i => i.ProjectID == id)
                                    .Single();

            if (project == null)
            {
                return HttpNotFound();
            }

            PopulateAssignedGenreData(project);
            PopulateAssignedInstrumentData(project);

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedOptions, string[] selectedOptionsInst)
        {
           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var projectToUpdate = db.Projects
                .Include(p => p.Genres)
                .Include(p => p.Instruments)
                .Where(i => i.ProjectID == id)
                .Single();

            if (TryUpdateModel(projectToUpdate, "",
                   new string[] { "ProjectID", "Title", "FacebookUrl", "SoundcloudUrl", "LocationName", "LocationGooglePlaceId", "LocationLatitude", "LocationLongitude" }))
            {
                try
                {
                    UpdateProjectGenres(selectedOptions, projectToUpdate);
                    UpdateProjectInstruments(selectedOptionsInst, projectToUpdate);

                    db.Entry(projectToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            
            PopulateAssignedGenreData(projectToUpdate);
            PopulateAssignedInstrumentData(projectToUpdate);

            return View(projectToUpdate);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void UpdateProjectGenres(string[] selectedOptions, Project projectToUpdate)
        {
            if (selectedOptions == null)
            {
                projectToUpdate.Genres = new List<Genre>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var projectGenres = new HashSet<int>
                (projectToUpdate.Genres.Select(b => b.GenreID));
            foreach (var genre in db.Genres)
            {
                if (selectedOptionsHS.Contains(genre.GenreID.ToString()))
                {
                    if (!projectGenres.Contains(genre.GenreID))
                    {
                        projectToUpdate.Genres.Add(genre);
                    }
                }
                else
                {
                    if (projectGenres.Contains(genre.GenreID))
                    {
                        projectToUpdate.Genres.Remove(genre);
                    }
                }
            }
        }

        private void UpdateProjectInstruments(string[] selectedOptionsInst, Project projectToUpdate)
        {
            if (selectedOptionsInst == null)
            {
                projectToUpdate.Instruments = new List<Instrument>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptionsInst);
            var projectInstruments = new HashSet<int>
                (projectToUpdate.Instruments.Select(b => b.InstrumentID));
            foreach (var instrument in db.Instruments)
            {
                if (selectedOptionsHS.Contains(instrument.InstrumentID.ToString()))
                {
                    if (!projectInstruments.Contains(instrument.InstrumentID))
                    {
                        projectToUpdate.Instruments.Add(instrument);
                    }
                }
                else
                {
                    if (projectInstruments.Contains(instrument.InstrumentID))
                    {
                        projectToUpdate.Instruments.Remove(instrument);
                    }
                }
            }
        }

        private void PopulateAssignedGenreData(Project project)
        {
            var allGenres = db.Genres;
            var projectGenres = new HashSet<int>(project.Genres.Select(b => b.GenreID));
            var viewModelAvailable = new List<ProjectGenreViewModel>();
            var viewModelSelected = new List<ProjectGenreViewModel>();
            foreach (var genre in allGenres)
            {
                if (projectGenres.Contains(genre.GenreID))
                {
                    viewModelSelected.Add(new ProjectGenreViewModel
                    {
                        GenreID = genre.GenreID,
                        GenreName = genre.Name,
                        //Assigned = true
                    });
                }
                else
                {
                    viewModelAvailable.Add(new ProjectGenreViewModel
                    {
                        GenreID = genre.GenreID,
                        GenreName = genre.Name,
                        //Assigned = false
                    });
                }
            }

            ViewBag.selOpts = new MultiSelectList(viewModelSelected, "GenreID", "GenreName");
            ViewBag.availOpts = new MultiSelectList(viewModelAvailable, "GenreID", "GenreName");
        }

        private void PopulateAssignedInstrumentData(Project project)
        {
            var allInstruments = db.Instruments;
            var projectInstruments = new HashSet<int>(project.Instruments.Select(b => b.InstrumentID));
            var viewModelAvailable = new List<ProjectInstrumentViewModel>();
            var viewModelSelected = new List<ProjectInstrumentViewModel>();
            foreach (var instrument in allInstruments)
            {
                if (projectInstruments.Contains(instrument.InstrumentID))
                {
                    viewModelSelected.Add(new ProjectInstrumentViewModel
                    {
                        InstrumentID = instrument.InstrumentID,
                        InstrumentName = instrument.Name,
                        //Assigned = true
                    });
                }
                else
                {
                    viewModelAvailable.Add(new ProjectInstrumentViewModel
                    {
                        InstrumentID = instrument.InstrumentID,
                        InstrumentName = instrument.Name,
                        //Assigned = false
                    });
                }
            }

            ViewBag.selOptsInst = new MultiSelectList(viewModelSelected, "InstrumentID", "InstrumentName");
            ViewBag.availOptsInst = new MultiSelectList(viewModelAvailable, "InstrumentID", "InstrumentName");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
