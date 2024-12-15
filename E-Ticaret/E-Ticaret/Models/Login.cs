using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class Login
    {
        [Required] //zorunlu
        [DisplayName("Kullanıcı Adınız")]//gösterilecek alan
        public string Username { get; set; }

        [Required] //zorunlu
        [DisplayName("Şifre")]//gösterilecek alan
        public string Password { get; set; }
        
        [DisplayName("Beni Hatırla")]//gösterilecek alan
        public bool RememberMe { get; set; }
    }
}