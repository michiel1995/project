using nmct.ba.cashlessproject.models;
using nmct.ba.cashlessproject.webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace nmct.ba.cashlessproject.webapi.Controllers
{
    public class ChangePassController : ApiController
    {
        public HttpResponseMessage Put(ChangePassword pas)
        {
            try
            {
                if (pas == null)
                    throw new HttpResponseException(AddRequest(HttpStatusCode.BadRequest, "parameter is empty"));
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                int id = ChangePassDA.ChangePass(pas, p.Claims);
                if (id == 1)
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    string url = string.Format("{0}{1}", HttpContext.Current.Request.Url.ToString(), id);
                    response.Headers.Location = new Uri(url);
                    response.StatusCode = HttpStatusCode.Created;
                    return response;
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }


            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        private HttpResponseMessage AddRequest(HttpStatusCode code, string message)
        {
            return Request.CreateErrorResponse(code, message);
        }
    }
}
