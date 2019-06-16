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
using System.Web.Http;
using System.Web.Http.Description;
using AugmentedAspnetBackend.DAL;
using AugmentedAspnetBackend.Models;
using AugmentedAspnetBackend.Models.ApiHelpers;
using AugmentedAspnetBackend.Models.Workout;
using AugmentedAspnetBackend.Properties;

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

        // GET: api/CaffeineNutrientIntakePages?pageNumber=##&pageSize=##
        [HttpGet]
        public HttpResponseMessage GetPaginatedCaffeineNutrientIntakes([FromUri]PagingParameterModel pagingParameterModel)
        {
            if (pagingParameterModel.PageNumber < 1 || pagingParameterModel.PageSize < 1)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }
            var source = context.CaffeineNutrientIntakes.OrderByDescending(c => c.IntakeTime);
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

        [HttpGet]
        public HttpResponseMessage DownloadCaffeineNutrientIntakeCsv(string csv)
        {
            IEnumerable<CaffeineNutrientIntake> list = context.CaffeineNutrientIntakes.OrderBy(c => c.IntakeTime);
            var csvString = FullCaffeineNutrientIntakeListCsv(list);
            string fileName = CsvCaffeineNutrientIntakeFileName();
            var result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StringContent(csvString, Encoding.UTF8, "text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            return result;
        }

        protected string FullCaffeineNutrientIntakeListCsv(IEnumerable<CaffeineNutrientIntake> list)
        {
        StringBuilder csv = new StringBuilder();
            List<string> titles = new List<string>(new string[] { "Id", "Amount", "Amount Type", "Intake Time", "User Name", "Comment" });
            csv = WriteLineInCsvStringBuilder(csv, titles);
            foreach (CaffeineNutrientIntake row in list)
            {
                List<string> columns = new List<string>
                {
                    EscapeCommasOrQuotesForCsv(row.CaffeineNutrientIntakeId.ToString()),
                    EscapeCommasOrQuotesForCsv(row.Amount.ToString()),
                    EscapeCommasOrQuotesForCsv(row.AmountType),
                    EscapeCommasOrQuotesForCsv(row.IntakeTime.ToString()),
                    EscapeCommasOrQuotesForCsv(row.UserName),
                    EscapeCommasOrQuotesForCsv(row.Comment)
                };
                WriteLineInCsvStringBuilder(csv, columns);
            }
            return csv.ToString();
        }

        private StringBuilder WriteLineInCsvStringBuilder(StringBuilder csv, List<string> columns)
        {
            foreach (string column in columns)
            {
                csv.Append(column).Append(',');
            }
            csv.AppendLine();
            return csv;
        }

        private string EscapeCommasOrQuotesForCsv(string s)
        {
            string ret = s;
            if (s != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char c in s.ToCharArray())
                {
                    if (c.Equals("\""))
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

        private string CsvCaffeineNutrientIntakeFileName()
        {
            return "CaffeineNutrientIntake-" + DateTime.Now.ToUniversalTime() + "-GMT.csv";
        }

        private bool CaffeineNutrientIntakeExists(int id)
        {
            return context.CaffeineNutrientIntakes.Count(e => e.CaffeineNutrientIntakeId == id) > 0;
        }

        protected ApiLinksModel CreateLinks(PagingParameterModel pagingParameterModel, int totalPages)
        {
            string baseUrl = Settings.Default.BaseHttp + Settings.Default.Version + "/api/CaffeineNutrientIntakes?pageSize=" + pagingParameterModel.PageSize + "&pageNumber=";
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
    }
}
