using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RAIDAChat.Controllers
{
   
    public class ValuesController : ApiController
    {

        [HttpGet]
        [Route("api/openConnect")]
        public void openSocketConnect()
        {
            new newSocket().OpenSocket();
        }

    }
}
