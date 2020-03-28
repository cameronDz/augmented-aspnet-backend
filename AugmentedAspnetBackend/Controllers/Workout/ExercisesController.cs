using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AugmentedAspnetBackend.DAL;
using AugmentedAspnetBackend.Models.Workout;
using Newtonsoft.Json.Linq;

namespace AugmentedAspnetBackend.Controllers.Workout
{
    /// <summary>
    /// Used to track exercise names/descriptions
    /// </summary>
    public class ExercisesController : ApiController
    {
        private WorkoutContext db = new WorkoutContext();

        /// <summary>
        /// Give list of Access Control Headers and Methods allowed by the API
        /// </summary>
        [HttpOptions]
        [ResponseType(typeof(void))]
        public IHttpActionResult OptionsExercises()
        {
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "Content-Type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET,POST,OPTIONS");
            return Ok();
        }

        // GET: api/Exercises
        /// <summary>
        /// Returns a list of all Exercise objects. (Does Not Support pagination)
        /// </summary>
        public IQueryable<Exercise> GetExercises()
        {
            return db.Exercises;
        }

        // GET: api/Exercises/5
        /// <summary>
        /// Returns a specific Exercise record based on the index in the URL.
        /// </summary>
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
        /// <summary>
        /// Updates a specific Exercise record based on the index in URL and the payload in the body of the request.
        /// </summary>
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
        // POST: api/CardioMachineExercises
        /// <summary>
        /// Create a new Exercise record based on the payload in the body of the request.
        /// </summary>
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
        /// <summary>
        /// Delete a specific Exercise record based on the index in the URL.
        /// </summary>
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
