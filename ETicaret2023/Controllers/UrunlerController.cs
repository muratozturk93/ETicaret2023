using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ETicaret2023.Models;
using Newtonsoft.Json;

namespace ETicaret2023.Controllers
{
    public class UrunlerController : Controller
    {
        private ETicaretEntities1 db = new ETicaretEntities1();
        HttpClient client= new HttpClient();

        // GET: Urunler
        public ActionResult Index()
        {
            //var urunler = db.Urunler.Include(u => u.Kategoriler);
            //return View(urunler.ToList());

            List<Urunler> urunler = null;
            client.BaseAddress = new Uri("https://localhost:44341/api/");

            var response = client.GetAsync("Urun");

            response.Wait();
            var result =response.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsStringAsync();
                data.Wait();
                urunler = JsonConvert.DeserializeObject<List<Urunler>>(data.Result);
            }

            for (int i = 0; i < urunler.Count; i++)
            {
                urunler[i].Kategoriler = db.Kategoriler.Find(urunler[i].KategoriID);
            }
            return View(urunler);

        }

        // GET: Urunler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunler/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Urunler urunler,HttpPostedFileBase UrunResim)
        {
            if (ModelState.IsValid)
            {
                db.Urunler.Add(urunler);
                db.SaveChanges();

                if (UrunResim!=null)
                {
                    string dosya = Path.Combine(Server.MapPath("~/Resim"), urunler.UrunID + ".jfif");
                    UrunResim.SaveAs(dosya);
                }

                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", urunler.KategoriID);
            return View(urunler);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", urunler.KategoriID);
            return View(urunler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Urunler urunler, HttpPostedFileBase UrunResim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();

                if (UrunResim != null)
                {
                    string dosya = Path.Combine(Server.MapPath("~/Resim"), urunler.UrunID + ".jfif");
                    UrunResim.SaveAs(dosya);
                }
                return RedirectToAction("Index");
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", urunler.KategoriID);
            return View(urunler);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
            db.SaveChanges();

            string dosya = Path.Combine(Server.MapPath("~/Resim"), id + ".jfif");

            FileInfo fi = new FileInfo(dosya);
            if (fi.Exists)
            {
                fi.Delete();
            }


            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
