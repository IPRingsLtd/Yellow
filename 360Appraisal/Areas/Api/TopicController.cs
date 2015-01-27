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
    public class TopicController : ApiController
    {
        private AppContext db = new AppContext();

        // GET api/Topic
        public IQueryable<Topic> GetTopics()
        {
            return db.Topics;
        }

        // GET api/Topic/5
        [ResponseType(typeof(Topic))]
        public async Task<IHttpActionResult> GetTopic(string id)
        {
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }

            return Ok(topic);
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