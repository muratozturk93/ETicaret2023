﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebAPIProject.Models;


namespace WebAPIProject.Controllers
{
    public class KategoriController : ApiController
    {
        // GET Listeleme
        // POST Veri gönderimi
        // PUT Update etmek için kullanılır
        // DELETE Silme işlemleri

        ETicaretEntities db = new ETicaretEntities();

        [System.Web.Http.HttpGet]
        public List<Kategoriler> Get()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Kategoriler> liste = db.Kategoriler.ToList();

            return liste;
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult Get(int id)
        {
            Kategoriler kategori = db.Kategoriler.Find(id);

            Kategori kat = new Kategori()
            {
                KategoriID = kategori.KategoriID,
                KategoriAdi = kategori.KategoriAdi
            };
            return Ok(kat);
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Post([FromBody]Kategoriler kategori) 
        {
            db.Kategoriler.Add(kategori);
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Put([FromBody] Kategoriler kategori)    // UPDATE
        {
            db.Entry(kategori).State=System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            Kategoriler kategori = db.Kategoriler.Find(id);
            db.Kategoriler.Remove(kategori);
            db.SaveChanges();
            return Ok();
        }



    }
}
