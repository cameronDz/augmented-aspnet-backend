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
using augmented_aspnet_backend.DAL;
using augmented_aspnet_backend.Models.Workout;

namespace augmented_aspnet_backend.Controllers.Workout
{
    public class RoutineSetsController : ApiController
    {
        private WorkoutContext db = new WorkoutContext();

        // GET: api/RoutineSets
        public IQueryable<RoutineSet> GetRoutineSets()
        {
            return db.RoutineSets;
        }

        // GET: api/RoutineSets/5
        [ResponseType(typeof(RoutineSet))]
        public IHttpActionResult GetRoutineSet(int id)
        {
            RoutineSet routineSet = db.RoutineSets.Find(id);
            if (routineSet == null)
            {
                return NotFound();
            }

            return Ok(routineSet);
        }

        // PUT: api/RoutineSets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoutineSet(int id, RoutineSet routineSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != routineSet.RoutineSetId)
            {
                return BadRequest();
            }

            db.Entry(routineSet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoutineSetExists(id))
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

        // POST: api/RoutineSets
        [ResponseType(typeof(RoutineSet))]
        public IHttpActionResult PostRoutineSet(RoutineSet routineSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoutineSets.Add(routineSet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = routineSet.RoutineSetId }, routineSet);
        }

        // DELETE: api/RoutineSets/5
        [ResponseType(typeof(RoutineSet))]
        public IHttpActionResult DeleteRoutineSet(int id)
        {
            RoutineSet routineSet = db.RoutineSets.Find(id);
            if (routineSet == null)
            {
                return NotFound();
            }

            db.RoutineSets.Remove(routineSet);
            db.SaveChanges();

            return Ok(routineSet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoutineSetExists(int id)
        {
            return db.RoutineSets.Count(e => e.RoutineSetId == id) > 0;
        }
    }
}