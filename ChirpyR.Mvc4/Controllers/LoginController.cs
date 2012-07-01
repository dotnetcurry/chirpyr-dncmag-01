using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using ChirpyR.Domain.Model;
using System.Web.Security;

namespace ChirpyR.Web.Controllers.Api
{
    public class LoginController : ApiController
    {
        public HttpResponseMessage Post(ChirpyRUser user)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(user.UserId, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserId, false);
                    return Request.CreateResponse<ChirpyRUser>(System.Net.HttpStatusCode.OK, user);
                }
                else
                {
                    return Request.CreateResponse<ChirpyRUser>(System.Net.HttpStatusCode.Forbidden, user);
                }
            }
            else
            {
                return Request.CreateResponse<ChirpyRUser>(System.Net.HttpStatusCode.Forbidden, user);
            }
        }

        public HttpResponseMessage Put(ChirpyRUser user)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SignOut();
            }
            return Request.CreateResponse<ChirpyRUser>(System.Net.HttpStatusCode.OK, null);
        }
    }
}
