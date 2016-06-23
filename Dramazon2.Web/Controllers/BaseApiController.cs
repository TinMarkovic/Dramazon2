using Dramazon2.Data;
using Dramazon2.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dramazon2.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        private IDramazon2Repository _repo;
        private ModelFactory _modelFactory;

        public BaseApiController(IDramazon2Repository repo)
        {
            _repo = repo;
        }

        protected IDramazon2Repository TheRepository
        {
            get
            {
                return _repo;
            }
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(Request, _repo);
                }
                return _modelFactory;
            }
        }
    }
}
