﻿using System;
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
    public class RoutinesController : ApiController
    {
        private WorkoutContext db = new WorkoutContext();

        // GET: api/Routines
        public IQueryable<Routine> GetRoutines()
        {
            return db.Routines;
        }

        // GET: api/Routines/5
        [ResponseType(typeof(Routine))]
        public IHttpActionResult GetRoutine(int id)
        {
            Routine routine = db.Routines.Find(id);
            if (routine == null)
            {
                return NotFound();
            }

            return Ok(routine);
        }

        // PUT: api/Routines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoutine(int id, [FromBody]Routine routine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != routine.RoutineId)
            {
                return BadRequest();
            }

            db.Entry(routine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoutineExists(id))
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

        // POST: api/Routines
        [ResponseType(typeof(Routine))]
        public IHttpActionResult PostRoutine([FromBody]Routine routine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Routines.Add(routine);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = routine.RoutineId }, routine);
        }

        // DELETE: api/Routines/5
        [ResponseType(typeof(Routine))]
        public IHttpActionResult DeleteRoutine(int id)
        {
            Routine routine = db.Routines.Find(id);
            if (routine == null)
            {
                return NotFound();
            }

            db.Routines.Remove(routine);
            db.SaveChanges();

            return Ok(routine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoutineExists(int id)
        {
            return db.Routines.Count(e => e.RoutineId == id) > 0;
        }
    }
}