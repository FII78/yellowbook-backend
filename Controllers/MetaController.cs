using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindIt.API.Controllers
{
    [Route("api/meta")]
    [ApiController]
    public class MetaController : Controller
    {
        [HttpGet]
        [Route("aboutUs")]
        [AllowAnonymous]
        public ContentResult AboutUs()
        {
            var variable = "https://something.com";
            var result = new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html>" +
                "<body>" +
                "<div><p><b>Bla Bla</b></p></div>" +
                $"<p>click <a href={variable}> Doc </a></p>" +
                "/body" +
                "</html>"
            };
            return result;
        }
    }
}
