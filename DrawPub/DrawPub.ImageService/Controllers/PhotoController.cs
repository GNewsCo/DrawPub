﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace DrawPub.ImageService.Controllers
{
    public class PhotoController : ApiController
    {
        // GET: api/Photo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [Authorize]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Photo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Photo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Photo/5
        public void Delete(int id)
        {
        }
    }
}
