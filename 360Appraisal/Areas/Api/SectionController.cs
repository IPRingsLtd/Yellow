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

namespace _360Appraisal.Areas.Api
{
    public class SectionController : ApiController
    {
        private AppContext db = new AppContext();

        // GET api/Section
        public IQueryable<Section> GetSections()
        {
            return db.Sections;
        }

        // GET api/Section/5
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