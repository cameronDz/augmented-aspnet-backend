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
    public class ExerciseTypesController : ApiController
    {
        private WorkoutContext db = new WorkoutContext();

        // GET: api/ExerciseTypes
        public IQueryable<ExerciseType> GetExerciseTypes()
        {
            return db.ExerciseTypes;
        }

        // GET: api/ExerciseTypes/5
        [ResponseType(typeof(ExerciseType))]
        public IHttpActionResult GetExerciseType(int id)
        {
            ExerciseType exerciseType = db.ExerciseTypes.Find(id);
            if (exerciseType == null)
            {
                return NotFound();
            }

            return Ok(exerciseType);
        }

        // PUT: api/ExerciseTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExerciseType(int id, ExerciseType exerciseType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exerciseType.ExercisTypeId)
            {
                return BadRequest();
            }

            exerciseType.Name = exerciseType.Name.ToUpper();
            if (ExerciseTypeNameAlreadyRegistered(exerciseType.Name))
            {
                return BadRequest("Exercise Type already exists");
            }

            db.Entry(exerciseType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseTypeExists(id))
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

        // POST: api/ExerciseTypes
        [ResponseType(typeof(ExerciseType))]
        public IHttpActionResult PostExerciseType(ExerciseType exerciseType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            exerciseType.Name = exerciseType.Name.ToUpper();
            if (ExerciseTypeNameAlreadyRegistered(exerciseType.Name))
            {
                return BadRequest("Exercise Type already exists");
            }

            db.ExerciseTypes.Add(exerciseType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = exerciseType.ExercisTypeId }, exerciseType);
        }

        // DELETE: api/ExerciseTypes/5
        [ResponseType(typeof(ExerciseType))]
        public IHttpActionResult DeleteExerciseType(int id)
        {
            ExerciseType exerciseType = db.ExerciseTypes.Find(id);
            if (exerciseType == null)
            {
                return NotFound();
            }

            db.ExerciseTypes.Remove(exerciseType);
            db.SaveChanges();

            return Ok(exerciseType);
        }

        private bool ExerciseTypeNameAlreadyRegistered(string name)
        {
            foreach (ExerciseType e in db.ExerciseTypes)
            {
                if (e.Name.Equals(name))
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

        private bool ExerciseTypeExists(int id)
        {
            return db.ExerciseTypes.Count(e => e.ExercisTypeId == id) > 0;
        }
    }
}