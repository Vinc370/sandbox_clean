using dotnet.Interface;
using dotnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [Route("api/{controller}/{action}")]
    [ApiController]
    public class PersonCommandController : Controller
    {
        private readonly GenericCommand<Person> command;

        public PersonCommandController(GenericCommand<Person> command)
        {
            this.command = command;
        }

        [HttpPost]
        public async Task<IActionResult> createAPI([FromBody] Person model)
        {
            await command.Create(model);
            return Redirect("https://localhost:7026/api/personquery/index");
        }

        [HttpPost]
        public async Task<IActionResult> updateAPI([FromBody] Person model)
        {
            await command.Update(model);
            return Redirect("https://localhost:7026/api/personquery/index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            await command.Delete(id);
            return Redirect("https://localhost:7026/api/personquery/index");
        }
    }
}
