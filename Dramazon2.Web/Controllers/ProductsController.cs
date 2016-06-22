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
    public class ProductsController : BaseApiController
    {
        public ProductsController(IDramazon2Repository repo) : base(repo)
        {
        }

        public IEnumerable<ProductModel> Get()
        {
            IQueryable<Product> query;

            query = TheRepository.GetAllProducts();

            var results = query.ToList().Select(s => TheModelFactory.Create(s));

            return results;
        }

        public HttpResponseMessage GetProduct(int id)
        {
            try
            {
                var product = TheRepository.GetProduct(id);
                if (product != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(product));
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

        //public List<Product> Get()
        //{
        //    IDramazon2Repository repository = new Dramazon2Repository(new Dramazon2Context());

        //    return repository.GetAllProducts().ToList();
        //}

        //public Product GetProduct(int id)
        //{
        //    IDramazon2Repository repository = new Dramazon2Repository(new Dramazon2Context());

        //    return repository.GetProduct(id);
        //}
    }
}
