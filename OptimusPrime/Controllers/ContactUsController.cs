using OptimusPrimeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OptimusPrime.Controllers
{
    public class ContactUsController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                using (OptimusPrimeDBEntities db = new OptimusPrimeDBEntities())
                {
                    var contactUsList = db.ContactUs.Where(x => !x.IsDeleted).ToList();

                    if (contactUsList != null && contactUsList.Any())
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, contactUsList);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ContactUs data not found");
                    }

                }
            }            
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }

        [HttpGet]
        public HttpResponseMessage GetContactUsByID(int id)
        {
            try
            {
                using (OptimusPrimeDBEntities db = new OptimusPrimeDBEntities())
                {
                    var contactUs = db.ContactUs.FirstOrDefault(e => e.Id.Equals(id));

                    if (contactUs != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, contactUs);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ContactUs data with Id " + id + " not found");
                    }


                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddConnectUs([FromBody]ContactU contactUs)
        {
            try
            {
                using (OptimusPrimeDBEntities db = new OptimusPrimeDBEntities())
                {
                    contactUs.Created_Date = DateTime.Now;
                    db.ContactUs.Add(contactUs);
                    db.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.OK, contactUs);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + contactUs.Id.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
