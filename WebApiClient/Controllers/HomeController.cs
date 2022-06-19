using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;//nuget indirildikten sonra eklenmesi gereken kütüphaneler
using System.Net.Http.Headers;//
using WebApiClient.Models;

namespace WebApiClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult List()
        {
            List<Product> products = new List<Product>();

            return View(products);
        }
        [HttpPost]
        public ActionResult List(string Url,string Api)
        {
            List<Product> products = new List<Product>();

            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri(Url);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage mesaj = client.GetAsync(Api).Result;

                if(mesaj.IsSuccessStatusCode)//200 ok()
                {
                    products = mesaj.Content.ReadAsAsync<List<Product>>().Result;
                }
                else
                {
                }
            }
            return View(products);
        }
        [HttpGet]
        public ActionResult Product()
        {
            Product product = new Product();

            return View(product);
        }
        [HttpPost]
        public ActionResult Product(string Url, string Api,int id)
        {
            Product product = new Product();

            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage mesaj = client.GetAsync(Api + id).Result;//api/urunyonetimi/5

                if (mesaj.IsSuccessStatusCode)
                {
                    product = mesaj.Content.ReadAsAsync<Product>().Result;
                }
            }

            return View(product);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string Url, string Api, Product product)
        {
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage mesaj = client.PostAsJsonAsync(Api, product).Result;

                if (mesaj.IsSuccessStatusCode)
                {
                    ViewBag.mesaj = "Ürün Ekleme Başarılı";
                }
                else
                {
                    ViewBag.mesaj = "Ürün Ekleme Başarısız. Beklenmedik bir durum ile karşılaşıldı";
                }
            }


            return View();
        }
        [HttpGet]
        public ActionResult Delete()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Delete(string Url, string Api, int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage mesaj = client.DeleteAsync(Api + id).Result;//api/urunyonetimi/5

                
                    ViewBag.mesaj = mesaj.Content.ReadAsAsync<string>().Result;

            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(Product product)
        {
            
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(string Url, string Api, int id,Product product)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //if(string.IsNullOrEmpty(product.Name))
                HttpResponseMessage mesaj = client.PutAsJsonAsync(Api + id,product).Result;//api/urunyonetimi/5


                ViewBag.mesaj = mesaj.Content.ReadAsAsync<string>().Result;

            }

            return View();
        }
        [HttpGet]
        public ActionResult GetProduct()
        {
            
            return View();

        }
        [HttpPost]
        public ActionResult GetProduct(string Url, string Api, int id)
        {

            Product product = new Product();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage mesaj = client.GetAsync(Api + id).Result;//api/urunyonetimi/5s

                if (mesaj.IsSuccessStatusCode)
                {
                    product = mesaj.Content.ReadAsAsync<Product>().Result;
                }
            }

            return RedirectToAction("Edit", product);
        }
    }
}