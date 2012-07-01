using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChirpyR.Domain.Model;
using SignalR;
using ChirpyR.Mvc4.SignalR;
using System.Web.Security;
using ChirpyR.Domain.Repository;

namespace ChirpyR.Mvc4.Controllers
{
    public class ChirpyRController : ApiController
    {
        //IChirpyRRepository _repository;
        public ChirpyRController()
        {
        }

        // GET api/chirpyr
        public HttpResponseMessage Get()
        {
            //IList<Chirp> chirps =_repository.GetLatestChirpsFor(CurrentUserName());
            return Request.CreateResponse<IList<Chirp>>(HttpStatusCode.OK, null);
        }

        private string CurrentUserName()
        {
            return this.User != null && this.User.Identity != null
                                            ? this.User.Identity.Name
                                            : "(none)";
        }

        // GET api/chirpyr/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/chirpyr
        public void Post(Chirp model)
        {
            if (ModelState.IsValid)
            {
                //_repository.AddChirp(model);
                var hubContext =
                    GlobalHost
                        .ConnectionManager
                            .GetHubContext<ChirpyRHub>();
                model.ChirpBy = new ChirpyRUser
                {
                    UserId = this.User != null && this.User.Identity != null
                                ? this.User.Identity.Name
                                : "(none)",
                    Gravataar = @"http://www.gravatar.com/avatar/147bacafcdb00d67d3336ecdf4078ba5.png"
                };
                hubContext.Clients.NewChirp(model);
            }
        }

        // PUT api/chirpyr/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/chirpyr/5
        public void Delete(int id)
        {
        }
    }
}
