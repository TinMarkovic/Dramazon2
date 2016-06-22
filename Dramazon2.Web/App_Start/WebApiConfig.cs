using Dramazon2.Data;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Dramazon2.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IDramazon2Repository, Dramazon2Repository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
            name: "Products",
            routeTemplate: "api/products/{id}",
            defaults: new { controller = "products", id = RouteParameter.Optional }
            );
        }
    }
}
