using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class Register
    {
        [Required] //zorunlu
        [DisplayName("Adınız")]//gösterilecek alan
        public string Name { get; set; }
        [Required] //zorunlu
        [DisplayName("Soyadınız")]//gösterilecek alan
        public string Surname { get; set; }
        [Required] //zorunlu
        [DisplayName("Kullanıcı Adınız")]//gösterilecek alan
        public string Username { get; set; }
       
        [Required] //zorunlu
        [DisplayName("Eposta")]//gösterilecek alan
        [EmailAddress]//email formatı olsun
        public string Email { get; set; }
        [Required] //zorunlu
        [DisplayName("Şifre")]//gösterilecek alan
        public string Password { get; set; }
        [Required] //zorunlu
        [DisplayName("Şifre Tekrarı")]//gösterilecek alan
        [Compare("Password",ErrorMessage="Şifreleriniz uyuşmuyor")] //şifreyle kıyasla aynı değilse hata mesajı
        public string RePassword { get; set; }
    }
}