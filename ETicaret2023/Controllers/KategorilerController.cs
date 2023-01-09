using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ETicaret2023.Models;
using Newtonsoft.Json;

namespace ETicaret2023.Controllers
{
    public class KategorilerController : Controller
    {
        HttpClient client= new HttpClient();
        //ETicaretEntities1 db = new ETicaretEntities1();
        public ActionResult Index()
        {
            List<Kategoriler> kategoriler = new List<Kategoriler>();
            client.BaseAddress = new Uri("https://localhost:44341/api/");

            var cevap = client.GetAsync("Kategori");
            cevap.Wait();
            var sonuc = cevap.Result;

            if (sonuc.IsSuccessStatusCode)
            {
                var data = sonuc.Content.ReadAsStringAsync();
                data.Wait();
                kategoriler = JsonConvert.DeserializeObject<List<Kategoriler>>(data.Result);
            }

            return View(kategoriler);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Kategoriler kategoriler = db.Kategoriler.Find(id);

            Kategoriler kategoriler = KategoriBul(id);  // Aşağıda metot olarak olusturduk.

            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        private Kategoriler KategoriBul(int? id)
        {
            Kategoriler kategoriler = null;
            client.BaseAddress = new Uri("https://localhost:44341/api/");
            var response = client.GetAsync("Kategori/" + id);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var gelendata = result.Content.ReadAsAsync<Kategoriler>();
                gelendata.Wait();
                kategoriler = gelendata.Result;
            }

            return kategoriler;
        }

        // GET: Kategoriler/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                client.BaseAddress = new Uri("https://localhost:44341/api/");

                var response = HttpClientExtensions.PostAsJsonAsync<Kategoriler>(client, "Kategori", kategoriler);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                //db.Kategoriler.Add(kategoriler);
                //db.SaveChanges();
                return View(kategoriler);
            }

            return View(kategoriler);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = KategoriBul(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                client.BaseAddress = new Uri("https://localhost:44341/api/");

                var response = client.PutAsJsonAsync<Kategoriler>("Kategori", kategoriler);
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                //db.Entry(kategoriler).State = EntityState.Modified;
                //db.SaveChanges();
            }
            return View(kategoriler);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = KategoriBul(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            client.BaseAddress = new Uri("https://localhost:44341/api/");
            var response = client.DeleteAsync("Kategori/" + id);
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }


            //Kategoriler kategoriler = db.Kategoriler.Find(id);
            //db.Kategoriler.Remove(kategoriler);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
