using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _360Appraisal.Models;

namespace _360Appraisal.Controllers
{
    public class SectionsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: /Sections/
        public async Task<ActionResult> Index()
        {
            return View(await db.Sections.ToListAsync());
        }

        // GET: /Sections/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: /Sections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Description")] SectionViewModel section)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(new Section
                {
                    Description = section.Description
                });

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(section);
        }

        // GET: /Sections/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(new SectionEditViewModel(section));
        }

        // POST: /Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Key,Description")] SectionEditViewModel section)
        {
            if (ModelState.IsValid)
            {
                var editSection = await db.Sections.FindAsync(section.Key);

                if(editSection == null)
                {
                    return HttpNotFound();
                }

                editSection.Description = section.Description;

                db.Entry(editSection).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(section);
        }

        // GET: /Sections/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: /Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Section section = await db.Sections.FindAsync(id);
            db.Sections.Remove(section);
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
