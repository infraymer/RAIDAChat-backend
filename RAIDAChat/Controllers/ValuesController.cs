﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RAIDAChat.Controllers
{

    public class ValuesController : ApiController
    {
        //GET api/values
        public void Get()
        {
            new Socket();
            //return new string[] { "value1", "value2" };
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}

        [HttpGet]
        [Route("openConnect")]
        public void openSocketConnect()
        {
            new Socket();
        }

        [HttpGet]
        [Route("closeConnect")]
        public void closeSocketConnect()
        {
            new Socket();
        }
    }
}
