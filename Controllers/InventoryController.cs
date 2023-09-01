using InventoryAPIClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace InventoryAPIClient.Controllers
{
    public class InventoryController : Controller
    {
         Uri baseAddress = new Uri("https://localhost:7158");
        HttpClient client;

        public InventoryController()
        {
            client =new HttpClient();
            client.BaseAddress = baseAddress;
        }
        

        
        public IActionResult Index()
        {
            List<ProductModel> productModels = new List<ProductModel>();

            var response = client.GetAsync(client.BaseAddress +"Product").Result;
            if(response.IsSuccessStatusCode)
            {
                var jsonContent = response.Content.ReadAsStringAsync().Result;

                productModels = JsonConvert.DeserializeObject<List<ProductModel>>(jsonContent);
            }
            /*else
            {

            }*/
            return View(productModels);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            var json = JsonConvert.SerializeObject(productModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync(client.BaseAddress + "Product", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}
