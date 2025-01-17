using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using MyFirstApi.Services;

namespace MyApp.Namespace
{
    [Route("api/[controller]/")]
    [ApiController]
    public class LifetimeController : ControllerBase
    {
        private readonly IScopedService _scopedService;

        private readonly ITransientService _transientService;

        private readonly ISingletonService _singletonService;


        public LifetimeController(IScopedService scopedService, ITransientService transientService, ISingletonService singletonService)
        {
            _scopedService = scopedService;
            _transientService = transientService;
            _singletonService = singletonService;
        }


        [HttpGet]
        public ActionResult Get()
        {
            var scopedServiceMessage = _scopedService.SayHello();
            var transientServiceMessage = _transientService.SayHello();
            var singletonServiceMessage = _singletonService.SayHello();



            return Content(
                $"{scopedServiceMessage}{Environment.NewLine}{transientServiceMessage}{Environment.NewLine}{singletonServiceMessage}"
            );
        }
    }
}
