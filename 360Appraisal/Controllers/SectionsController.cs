using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using _360Appraisal.Models;

namespace _360Appraisal.Controllers
{
    public class SectionsController : ApiController
    {
        private AppContext db = new AppContext();

        // GET: api/Sections
        public IQueryable<Section> GetSections()
        {
            return db.Sections;
        }

        // GET: api/Sections/5
        [ResponseType(typeof(Section))]
        public async Task<IHttpActionResult> GetSection(string id)
        {
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            return Ok(section);
        }

        // PUT: api/Sections/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSection(string id, Section section)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != section.Key)
            {
                return BadRequest();
            }

            db.Entry(section).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Sections
        [ResponseType(typeof(Section))]
        public async Task<IHttpActionResult> PostSection(Section section)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }                     

            db.Sections.Add(section);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SectionExists(section.Key))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = section.Key }, section);
        }

        // DELETE: api/Sections/5
        [ResponseType(typeof(Section))]
        public async Task<IHttpActionResult> DeleteSection(string id)
        {
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            db.Sections.Remove(section);
            await db.SaveChangesAsync();

            return Ok(section);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SectionExists(string id)
        {
            return db.Sections.Count(e => e.Key == id) > 0;
        }
    }
}