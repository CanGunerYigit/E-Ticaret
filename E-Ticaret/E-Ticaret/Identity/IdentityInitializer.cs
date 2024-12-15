using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace E_Ticaret.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext> //database yoksa oluştursun
    {
        protected override void Seed(IdentityDataContext context)
        {
            //rolleri oluştur
            //userları oluştur
            if (!context.Roles.Any(i => i.Name == "admin")) //admin isminde bir rol yoksa
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager=new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name="admin",Description="admin rolü"}; //yeni rol oluşturuyoruz
                manager.Create(role);
            }
            if (!context.Roles.Any(i => i.Name == "user")) //admin isminde bir rol yoksa
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() {Name="user",Description="user rolü" }; //yeni rol oluşturuyoruz
                manager.Create(role);
            }
            if (!context.Users.Any(i => i.Name == "can")) //admin isminde bir rol yoksa oluştur
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() {Name="can",Surname="yiğit",UserName="canyigit",Email="can@gmail.com" }; //yeni rol oluşturuyoruz
                
                manager.Create(user,"123"); //parola oluşturduk
                manager.AddToRole(user.Id, "admin"); //roller atadık
                manager.AddToRole(user.Id, "user");
            }
            if (!context.Users.Any(i => i.Name == "ali")) //admin isminde bir rol yoksa oluştur
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "ali", Surname = "yiğit", UserName = "aliyigit", Email = "ali@gmail.com" }; //yeni rol oluşturuyoruz

                manager.Create(user, "123"); //parola oluşturduk
                 //roller atadık
                manager.AddToRole(user.Id, "user");
            }
            base.Seed(context);
        }
    }
}