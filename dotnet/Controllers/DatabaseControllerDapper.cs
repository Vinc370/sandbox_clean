using dotnet.Interface;
using dotnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [Route("api/database/{action}")]
    [ApiController]
    public class DatabaseControllerDapper : Controller
    {
        private readonly DapperInterface databaseInterface;

        public DatabaseControllerDapper(DapperInterface databaseInterface)
        {
            this.databaseInterface = databaseInterface;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Database> indexD = await databaseInterface.GetAll();
            return View(indexD);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> View(int id)
        {
            Database viewD = await databaseInterface.GetById(id);
            return View(viewD);
        }

        [HttpGet]
        public async Task<IActionResult> IndexAPI()
        {
            IEnumerable<Database> indexD = await databaseInterface.GetAll();
            return Ok(indexD);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ViewAPI(int id)
        {
            Database viewD = await databaseInterface.GetById(id);
            return Ok(viewD);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteAPI(int id)
        {
            await databaseInterface.Delete(id);
            return Redirect("https://localhost:7026/api/database/index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAPI([FromForm] Database database)
        {
            await databaseInterface.Create(database);
            return Redirect("https://localhost:7026/api/database/index");
        }

        [HttpGet("{id}")]
        public Task<IActionResult> Update(int id)
        {
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAPI([FromForm] Database database)
        {
            await databaseInterface.Update(database);
            return Redirect("https://localhost:7026/api/database/index");
        }
    }
}
