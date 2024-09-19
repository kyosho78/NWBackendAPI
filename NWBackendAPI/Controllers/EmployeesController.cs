using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWBackendAPI.Models;

namespace NWBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        //Luodaan tietokantayhteys, db on Olio-ohjelmoinnin käsitteitä
        //private readonly NorthwindOriginalContext db = new();

        //Dependency Injection, tietokantayhteys injektoidaan kontrolleriin
        private readonly NorthwindOriginalContext db;

        public EmployeesController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }

        //Hakee kaikki asiakkaat
        [HttpGet]
        public ActionResult GetEmployees()
        {
            List<Employee> employees = db.Employees.ToList();
            return Ok(employees);
        }



    }
}
