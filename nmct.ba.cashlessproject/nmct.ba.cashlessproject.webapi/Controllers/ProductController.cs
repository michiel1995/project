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
    public class ProductController : ApiController
    {
        public List<Product> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return ProductDA.GetListProduct(p.Claims);
        }

        public HttpResponseMessage Post(Product newProduct)
        {
            try
            {
                if (newProduct == null)
                    throw new HttpResponseException(AddRequest(HttpStatusCode.BadRequest, "parameter is empty"));

                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                int id = ProductDA.AddProduct(newProduct,p.Claims);

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
        public HttpResponseMessage Put(Product updateProduct)
        {
            try
            {
                if (updateProduct == null)
                    throw new HttpResponseException(AddRequest(HttpStatusCode.BadRequest, "parameter is empty"));

                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                int id = ProductDA.ModifyProduct(updateProduct,p.Claims);

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
                ProductDA.DeleteProduct(id,p.Claims);
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
