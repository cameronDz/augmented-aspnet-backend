using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AugmentedAspnetBackend.DAL;
using AugmentedAspnetBackend.Models.Workout;

namespace AugmentedAspnetBackend.Controllers.Workout
{
    public class ExercisesController : ApiController
    {
        private WorkoutContext db = new WorkoutContext();

        // GET: api/Exercises
        public IQueryable<Exercise> GetExercises()
        {
            return db.Exercises;
        }

        // GET: api/Exercises/5
        [ResponseType(typeof(Exercise))]
        public IHttpActionResult GetExercise(int id)
        {
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        // PUT: api/Exercises/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExercise(int id, [FromBody]Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exercise.ExerciseId)
            {
                return BadRequest();
            }

            exercise.Name = exercise.Name.ToUpper();

            if(ExerciseNameAlreadyRegistered(exercise.Name))
            {
                return BadRequest("Exercise already exists");
            }

            db.Entry(exercise).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(id))
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

        // POST: api/Exercises
        [ResponseType(typeof(Exercise))]
        public IHttpActionResult PostExercise([FromBody]Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            exercise.Name = exercise.Name.ToUpper();

            if (ExerciseNameAlreadyRegistered(exercise.Name))
            {
                return BadRequest("Exercise already exists");
            }
            db.Exercises.Add(exercise);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = exercise.ExerciseId }, exercise);
        }

        // DELETE: api/Exercises/5
        [ResponseType(typeof(Exercise))]
        public IHttpActionResult DeleteExercise(int id)
        {
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return NotFound();
            }

            db.Exercises.Remove(exercise);
            db.SaveChanges();

            return Ok(exercise);
        }

        private bool ExerciseNameAlreadyRegistered(string name)
        {
            foreach(Exercise e in db.Exercises)
            {
                if(e.Name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExerciseExists(int id)
        {
            return db.Exercises.Count(e => e.ExerciseId == id) > 0;
        }
    }
}