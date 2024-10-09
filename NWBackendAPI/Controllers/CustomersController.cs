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
        //private readonly NorthwindOriginalContext db = new();

        //Dependency Injection, tietokantayhteys injektoidaan kontrolleriin
        private readonly NorthwindOriginalContext db;

        public CustomersController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }

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

        //Asiakkaan haku companynamen perusteella, nimen osa riittää
        [HttpGet("company/{companyname}")]
        //Metodi 
        public ActionResult SearchCustomerByCompanyName(string companyname)
        //Contains metodi etsii osaa nimestä(lambda funktio)
        {
            var asiakkaat = db.Customers.Where(c => c.CompanyName.Contains(companyname));
            //var asiakkaat = db.Customers.Where(c => c.CompanyName == companyname); <--- Tämä etsii tasan tietyn nimen
            //var asiakkaat = from c db.Customers where c.CompanyName.Contains(companyname); <--- Toinen tapa tehdä sama asia

            return Ok(asiakkaat);
        }

        //Asiakkaan muokkaaminen
        [HttpPut("{id}")]
        public ActionResult EditCustomer(string id, [FromBody] Customer customer)
        {

            //Haetaan id:n perusteella tietokannasta vanha asiakasobjekti
            var asiakas = db.Customers.Find(id);
            if (asiakas != null)
            {
                //Etsitään asiakas ja päivitetään sen tiedot
                asiakas = customer;

                db.SaveChanges();
                return Ok($"Asiakkaan {asiakas.CompanyName} tiedot päivitetty");
            }
            else
            {
                return NotFound($"Asiakasta {id} ei löydy");
            }
        }
    }
}
