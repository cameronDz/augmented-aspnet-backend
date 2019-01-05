using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AugmentedAspnetBackend.DAL;
using AugmentedAspnetBackend.Models;
using AugmentedAspnetBackend.Models.ApiHelpers;
using AugmentedAspnetBackend.Models.Workout;
using AugmentedAspnetBackend.Properties;
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

        // GET: api/CardioMachineExercises?pageNumber=##&pageSize=##
        [HttpGet]
        public HttpResponseMessage GetCardioMachineExercises([FromUri]PagingParameterModel pagingParameterModel)
        {
            if (pagingParameterModel.PageNumber < 1 || pagingParameterModel.PageSize < 1)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }
            var source = context.CardioMachineExercises.OrderByDescending(c => c.StartTime);

            // Get's No of Rows Count; throw exception if not content 
            int totalRecords = source.Count();
            if (totalRecords == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            // get page size and count info
            int pageSize = pagingParameterModel.PageSize;
            int currentPage = pagingParameterModel.PageNumber;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            
            // Returns List of Customer after applying Paging   
            var items = source.Skip((currentPage - 1) * pageSize).Take(pageSize);
            var links = CreateLinks(pagingParameterModel, totalPages);
            var metaData = new ApiMetaDataModel()
            {
                _totalRecords = totalRecords,
                _totalPages = totalPages,
                _currentPage = currentPage
            };

            var payload = new ApiResponseModel() { Data = items, Meta = metaData, Links = links };
            var response = Request.CreateResponse(HttpStatusCode.OK, payload);
            return response;
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
        public HttpResponseMessage DownloadCardioMachineExercieCsv(string csv)
        {
            IEnumerable<CardioMachineExercise> list = context.CardioMachineExercises.OrderBy(c => c.StartTime);
            var csvString = FullCardioMachineExerciseListCsv(list);
            string fileName = CsvCardioMachineExerciseFileName();
            var result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StringContent(csvString, Encoding.UTF8, "text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            return result;
        }

        public string FullCardioMachineExerciseListCsv(IEnumerable<CardioMachineExercise> list)
        {
            StringBuilder csv = new StringBuilder();
            List<string> titles = new List<string>( new string[] { "Id", "Machine Type", "Start Time", "Duration Seconds", "Distance Miles", "User Name", "Comment" } );
            csv = WriteLineInCsvStringBuilder(csv, titles);
            foreach (CardioMachineExercise row in list)
            {
                List<string> columns = new List<string>
                {
                    EscapeCommasOrQuotesForCsv(row.CardioMachineExerciseId.ToString()),
                    EscapeCommasOrQuotesForCsv(row.MachineType),
                    EscapeCommasOrQuotesForCsv(row.StartTime.ToString()),
                    EscapeCommasOrQuotesForCsv(row.DurationSeconds.ToString()),
                    EscapeCommasOrQuotesForCsv(row.DistanceMiles.ToString()),
                    EscapeCommasOrQuotesForCsv(row.UserName),
                    EscapeCommasOrQuotesForCsv(row.Comment)
                };
                WriteLineInCsvStringBuilder(csv, columns);
            }
            return csv.ToString();
        }

        private StringBuilder WriteLineInCsvStringBuilder(StringBuilder csv, List<string> columns)
        {
            foreach(string column in columns)
            {
                csv.Append(column).Append(',');
            }
            csv.AppendLine();
            return csv;
        }

        private string EscapeCommasOrQuotesForCsv(string s)
        {
            string ret = s;
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

        private string CsvCardioMachineExerciseFileName()
        {
            return "cardioMachineExercise-" + DateTime.Now.ToUniversalTime() + "-GMT.csv";
        }

        protected ApiLinksModel CreateLinks(PagingParameterModel pagingParameterModel, int totalPages)
        {
            string baseUrl = Settings.Default.BaseHttp + Settings.Default.Version + "/api/CardioMachineExercises?pageSize=" + pagingParameterModel.PageSize + "&pageNumber=";
            var links = new ApiLinksModel
            {
                Self = baseUrl + pagingParameterModel.PageNumber,
                First = baseUrl + 1,
                Last = baseUrl + totalPages,
                Prev = baseUrl + (pagingParameterModel.PageNumber == 1 ? 1 : pagingParameterModel.PageNumber - 1),
                Next = baseUrl + (pagingParameterModel.PageNumber == totalPages ? totalPages : pagingParameterModel.PageNumber + 1)
            };
            return links;
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