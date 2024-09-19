using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWBackendAPI.Models;

namespace NWBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Luodaan tietokantayhteys, db on Olio-ohjelmoinnin käsitteitä
        //private readonly NorthwindOriginalContext db = new();

        //Dependency Injection, tietokantayhteys injektoidaan kontrolleriin
        private readonly NorthwindOriginalContext db;

        public ProductsController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }

        //Hakee kaikki asiakkaat
        [HttpGet]
        public ActionResult GetProducts()
        {
            List<Product> products = db.Products.ToList();
            return Ok(products);
        }
    }
}
