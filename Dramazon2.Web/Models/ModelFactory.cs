using Dramazon2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Dramazon2.Web.Models
{
    public class ModelFactory
    {
        private System.Web.Http.Routing.UrlHelper _UrlHelper;

        public ModelFactory(HttpRequestMessage request)
        {
            _UrlHelper = new System.Web.Http.Routing.UrlHelper(request);
        }

        public ProductModel Create(Product product)
        {
            if (product == null) return null;
            return new ProductModel()
            {
                Url = _UrlHelper.Link("Products", new { id = product.Id }),
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Creator = Create(product.Creator)
            };
        }

        public CustomerModel Create(Customer customer)
        {
            if(customer == null) return null;
            return new CustomerModel()
            {
                Id = customer.Id,
                Username = customer.Username,
                Address = customer.Address,
                Fullname = customer.Fullname
            };
        }

        public TagModel Create(Tag tag)
        {
            if (tag == null) return null;
            return new TagModel()
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }
    }
}