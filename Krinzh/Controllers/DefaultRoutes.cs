using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Krinzh.Models;

namespace Krinzh.Controllers
{
    [ApiController]
    public class DefaultRoutes : ControllerBase
    {
        [HttpGet]
        [Route("/{*url}")]
        public IActionResult RedirectAll()
        {
            // Return index.html file for all routes
            // Specific routes are handled by react-router-dom
            return File("~/index.html", "text/html");
        }
    }
}