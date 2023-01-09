//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ETicaret2023.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urunler()
        {
            this.Sepet = new HashSet<Sepet>();
            this.SiparisDetay = new HashSet<SiparisDetay>();
        }
    
        public int UrunID { get; set; }
        [DisplayName("�r�n Ad�")]
        public string UrunAdi { get; set; }
        public int KategoriID { get; set; }
        [DisplayName("�r�n A��klamas�")]
        public string UrunAciklamasi { get; set; }
        [DisplayName("�r�n Fiyat�")]
        public int UrunFiyati { get; set; }
    
        public virtual Kategoriler Kategoriler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sepet> Sepet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiparisDetay> SiparisDetay { get; set; }
    }
}
