using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController(IConfiguration configuration) : ControllerBase
    {

        private string dbName = "Test";

        [HttpGet]
        [Route("my-key")]
        public IActionResult GetMyKey()
        {
            var myKey = configuration["MyKey"];
            return Ok(myKey);
        }

        [HttpGet]
        [Route("datbase-configuration")]
        public IActionResult GetDatabaseConfiguration()
        {
            var type = configuration["Database:Type"];
            var connectionString = configuration["Database:ConnectionString"];

            return Ok(new { Type = type, ConnectionString = connectionString });
        }

        [HttpGet]
        [Route("database-configuration-with-bind")]
        public IActionResult GetDatabaseConfigurationWithBind()
        {
            var databaseOption = new DatabaseOption();

            configuration.GetSection(DatabaseOption.SectionName).Bind(databaseOption);
            return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
        }

        [HttpGet]
        [Route(nameof(GetDatabaseConfigurationGenericType))]
        public IActionResult GetDatabaseConfigurationGenericType()
        {
            var databaseOption = configuration.GetSection(DatabaseOption.SectionName).Get<DatabaseOption>();
            return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
        }

        [HttpGet]
        [Route(nameof(GetDatabaseConfigurationWithIOption))]
        public IActionResult GetDatabaseConfigurationWithIOption([FromServices] IOptions<DatabaseOption> options)
        {
            var databaseOption = options.Value;
            return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
        }

        [HttpGet]
        [Route("datbase-configuration-user-secret")]
        public IActionResult GetDatabaseConfigurationUserSecret()
        {
            var type = configuration["Database:Type"];
            

            return Ok(new { Type = type });
        }
    }
}
