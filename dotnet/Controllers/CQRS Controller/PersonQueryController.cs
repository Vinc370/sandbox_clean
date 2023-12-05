using ClosedXML.Excel;
using dotnet.Interface;
using dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using OfficeOpenXml;
using CsvHelper;
using System.Globalization;

namespace dotnet.Controllers
{
    [Route("api/{controller}/{action}")]
    [ApiController]
    public class PersonQueryController : Controller
    {
        private readonly GenericQuery<Person> query;

        public PersonQueryController (GenericQuery<Person> query)
        {
            this.query = query;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<Person> index = await query.FindAllNoPage();
            return Ok(index);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> IndexAPI(int id)
        {
            IEnumerable<Person> index = await query.FindAll(id);
            return Ok(index);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ViewAPI(int id)
        {
            Person index = await query.FindById(id);
            return Ok(index);
        }

        [HttpGet("{search}/{page}")]
        public async Task<IActionResult> SearchAPI(String search, int page)
        {
            IEnumerable<Person> index = await query.Search(search, page);
            return Ok(index);
        }

        [HttpGet("{asc}/{page}")]
        public async Task<IActionResult> Sort(bool asc, int page)
        {
            IEnumerable<Person> index = await query.SortByAge(asc, page);
            return Ok(index);
        }

        [HttpGet("{asc}/{page}")]
        public async Task<IActionResult> SortName(bool asc, int page)
        {
            IEnumerable<Person> index = await query.SortByName(asc, page);
            return Ok(index);
        }

        [HttpGet]
        public async Task<IActionResult> Export()
        {
            IEnumerable<Person> index = await query.FindAllNoPage();

            DataTable table = new DataTable("Personal Data");

            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("Fullname"),
                new DataColumn("Date of birth")
            });

            foreach (Person person in index)
            {
                table.Rows.Add(person.id, person.name, person.dob);
            }

            using XLWorkbook excel = new XLWorkbook();
            excel.Worksheets.Add(table);

            using MemoryStream stream = new MemoryStream();
            excel.SaveAs(stream);

            return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "export.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            List<Person> index = new List<Person>();

            using MemoryStream importData = new MemoryStream();
            await file.CopyToAsync(importData);

            using ExcelPackage excelPackage = new ExcelPackage(importData);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;

            for(int row = 2; row <= rowCount; row++)
            {
                var importResult = new Person
                {
                    id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                    name = Convert.ToString(worksheet.Cells[row, 2].Value),
                    dob = Convert.ToDateTime(worksheet.Cells[row, 3].Value),
                };
                index.Add(importResult);
            }
            return Ok(index);
        }

        [HttpPost]
        public async Task<IActionResult> ReadCSV(IFormFile file)
        {
            var streamReader = new StreamReader(file.OpenReadStream());
            var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            var index = csvReader.GetRecords<Person>();
            return Ok(index);
        }
    }
}
