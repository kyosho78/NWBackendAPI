using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NWBackendAPI.Models;
using System.Text;

namespace NWBackendAPI.Tests
{
    [TestClass]
    public class ProductsTests
    {
        [TestMethod]
        public async Task ProductsCrudTest()
        {
            // Alustus
            WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>();
            HttpClient client = webAppFactory.CreateDefaultClient();

            // Luodaan uusi Product  
            Product newProduct = new Product
            {
                ProductName = "TestProduct",
                UnitPrice = 1,
                CategoryId = 1
            };

            string input = JsonConvert.SerializeObject(newProduct);
            StringContent content = new StringContent(input, Encoding.UTF8, "application/json");

            // POST pyyntö asiakkaan lisäämiseksi
            var responsePost = await client.PostAsync("api/products", content);
            Assert.AreEqual("created", responsePost.StatusCode.ToString().ToLower());

            // Lähetetään muodostettu data testattavalle api:lle post pyyntönä
            var responseGet = await client.GetAsync("api/products");


            // Tarkistetaan onko vastaus ok
            Assert.AreEqual("created", responsePost.StatusCode.ToString().ToLower());

            // Koitetaan hakea saman niminen Product 
            var json = await responseGet.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<Product[]>(json).ToList();
            var product = products.FirstOrDefault(p => p.ProductName == "TestProduct");
            Assert.IsNotNull(product);
            Assert.AreEqual("TestProduct", product.ProductName);

            // Poistetaan Product
            var responseDelete = await client.DeleteAsync($"api/products/{product.ProductId}");
            Assert.AreEqual("ok", responseDelete.StatusCode.ToString().ToLower());

            //Varmistetaan että Product on poistettu
            var responseGetDeleted = await client.GetAsync($"api/products/{product.ProductId}");
            Assert.AreEqual("notfound", responseGetDeleted.StatusCode.ToString().ToLower());
        }
    }
}