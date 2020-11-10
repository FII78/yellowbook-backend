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
                        
                             "<div>< h1 > FINDIT APPICATION </ h1 ></ div > "+
                            $"<p>click <a href={URL}> Doc </a></p>" +

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
            $"<p>Number of locations currently in the databaase= {accounts.Count()}   </a></p>" +
            "</body>" +
            "</html>"
            };
            return result;
            }
       
             
        }
    }

