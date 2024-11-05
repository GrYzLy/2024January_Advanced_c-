using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using MyFirstApi.Services;

namespace MyApp.Namespace
{
    [Route("api/[controller]/")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }


        [HttpGet]
        public ActionResult Get()
        {
            return Content(_demoService.SayHello());
        }
    }
}
