using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class ShippingDetails
    {
        public string FullName { get; set; }
        [Required(ErrorMessage ="Lütfen adres giriniz") ]
        public string AdresBasligi { get; set; }
        [Required(ErrorMessage = "Lütfen bir adres giriniz")]

        public string Adres { get; set; }
        [Required(ErrorMessage = "Lütfen Şehir giriniz")]

        public string Sehir {  get; set; }
        [Required(ErrorMessage = "Lütfen Semt giriniz")]

        public string Semt { get; set; }
        [Required(ErrorMessage = "Lütfen Mahalle giriniz")]

        public string Mahalle { get; set; }
        [Required(ErrorMessage = "Lütfen Posta Kodu giriniz")]

        public string PostaKodu { get; set; }
    }
}