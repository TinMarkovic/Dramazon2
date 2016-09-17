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
    [RoutePrefix("api/tags")]
    public class TagsController : BaseApiController
    {
        public TagsController(IDramazon2Repository repo) : base(repo)
        {
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<TagModel> Get()
        {
            IQueryable<Tag> query;

            query = TheRepository.GetAllTags();

            var results = query.ToList().Select(s => TheModelFactory.Create(s));

            return results;
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetTag(int id)
        {
            try
            {
                var tag = TheRepository.GetTag(id);
                if (tag != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(tag));
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

        [HttpGet]
        [Route("{name}")]
        public HttpResponseMessage GetTag(string name)
        {
            try
            {
                var tag = TheRepository.GetTagByName(name);
                if (tag != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(tag));
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

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Tag tag)
        {
            try
            {
                var entity = tag;

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
        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, [FromBody] Tag tagModel)
        {
            try
            {

                var updatedTag = tagModel;

                if (updatedTag == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read data from body");

                var originalTag = TheRepository.GetTag(id);

                if (originalTag == null || originalTag.Id != id)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Tag is not found");
                }
                else
                {
                    updatedTag.Id = id;
                }

                if (TheRepository.Update(originalTag, updatedTag) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(updatedTag));
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
        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var tag = TheRepository.GetTag(id);

                if (tag == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (TheRepository.DeleteTag(id) && TheRepository.SaveAll())
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
