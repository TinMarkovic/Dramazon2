using Dramazon2.Data;
using Dramazon2.Data.Models;
using Dramazon2.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dramazon2.Web.Controllers
{
    public class CustomersController : BaseApiController
    {
        public CustomersController(IDramazon2Repository repo) : base(repo)
        {
        }

        //[HttpGet]
        //public IEnumerable<CustomerModel> Get()
        //{
        //    IQueryable<Customer> query;

        //    query = TheRepository.GetAllCustomers();

        //    var results = query.ToList().Select(s => TheModelFactory.Create(s));

        //    return results;
        //}

        [HttpGet]
        public HttpResponseMessage GetCustomer(int id)
        {
            try
            {
                var customer = TheRepository.GetProduct(id);
                if (customer != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(customer));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] ProductModel customerModel)
        {
            try
            {
                var entity = TheModelFactory.Parse(customerModel);

                if (entity == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read data from body");

                if (TheRepository.Insert(entity) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(entity));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to the database.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPatch]
        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put(int id, [FromBody] ProductModel customerModel)
        {
            try
            {

                var updatedCustomer = TheModelFactory.Parse(customerModel);

                if (updatedCustomer == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read data from body");

                var originalCustomer = TheRepository.GetCustomerById(id);

                if (originalCustomer == null || originalCustomer.Id != id)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Course is not found");
                }
                else
                {
                    updatedCustomer.Id = id;
                }

                if (TheRepository.Update(originalCustomer, updatedCustomer) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(updatedCustomer));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var customer = TheRepository.GetCustomerById(id);

                if (customer == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (TheRepository.DeleteCustomer(id) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
