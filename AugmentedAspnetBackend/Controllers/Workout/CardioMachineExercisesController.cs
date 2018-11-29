using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using AugmentedAspnetBackend.DAL;
using AugmentedAspnetBackend.Models.Workout;
using ActionNameAttribute = System.Web.Http.ActionNameAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpOptionsAttribute = System.Web.Http.HttpOptionsAttribute;

namespace AugmentedAspnetBackend.Controllers.Workout
{
    public class CardioMachineExercisesController : ApiController
    {
        private WorkoutContext db = new WorkoutContext();

        [HttpOptions]
        [ResponseType(typeof(void))]
        public IHttpActionResult OptionsExercises()
        {
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "Content-Type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET,POST,OPTIONS");
            return Ok();
        }

        // GET: api/CardioMachineExercises
        public IQueryable<CardioMachineExercise> GetCardioMachineExercises()
        {
            return db.CardioMachineExercises.OrderByDescending(c => c.StartTime);
        }

        // GET: api/CardioMachineExercises?startDate=mmDDyyyy&endDate=mmDDyyy
        public IQueryable<CardioMachineExercise> GetCardioMachineExercises([FromUri]String startDate, [FromUri]String endDate)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            String format = "MMddyyyy";
            DateTime startTime = DateTime.ParseExact(startDate, format, provider);
            DateTime endTime = DateTime.ParseExact(endDate, format, provider);
            return db.CardioMachineExercises.Where(c => c.StartTime >= startTime && c.StartTime <= endTime).OrderBy(c => c.StartTime);
        }

        // GET: api/CardioMachineExercises/5
        [ResponseType(typeof(CardioMachineExercise))]
        public IHttpActionResult GetCardioMachineExercise(int id)
        {
            CardioMachineExercise cardioMachineExercise = db.CardioMachineExercises.Find(id);
            if (cardioMachineExercise == null)
            {
                return NotFound();
            }
            return Ok(cardioMachineExercise);
        }



        // PUT: api/CardioMachineExercises/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCardioMachineExercise(int id, CardioMachineExercise cardioMachineExercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cardioMachineExercise.CardioMachineExerciseId)
            {
                return BadRequest();
            }

            db.Entry(cardioMachineExercise).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardioMachineExerciseExists(id))
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

        // POST: api/CardioMachineExercises
        [ResponseType(typeof(CardioMachineExercise))]
        public IHttpActionResult PostCardioMachineExercise([FromBody]CardioMachineExercise cardioMachineExercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.CardioMachineExercises.Add(cardioMachineExercise);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cardioMachineExercise.CardioMachineExerciseId }, cardioMachineExercise);
        }

        // DELETE: api/CardioMachineExercises/5
        [ResponseType(typeof(CardioMachineExercise))]
        public IHttpActionResult DeleteCardioMachineExercise(int id)
        {
            CardioMachineExercise cardioMachineExercise = db.CardioMachineExercises.Find(id);
            if (cardioMachineExercise == null)
            {
                return NotFound();
            }

            db.CardioMachineExercises.Remove(cardioMachineExercise);
            db.SaveChanges();

            return Ok(cardioMachineExercise);
        }

        [HttpGet]
        public HttpResponseMessage DownloadCardioMachineExercieCsv(String csv)
        {
            var csvString = FullCardioMachineExerciseListCsv();
            String fileName = CsvCardioMachineExerciseFileName();
            var result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StringContent(csvString, Encoding.UTF8, "text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };

            return result;
        }

        private string FullCardioMachineExerciseListCsv()
        {
            StringBuilder csv = new StringBuilder();
            IEnumerable<CardioMachineExercise> list = db.CardioMachineExercises.OrderBy(c => c.StartTime);
            csv = WriteLineInCsvStringBuilder(csv,
                    "Id",
                    "Machine Type",
                    "Start Time",
                    "Duration Seconds",
                    "Distance Miles",
                    "User Name",
                    "Comment");
            foreach (CardioMachineExercise row in list)
            {
                WriteLineInCsvStringBuilder(csv,
                    row.CardioMachineExerciseId.ToString(),
                    row.MachineType,
                    row.StartTime.ToString(),
                    row.DurationSeconds.ToString(),
                    row.DistanceMiles.ToString(),
                    row.UserName,
                    row.Comment);
            }
            return csv.ToString();
        }

        private StringBuilder WriteLineInCsvStringBuilder(StringBuilder csv, String columnOne, String columnTwo, 
            String columnThree, String columnFour, String columnFive, String columnSix, String columnSeven)
        {
            csv.Append(columnOne).Append(',');
            csv.Append(columnTwo).Append(',');
            csv.Append(columnThree).Append(',');
            csv.Append(columnFour).Append(',');
            csv.Append(columnFive).Append(',');
            csv.Append(columnSix).Append(',');
            csv.Append(columnSeven).Append(',');
            csv.AppendLine();
            return csv;
        }

        private String CsvCardioMachineExerciseFileName()
        {
            return "cardioMachineExercise-" + DateTime.Now.ToUniversalTime() + "-GMT.csv";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardioMachineExerciseExists(int id)
        {
            return db.CardioMachineExercises.Count(e => e.CardioMachineExerciseId == id) > 0;
        }
    }
}