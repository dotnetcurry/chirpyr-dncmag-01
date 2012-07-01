using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChirpyR.Domain.Repository;

namespace ChirpyR.Mvc4.Controllers
{
    public class BaseChirpyRController : ApiController
    {
        protected IChirpyRRepository _repository;
        public BaseChirpyRController(IChirpyRRepository repository)
        {
            _repository = repository;
        }

    }
}
