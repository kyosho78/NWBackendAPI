using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWBackendAPI.Models;
using System.Reflection.Metadata;

namespace NWBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {
        NorthwindOriginalContext db = new();


        [HttpGet]
        public ActionResult GetDocuments()
        {
            List<Documentation> documents = db.Documentations.ToList();
            return Ok(documents);
        }



        [HttpGet("{id:int}")]
        public ActionResult GetDocumentationById(int id)
        {
            try
            {
                var documentation = db.Documentations.FirstOrDefault(d => d.DocumentationId == id);
                if (documentation != null)
                {
                    return Ok(documentation);
                }
                return NotFound($"Documentation with ID {id} not found at {DateTime.Now}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
