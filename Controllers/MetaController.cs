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
                Content =

                        "<html>" +
                        "<body>" +
                        "<div>"+
                             "<div>"+
                                "< h1 > FINDIT APPICATION </ h1 >"+
                                $"<p>click <a href={variable}> Doc </a></p>"+
                            "</ div >" +
                            "<div>"+
                                "<div>"+
                                    "< H3 >< strong > 1.THE APP </ strong ></ H3 >"+
                                    "< p > FindIt App is super user friendly easily adaptable make sure to check it out on play store.</ p >"+


                               " </ div >"+
                            "</ div >"+ 
                            "<div>"+

                                "< h3 > 2.THE RESTFUL WEBAPI </ h3 >"+
                                "< ul >"+
                                   " < li > Easily integratable </ li >"+
                                    "< li > Loosely Coupled </ li >"+
                                    "< li > Based on best practices </ li >"+
                                    "< li > Super fast database </ li >"+
                                "</ ul >"+
                                "< h3 > Endpoints Included in Version 1.0 </ h3 >"+
                                "< ul >"+
                                   " < li > Registration of users </ li >"+
                                    "< li > Adding your location </ li >"+
                                    "< li > Finding the nearest location </ li >"+
                                    "< li > and more... </ li >"+

                                "</ ul >"+
                           " </ div >"+ 
                        "</div>" +
                        "</body>" +
                        "</html>"

                //"<html>" +
                //"<body>" +
                //"<div><p><b></b></p></div>" +
                //$"<p>click <a href={variable}> Doc </a></p>" +
                //"</body>" +
                //"</html>"
            };
            return result;
        }
    }
}
