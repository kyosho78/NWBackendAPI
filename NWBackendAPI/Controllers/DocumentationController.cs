using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NWBackendAPI.Models;
using System.Reflection.Metadata;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace NWBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {
        //private readonly NorthwindOriginalContext db = new();

        private readonly NorthwindOriginalContext db;

        public DocumentationController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }


        [HttpGet]
        public ActionResult GetDocuments()
        {
            List<Documentation> documents = db.Documentations.ToList();
            return Ok(documents);
        }


        [HttpGet("{keycode}")]

        public ActionResult GetDocumentation(string keycode)
        {
            if (keycode == "WB")
            {
                var docs = db.Documentations.ToList();
                return Ok(docs);
            }
            else
            {
                return Unauthorized("Invalid Keycode");
            }
        }




        //[HttpGet("{id}")]
        //public ActionResult GetDocumentationById(int id)
        //{
        //    try
        //    {
        //        var documentation = db.Documentations.FirstOrDefault(d => d.DocumentationId == id);
        //        if (documentation != null)
        //        {
        //            return Ok(documentation);
        //        }
        //        return NotFound($"Documentation with ID {id} not found at {DateTime.Now}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}

        //private readonly NorthwindOriginalContext _context = new();
        //private const string KeyCode = "123";

        //public DocumentationController(NorthwindOriginalContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet("{keycode}")]
        //public ActionResult GetDocumentationByKeycode(string keycode)
        //{
        //    if (keycode == KeyCode)
        //    {
        //        var documentation = _context.Documentations.ToList();
        //        if (documentation.Any())
        //        {
        //            return Ok(documentation);
        //        }
        //        return NotFound("Documentation is empty");
        //    }
        //    else
        //    {
        //        return NotFound($"Documentation missing at {DateTime.Now}");
        //    }
        //}

    }
}
