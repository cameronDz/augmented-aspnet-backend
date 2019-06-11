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
using AugmentedAspnetBackend.Models.ApiHelpers;
using AugmentedAspnetBackend.Models.Workout;

namespace AugmentedAspnetBackend.Controllers.Workout
{
    public class CaffeineNutrientIntakesController : ApiController
    {
        private WorkoutContext context;
        public CaffeineNutrientIntakesController()
        {
            context = new WorkoutContext();
        }
        public CaffeineNutrientIntakesController(WorkoutContext context)
        {
            this.context = context;
        }

        [HttpOptions]
        [ResponseType(typeof(void))]
        public HttpResponseMessage OptionsExercises()
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            message.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS");
            return message;
        }

        // GET: api/CaffeineNutrientIntakes
        [HttpGet]
        public HttpResponseMessage GetCaffeineNutrientIntake()
        {
            var source = context.CaffeineNutrientIntakes.OrderByDescending(c => c.IntakeTime);
            var metaData = new ApiMetaDataModel() { _totalRecords = source.Count(),  _totalPages = 1, _currentPage = 1 };
            var payload = new ApiResponseModel() { Data = source, Meta = metaData, Links = null };
            var response = Request.CreateResponse(HttpStatusCode.OK, payload);
            return response;
        }
        
        // POST: api/CaffeineNutrientIntakes
        [ResponseType(typeof(CaffeineNutrientIntake))]
        public IHttpActionResult PostCaffeineNutrientIntake(CaffeineNutrientIntake caffeineNutrientIntake)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.CaffeineNutrientIntakes.Add(caffeineNutrientIntake);
            context.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = caffeineNutrientIntake.CaffeineNutrientIntakeId }, caffeineNutrientIntake);
        }

        // DELETE: api/CaffeineNutrientIntakes/5
        [ResponseType(typeof(CaffeineNutrientIntake))]
        public IHttpActionResult DeleteCaffeineNutrientIntake(int id)
        {
            CaffeineNutrientIntake caffeineNutrientIntake = context.CaffeineNutrientIntakes.Find(id);
            if (caffeineNutrientIntake == null)
            {
                return NotFound();
            }

            context.CaffeineNutrientIntakes.Remove(caffeineNutrientIntake);
            context.SaveChanges();

            return Ok(caffeineNutrientIntake);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CaffeineNutrientIntakeExists(int id)
        {
            return context.CaffeineNutrientIntakes.Count(e => e.CaffeineNutrientIntakeId == id) > 0;
        }
    }
}
