using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Identity;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Krinzh.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Authorization;
using Rest;
using TwitchAPI.Util.JsonSerializers;
using TwitchAPI.Models;
using Krinzh.Contexts;

namespace Krinzh.Controllers
{
    [ApiController]
    public class TempController : ControllerBase
    {
        private readonly PostgresContext _context; /* InMemory context */

        public TempController(PostgresContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

    }
}
