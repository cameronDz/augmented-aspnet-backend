using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AugmentedAspnetBackend.DAL;
using AugmentedAspnetBackend.Models.Workout;

namespace AugmentedAspnetBackend.Controllers.Workout
{
    public class SetsController : ApiController
    {
        private WorkoutContext db = new WorkoutContext();

        // GET: api/Sets
        public IQueryable<Set> GetSets()
        {
            return db.Sets;
        }

        // GET: api/Sets/5
        [ResponseType(typeof(Set))]
        public IHttpActionResult GetSet(int id)
        {
            Set set = db.Sets.Find(id);
            if (set == null)
            {
                return NotFound();
            }

            return Ok(set);
        }

        // PUT: api/Sets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSet(int id, [FromBody]Set set)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != set.SetId)
            {
                return BadRequest();
            }

            db.Entry(set).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetExists(id))
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

        // POST: api/Sets
        [ResponseType(typeof(Set))]
        public IHttpActionResult PostSet([FromBody]Set set)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sets.Add(set);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = set.SetId }, set);
        }

        // DELETE: api/Sets/5
        [ResponseType(typeof(Set))]
        public IHttpActionResult DeleteSet(int id)
        {
            Set set = db.Sets.Find(id);
            if (set == null)
            {
                return NotFound();
            }

            db.Sets.Remove(set);
            db.SaveChanges();

            return Ok(set);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SetExists(int id)
        {
            return db.Sets.Count(e => e.SetId == id) > 0;
        }
    }
}