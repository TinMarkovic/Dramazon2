﻿using Dramazon2.Data;
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

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            IQueryable<Product> query;

            query = TheRepository.GetAllProducts();

            var results = query.ToList().Select(s => TheModelFactory.Create(s));

            return results;
        }

        [HttpGet]
        [Route("~/api/products/{id}")]
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

        [HttpPost]
        public HttpResponseMessage Post(ProductModel productModel)
        {
            try
            {
                var entity = TheModelFactory.Parse(productModel);

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
        [Route("~/api/products/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] ProductModel productModel)
        {
            try
            {

                var updatedProduct = TheModelFactory.Parse(productModel);

                if (updatedProduct == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read data from body");

                var originalProduct = TheRepository.GetProduct(id);

                if (originalProduct == null || originalProduct.Id != id)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Product is not found");
                }
                else
                {
                    updatedProduct.Id = id;
                }

                if (TheRepository.Update(originalProduct, updatedProduct) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(updatedProduct));
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
        [Route("~/api/products/{id}")]
        public HttpResponseMessage Delete(int id)
        {

            try
            {
                var product = TheRepository.GetProduct(id);

                if (product == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (TheRepository.DeleteProduct(id) && TheRepository.SaveAll())
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

        [Route("~/api/tags/{tagId:int}/products")]
        [Route("~/api/products/tags/{tagId:int}")]
        [HttpGet]
        public IEnumerable<ProductModel> GetProductsByTag(int tagId)
        {
            IQueryable<Product> query;

            query = TheRepository.GetProductsByTag(tagId);

            var results = query.ToList().Select(s => TheModelFactory.Create(s));

            return results;
        }
    }
}
