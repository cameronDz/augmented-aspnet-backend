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
        private WorkoutContext context;

        public CardioMachineExercisesController()
        {
            context = new WorkoutContext();
        }
        public CardioMachineExercisesController(WorkoutContext context)
        {
            this.context = context;
        }

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
            return context.CardioMachineExercises.OrderByDescending(c => c.StartTime);
        }

        // GET: api/CardioMachineExercises?startDate=mmDDyyyy&endDate=mmDDyyy
        public IQueryable<CardioMachineExercise> GetCardioMachineExercises([FromUri]String startDate, [FromUri]String endDate)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            String format = "MMddyyyy";
            DateTime startTime = DateTime.ParseExact(startDate, format, provider);
            DateTime endTime = DateTime.ParseExact(endDate, format, provider);
            return context.CardioMachineExercises.Where(c => c.StartTime >= startTime && c.StartTime <= endTime).OrderBy(c => c.StartTime);
        }

        // GET: api/CardioMachineExercises/5
        [ResponseType(typeof(CardioMachineExercise))]
        public IHttpActionResult GetCardioMachineExercise(int id)
        {
            CardioMachineExercise cardioMachineExercise = context.CardioMachineExercises.Find(id);
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
            context.Entry(cardioMachineExercise).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
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
            context.CardioMachineExercises.Add(cardioMachineExercise);
            context.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = cardioMachineExercise.CardioMachineExerciseId }, cardioMachineExercise);
        }

        // DELETE: api/CardioMachineExercises/5
        [ResponseType(typeof(CardioMachineExercise))]
        public IHttpActionResult DeleteCardioMachineExercise(int id)
        {
            CardioMachineExercise cardioMachineExercise = context.CardioMachineExercises.Find(id);
            if (cardioMachineExercise == null)
            {
                return NotFound();
            }
            context.CardioMachineExercises.Remove(cardioMachineExercise);
            context.SaveChanges();
            return Ok(cardioMachineExercise);
        }

        [HttpGet]
        public HttpResponseMessage DownloadCardioMachineExercieCsv(String csv)
        {
            IEnumerable<CardioMachineExercise> list = context.CardioMachineExercises.OrderBy(c => c.StartTime);
            var csvString = FullCardioMachineExerciseListCsv(list);
            String fileName = CsvCardioMachineExerciseFileName();
            var result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StringContent(csvString, Encoding.UTF8, "text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            return result;
        }

        protected string FullCardioMachineExerciseListCsv(IEnumerable<CardioMachineExercise> list)
        {
            StringBuilder csv = new StringBuilder();
            List<String> titles = new List<String>( new String[] { "Id", "Machine Type", "Start Time", "Duration Seconds", "Distance Miles", "User Name", "Comment" } );
            csv = WriteLineInCsvStringBuilder(csv, titles);
            foreach (CardioMachineExercise row in list)
            {
                List<String> columns = new List<String>();
                columns.Add(escapeCommasOrQuotesForCsv(row.CardioMachineExerciseId.ToString()));
                columns.Add(escapeCommasOrQuotesForCsv(row.MachineType));
                columns.Add(escapeCommasOrQuotesForCsv(row.StartTime.ToString()));
                columns.Add(escapeCommasOrQuotesForCsv(row.DurationSeconds.ToString()));
                columns.Add(escapeCommasOrQuotesForCsv(row.DistanceMiles.ToString()));
                columns.Add(escapeCommasOrQuotesForCsv(row.UserName));
                columns.Add(escapeCommasOrQuotesForCsv(row.Comment));
                WriteLineInCsvStringBuilder(csv, columns);
            }
            return csv.ToString();
        }

        private StringBuilder WriteLineInCsvStringBuilder(StringBuilder csv, List<String> columns)
        {
            foreach(String column in columns)
            {
                csv.Append(column).Append(',');
            }
            csv.AppendLine();
            return csv;
        }

        private String escapeCommasOrQuotesForCsv(String s)
        {
            String ret = s;
            if(s != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach(char c in s.ToCharArray())
                {
                    if(c.Equals("\""))
                    {
                        sb.Append(c);
                    }
                    sb.Append(c);
                }
                sb.Append("\"");
                ret = sb.ToString();
            }
            return ret;
        }

        private String CsvCardioMachineExerciseFileName()
        {
            return "cardioMachineExercise-" + DateTime.Now.ToUniversalTime() + "-GMT.csv";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardioMachineExerciseExists(int id)
        {
            return context.CardioMachineExercises.Count(e => e.CardioMachineExerciseId == id) > 0;
        }
    }
}