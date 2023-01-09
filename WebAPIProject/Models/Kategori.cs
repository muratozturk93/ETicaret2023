using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPIProject.Models
{
    public class Kategori
    {
        public int KategoriID { get; set; }
        [DisplayName("Kategori Adı")]
        public string KategoriAdi { get; set; }
    }
}