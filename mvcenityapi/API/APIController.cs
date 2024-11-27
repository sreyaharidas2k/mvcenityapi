using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mvcenityapi.API
{
    public class APIController : ApiController
    {
        apidbEntities dbobj = new apidbEntities();
        // GET: api/API
        [HttpGet]
        [Route("api/API/getapi_tbs")]
        public IHttpActionResult Get()
        {
            return Ok(dbobj.api_tb.ToList());
        }

        // GET: api/API/5
        [HttpGet]
        [Route("api/API/getapi_tbwithid/{id}")]
        public IHttpActionResult Getwithid(int id)
        {
            api_tb employee = dbobj.api_tb.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST: api/API
        [HttpPost]
        [Route("api/API/postapi_tb")]
        public IHttpActionResult Post(api_tb api_tb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbobj.api_tb.Add(api_tb);
            dbobj.SaveChanges();
            return Ok(200);
        }

        // PUT: api/API/5
        [HttpPut]
        [Route("api/API/putapi_tbwithid/{id}")]
        public IHttpActionResult Put(int id, api_tb api_tb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbobj.Entry(api_tb).State = EntityState.Modified;
            dbobj.SaveChanges();
            return Ok(200);
        }

        // DELETE: api/API/5
        [HttpDelete]
        [Route("api/API/deleteapi_tbwithid/{id}")]
        public IHttpActionResult Delete(int id)
        {
            api_tb api_tb = dbobj.api_tb.Find(id);
            if (api_tb == null)
            {
                return NotFound();
            }
            dbobj.api_tb.Remove(api_tb);
            dbobj.SaveChanges();
            return Ok(api_tb);
        }
    }
}

