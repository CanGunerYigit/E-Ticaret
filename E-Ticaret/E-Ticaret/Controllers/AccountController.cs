using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Ticaret.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using E_Ticaret.Models;
using Microsoft.Owin.Security;

namespace E_Ticaret.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager=new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager=new RoleManager<ApplicationRole>(roleStore); 
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid) 
            { 
                //Kayıt işlemleri
                var user=new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName=model.Username;

                var result=UserManager.Create(user, model.Password); //oluşturulan bilgileri usermanagere gönder
                if (result.Succeeded) //kayıt işlemi gerçekleşmiş demek
                {
                    //kullanıcı oluştu ve role ata
                    if (RoleManager.RoleExists("user")) //böyle bir rol var ise
                    {
                        UserManager.AddToRole(user.Id, "user"); //oluşturulan kullanıcıya user rolü ata
                    }
                    return RedirectToAction("Login", "Account"); //kullanıcıyı login sayfasına gönder
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError","Kullanıcı Oluşturma Hatası");
                }
            }
            return View(model); //kullandığı bilgiler tekrar karşısına gelir sıfırdan form doldurmaz
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                //Login işlemleri
                var user=UserManager.Find(model.Username,model.Password); //kullanıcıyı bul

                if (user != null)  //kullanıcı veritabanında varsa sisteme dahil et
                {
                    //ApplicationCookie oluştur ve sisteme bırak
                    var authManager = HttpContext.GetOwinContext().Authentication;

                    var identityClaims=UserManager.CreateIdentity(user,"ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties,identityClaims);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Kullanıcı Bulunamadı");

                }

            }
            return View(model); //kullandığı bilgiler tekrar karşısına gelir sıfırdan form doldurmaz
        }
        public ActionResult Logout()
        {
            var authManager =HttpContext.GetOwinContext().Authentication; //oluşturulan cookie yi sistemden sil
            authManager.SignOut();
            return RedirectToAction("Index","Home");
        }

    }
}