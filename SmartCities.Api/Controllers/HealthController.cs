﻿using Microsoft.AspNetCore.Mvc;

namespace SmartCities.Api.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() =>
            Ok(new { status = "UP" });
    }
}
