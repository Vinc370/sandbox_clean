using Microsoft.AspNetCore.Mvc;
using dotnet.Data;
using dotnet.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using dotnet.Interface;

namespace dotnet.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly DatabaseInterface databaseInterface;

        public DatabaseController(DatabaseInterface databaseInterface)
        {
            this.databaseInterface = databaseInterface;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Database> database = await databaseInterface.GetAll();
            return View(database);
        }

        public async Task<IActionResult> View(int id)
        {
            Database database = await databaseInterface.GetById(id);
            return View(database);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            Database database = await databaseInterface.GetById(id);
            return View(database);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Database database)
        {
            databaseInterface.Create(database);
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Database database = await databaseInterface.GetById(id);
            databaseInterface.Delete(database);
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Database database)
        {
            databaseInterface.Update(database);
            return RedirectToAction("index");
        }
    }
}
