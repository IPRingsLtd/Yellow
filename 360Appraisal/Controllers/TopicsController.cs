using _360Appraisal.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace _360Appraisal.Controllers
{
    public class TopicsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: /Topics/
        public async Task<ActionResult> Index()
        {
            return View(await db.Topics.Include(x => x.Section).ToListAsync());
        }

        // GET: /Topics/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: /Topics/Create
        public async Task<ActionResult> Create()
        {
            var Sections = await db.Sections.ToListAsync();

            return View(new TopicViewModel
            {
                Sections = new SelectList(Sections, "Key", "Description")
            });
        }

        // POST: /Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Description,Section")] TopicViewModel topic)
        {
            var Sections = await db.Sections.ToListAsync();

            if (ModelState.IsValid)
            {
                var section = await db.Sections.FindAsync(topic.Section);

                if (section == null)
                {
                    return HttpNotFound();
                }

                db.Topics.Add(new Topic
                {
                    Description = topic.Description,
                    Section = section
                });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            topic.Sections = new SelectList(Sections, "Key", "Description", topic.Section);

            return View(topic);
        }

        // GET: /Topics/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Sections = await db.Sections.ToListAsync();

            Topic topic = await db.Topics.FindAsync(id);

            if (topic == null)
            {
                return HttpNotFound();
            }

            return View(new TopicEditViewModel(topic)
            {
                Sections = new SelectList(Sections, "Key", "Description", topic.Section.Key)
            });
        }

        // POST: /Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Key,Description,Section")] TopicEditViewModel topic)
        {
            var Sections = await db.Sections.ToListAsync();

            if (ModelState.IsValid)
            {
                var editTopic = await db.Topics.Include(x => x.Section).Where(x => x.Key == topic.Key).FirstOrDefaultAsync();

                if (editTopic == null)
                {
                    return HttpNotFound();
                }

                if (editTopic.Section.Key != topic.Section)
                {
                    var section = await db.Sections.FindAsync(topic.Section);

                    if (section == null)
                    {
                        return HttpNotFound();
                    }

                    editTopic.Section = section;
                }

                editTopic.Description = topic.Description;

                db.Entry(editTopic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            topic.Sections = new SelectList(Sections, "Key", "Description", topic.Section);

            return View(topic);
        }

        // GET: /Topics/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: /Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Topic topic = await db.Topics.FindAsync(id);
            db.Topics.Remove(topic);
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
