using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWBackendAPI.Models;
using System.Reflection;

namespace NWBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //Luodaan tietokantayhteys, db on Olio-ohjelmoinnin käsitteitä
        NorthwindOriginalContext db = new();

        //Hakee kaikki asiakkaat
        [HttpGet]
        public ActionResult GetCustomers()
        {
            List<Customer>asiakkaat = db.Customers.ToList();
            return Ok(asiakkaat);
        }


        //Hakee asiakkaan pääavaimella
        [HttpGet("{id}")]
        public ActionResult GetCustomerById(string id)
        {
            var asiakas = db.Customers.Find(id);
            if (asiakas == null)
            {
                //String interpolation tyyli
                return NotFound($"Asiakas {id} ei löydy!");
            }

            return Ok(asiakas);

        }
        //Uuden asiakkaan lisääminen
        [HttpPost]
        public ActionResult AddNewCustomer([FromBody] Customer customer)
        {


                db.Customers.Add(customer);
                db.SaveChanges();

                //Tämä palauttaa vastauksen, että onnistui
                return Ok($"Lisätty uusi {customer.CompanyName}");

        }

        //Asiakkaan poistaminen url parametrina, id:n perusteella
        [HttpDelete("{id}")]
        public ActionResult RemoveCustomerById(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);

                if (asiakas != null)
                {
                    //Poistetaan asiakas
                    db.Customers.Remove(asiakas);
                    db.SaveChanges();
                    //Kuittaus onnistuneesta poistosta
                    return Ok($"Poistettiin Asiakas {asiakas.CompanyName}");
                }
                else
                {
                    //String interpolation tyyli
                    return NotFound($"Asiakas {id} ei löydetty poistettavaksi!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Virhe {ex.Message}");
            }
        }
    }
}
