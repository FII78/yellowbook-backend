using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FindIt.Backend.Models;
using FindIt.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindIt.API.Controllers
{
    [Route("api/meta")]
    [ApiController]
    public class MetaController : Controller
    {
        IGeoService _geoRepository;

        public MetaController(IGeoService geoRepository)
        {
            _geoRepository = geoRepository ?? throw new ArgumentNullException(nameof(geoRepository));
           
        }
        [HttpGet]
        [Route("documentationPage")]
        [AllowAnonymous]
        public ContentResult AboutUs()
        {
            var URL = "https://documenter.getpostman.com/view/10132092/TVejgVZp";
            var result = new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content =

                        "<html>" +
                        "<body>" +
                        "<div>"+
                             "<div>< h1 > FINDIT APPICATION </ h1 >" +
                             $"<p>Here's the documentation for the FIndIT RestFull WebAPI <a href={URL}> Postman Documentation</a></p></ div >"
                                 +
                            "<div>"+
                                "<div>< h3 >  1.THE APP </ h3 >< p > FindIt App is super user friendly easily adaptable make sure to check it out on play store.</ p > </ div >"+
                            "</ div >"+ 
                            "<div>< h3 > 2.THE RESTFUL WEBAPI </ h3 >"+

                                
                                "< ul>< li > Easily integratable </ li >< li > Loosely Coupled </ li > < li > Based on best practices </ li > < li > Super fast database </ li ></ ul >"+
                               
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

        [HttpGet]
        [Route("GetAllLocationDemo")]
        [AllowAnonymous]
       
            public async Task<ContentResult> Get()
            {
            var accounts = await _geoRepository.GetAllAsync();
            var result = new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content =

            "<html>" +
            "<body>" +
            "<div><p><b></b></p></div>" +
            $"<p>click {accounts} Doc </a></p>" +
            "</body>" +
            "</html>"
            };
            return result;
            }
       
             
        }
    }

