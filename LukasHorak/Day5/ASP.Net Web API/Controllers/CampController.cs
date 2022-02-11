﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.Net_Web_API.Controllers
{
    [Route("api/[controller]")]
    public class CampController : Controller
    {
        public object Get()
        {
            return new { Moniker = "Praha", Name = ".Net Hatchery 2022" };
        }
    }
}
