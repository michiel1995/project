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
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace nmct.ba.cashlessproject.webapi.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        public List<Sale> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return SaleDA.GetListSale(p.Claims);
        }

        public HttpResponseMessage Post(List<Sale> Sales)
        {
            try
            {
                if (Sales == null)
                    throw new HttpResponseException(AddRequest(HttpStatusCode.BadRequest, "parameter is empty"));

                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                int id = SaleDA.AddSales(Sales,p.Claims);

                HttpResponseMessage response = new HttpResponseMessage();
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
                SaleDA.DeleteSale(id,p.Claims);
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
