using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManyToMany.Models;
using ManyToMany.DAL;

namespace ManyToMany.Controllers
{
    public class BatController : Controller
    {
        private MtoMEntities db = new MtoMEntities();

        // GET: /Bat/
        public async Task<ActionResult> Index()
        {
            return View(await db.Bats.ToListAsync());
        }

        // GET: /Bat/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bat bat = await db.Bats.FindAsync(id);
            if (bat == null)
            {
                return HttpNotFound();
            }
            return View(bat);
        }

        // GET: /Bat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Bat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,BatName")] Bat bat)
        {
            if (ModelState.IsValid)
            {
                db.Bats.Add(bat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bat);
        }

        // GET: /Bat/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bat bat = await db.Bats.FindAsync(id);
            if (bat == null)
            {
                return HttpNotFound();
            }
            return View(bat);
        }

        // POST: /Bat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,BatName")] Bat bat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bat);
        }

        // GET: /Bat/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bat bat = await db.Bats.FindAsync(id);
            if (bat == null)
            {
                return HttpNotFound();
            }
            return View(bat);
        }

        // POST: /Bat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bat bat = await db.Bats.FindAsync(id);
            db.Bats.Remove(bat);
            await db.SaveChangesAsync();
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
