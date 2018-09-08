using AugmentedAspnetBackend.Repositories;
using AugmentedAspnetBackend.ViewModels.Workouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AugmentedAspnetBackend.Controllers
{
    public class SessionViewModelController : ApiController
    {
        SessionRepository _repository;

        public SessionViewModelController()
        {
            _repository = new SessionRepository();
        }

        public SessionViewModelController(SessionRepository repository)
        {
            _repository = repository;
        }

        // GET: api/SessionViewModel
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SessionViewModel/5
        public SessionsViewModel Get(int id)
        {
            return _repository.GetSessionsViewModel(id);
        }

        // POST: api/SessionViewModel
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SessionViewModel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SessionViewModel/5
        public void Delete(int id)
        {
        }
    }
}
