using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using ChirpyR.Domain.Model;
using System.Net;

namespace ChirpyR.Web.Controllers
{
    public class RegistrationController : ApiController
    {
        // POST /api/values
        public HttpResponseMessage Post(ChirpyRUser model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                MembershipUser currentUser = Membership.GetUser(model.UserId);
                if (currentUser == null)
                {
                    Membership.CreateUser(model.UserId, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserId, createPersistentCookie: false);
                        return Request.CreateResponse<ChirpyRUser>(System.Net.HttpStatusCode.Created, model);
                    }
                    else
                    {
                        return Request.CreateResponse<ChirpyRUser>(System.Net.HttpStatusCode.InternalServerError, model);
                    }
                }
                else
                {
                    if (currentUser.ChangePassword(model.OldPassword, model.Password))
                    {
                        Membership.UpdateUser(currentUser);
                    }
                }
            }
            return Request.CreateResponse<ChirpyRUser>(System.Net.HttpStatusCode.InternalServerError, model);
        }

        public HttpResponseMessage Put(ChirpyRUser model)
        {
            MembershipCreateStatus createStatus;
            MembershipUser currentUser = Membership.GetUser(model.UserId);
            if (currentUser != null)
            {
                if (currentUser.ChangePassword(model.OldPassword, model.Password))
                {
                    Membership.UpdateUser(currentUser);
                }
            }
            return Request.CreateResponse<ChirpyRUser>(System.Net.HttpStatusCode.Accepted, model);
        }

        public HttpResponseMessage Get(string userId)
        {
            MembershipUser currentUser = Membership.GetUser(userId);
            if (currentUser != null)
            {
                ChirpyRUser model = new ChirpyRUser();
                model.UserId = userId;
                model.Email = currentUser.Email;
                return Request.CreateResponse<ChirpyRUser>(HttpStatusCode.Accepted, model);
            }
            else
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}