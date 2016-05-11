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
    public class PlayerController : Controller
    {
        private MtoMEntities db = new MtoMEntities();

        // GET: /Player/
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.Team);
            return View(players.ToList());
        }

        // GET: /Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: /Player/Create
        public ActionResult Create()
        {
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName");
            var player = new Player();
            player.Bats = new List<Bat>();
            PopulateAssignedBatData(player);
            return View();
        }

        // POST: /Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PlayerFName,PlayerLName,DOB,TeamID")] Player player, string[] selectedOptions)
        {
            if (selectedOptions != null)
            {
                player.Bats = new List<Bat>();
                foreach (var bat in selectedOptions)
                {
                    var batToAdd = db.Bats.Find(int.Parse(bat));
                    player.Bats.Add(batToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName", player.TeamID);
            PopulateAssignedBatData(player);
            return View(player);
        }

        // GET: /Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Player player = db.Players.Find(id);
            Player player = db.Players
                .Include(p => p.Bats)
                .Where(i => i.ID == id)
                .Single();
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName", player.TeamID);
            PopulateAssignedBatData(player);
            return View(player);
        }
        private void PopulateAssignedBatData(Player player)
        {
            var allBats = db.Bats;
            var playBats = new HashSet<int>(player.Bats.Select(b => b.ID));
            var viewModelAvailable = new List<PlayerBatVM>();
            var viewModelSelected = new List<PlayerBatVM>();
            foreach (var bat in allBats)
            {
                if (playBats.Contains(bat.ID))
                {
                    viewModelSelected.Add(new PlayerBatVM
                    {
                        BatID = bat.ID,
                        batName = bat.BatName,
                        //Assigned = true
                    });
                }
                else
                {
                    viewModelAvailable.Add(new PlayerBatVM
                    {
                        BatID = bat.ID,
                        batName = bat.BatName,
                        //Assigned = false
                    });
                }
            }

            ViewBag.selOpts = new MultiSelectList(viewModelSelected, "BatID", "batName");
            ViewBag.availOpts = new MultiSelectList(viewModelAvailable, "BatID", "batName");
        }

        // POST: /Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedOptions)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var playerToUpdate = db.Players
                .Include(p => p.Bats)
                .Where(i => i.ID == id)
                .Single();
            if (TryUpdateModel(playerToUpdate, "",
                   new string[] { "ID", "PlayerFName", "PlayerLName", "DOB", "TeamID" }))
            {
                try
                {
                    UpdatePlayerBats(selectedOptions, playerToUpdate);

                    db.Entry(playerToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName", playerToUpdate.TeamID);
            PopulateAssignedBatData(playerToUpdate);
            return View(playerToUpdate);
        }
        private void UpdatePlayerBats(string[] selectedOptions, Player playerToUpdate)
        {
            if (selectedOptions == null)
            {
                playerToUpdate.Bats = new List<Bat>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var playerBats = new HashSet<int>
                (playerToUpdate.Bats.Select(b => b.ID));
            foreach (var bat in db.Bats)
            {
                if (selectedOptionsHS.Contains(bat.ID.ToString()))
                {
                    if (!playerBats.Contains(bat.ID))
                    {
                        playerToUpdate.Bats.Add(bat);
                    }
                }
                else
                {
                    if (playerBats.Contains(bat.ID))
                    {
                        playerToUpdate.Bats.Remove(bat);
                    }
                }
            }
        }

        // GET: /Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: /Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
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
