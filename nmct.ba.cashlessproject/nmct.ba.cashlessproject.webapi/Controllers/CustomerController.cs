using nmct.ba.cashlessproject.models;
using nmct.ba.cashlessproject.webapi.helperClass;
using nmct.ba.cashlessproject.webapi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace nmct.ba.cashlessproject.webapi.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {
        public List<Customer> Get() {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return CustomerDA.GetListCustomers(p.Claims);
        }
        public HttpResponseMessage Get(string id)
        {

            if (string.IsNullOrEmpty(id))
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            Customer cust = CustomerDA.GetCustomer(Convert.ToInt32(id), p.Claims);
            if (cust == null)
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.NotFound };

            HttpResponseMessage response = new HttpResponseMessage();
            HttpContent content = new ObjectContent(typeof(Customer), cust, new JsonMediaTypeFormatter());
            response.Content = content;
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        public HttpResponseMessage Post(Customer newCustomer)
        {
            try
            {
                if (newCustomer == null)
                    throw new HttpResponseException(AddRequest(HttpStatusCode.BadRequest, "parameter is empty"));
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                int id = CustomerDA.AddCustomer(newCustomer, p.Claims);

                HttpResponseMessage response = new HttpResponseMessage();
                string url = string.Format("{0}{1}", HttpContext.Current.Request.Url.ToString(), id);
                response.Headers.Location = new Uri(url);
                response.StatusCode = HttpStatusCode.Created;
                return response;


            }
           catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage Put(Customer UpdateCustomer)
        {
            try
            {
                if (UpdateCustomer == null)
                    throw new HttpResponseException(AddRequest(HttpStatusCode.BadRequest, "parameter is empty"));
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                int id = CustomerDA.ModifyCustomer(UpdateCustomer, p.Claims);

                HttpResponseMessage response = new HttpResponseMessage();
                string url = string.Format("{0}{1}", HttpContext.Current.Request.Url.ToString(), id);
                response.Headers.Location = new Uri(url);
                response.StatusCode = HttpStatusCode.Created;
                return response;


            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

       

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                CustomerDA.DeleteCustomer(id,p.Claims);
                return new HttpResponseMessage(HttpStatusCode.OK);
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
