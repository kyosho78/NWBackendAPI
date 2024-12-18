using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NWBackendAPI.Models;
using System.Text;

namespace NWBackendAPI.Tests
{
    [TestClass]
    public class ProductsMoreTests
    {
        [TestMethod]
        public async Task GetProductById()
        {
            // Alustus
            var webAppFactory = new WebApplicationFactory<Program>();
            var client = webAppFactory.CreateDefaultClient();

            // Lisätään ensin uusi Product
            var newProduct = new Product { ProductName = "GetTestProduct", UnitPrice = 1, CategoryId = 1 };
            var content = new StringContent(JsonConvert.SerializeObject(newProduct), Encoding.UTF8, "application/json");
            var responsePost = await client.PostAsync("api/products", content);
            var createdProduct = JsonConvert.DeserializeObject<Product>(await responsePost.Content.ReadAsStringAsync());

            // Haetaan lisätty Product
            var responseGet = await client.GetAsync($"api/products/{createdProduct.ProductId}");
            var fetchedProduct = JsonConvert.DeserializeObject<Product>(await responseGet.Content.ReadAsStringAsync());

            // Tarkistetaan että Product on haettu
            Assert.AreEqual(System.Net.HttpStatusCode.OK, responseGet.StatusCode);
            Assert.AreEqual("GetTestProduct", fetchedProduct.ProductName);
        }
    }
}