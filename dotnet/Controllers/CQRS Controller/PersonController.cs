using ClosedXML.Excel;
using dotnet.Interface;
using dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace dotnet.Controllers
{
    public class PersonController : Controller
    {
        private readonly GenericQuery<Person> query;

        public PersonController(GenericQuery<Person> query)
        {
            this.query = query;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult create()
        {
            return View();
        }

        public IActionResult View(int id)
        {
            return View();
        }

        public IActionResult update(int id)
        {
            return View();
        }
    }
}
