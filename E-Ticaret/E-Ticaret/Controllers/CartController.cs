using E_Ticaret.Entity;
using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class CartController : Controller
    {
        private DataContext db=new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }
        public ActionResult AddToCart(int Id)
        {
            var product= db.Products.FirstOrDefault(i=>i.Id==Id);
            if(product!=null) //veritabanında ürün varsa
            {
                GetCart().AddProduct(product,1);
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);
            if (product != null) //veritabanında ürün varsa
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }
        public Cart GetCart()
        {
            var cart =(Cart) Session["Cart"];
            if (cart == null) {   //sepet boşsa yeni sepet açacak
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart; //cart mevcutsa kullanıcı eskisinden devam edecek
        }
        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }
        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var cart = GetCart();
            if(cart.CartLines.Count==0) // sepette ürün yoksa
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün yok");
            }
            if (ModelState.IsValid) //form eksiksiz doldurulduysa siparişi veritabanına kaydet
            {
                SaveOrder(cart, entity);
                cart.Clear();
                return View("Completed"); //sipariş tamamlandı viewı çıkaracak
            }
            else
            {
                return View(entity); //entity tekrar gönderilecek ve form tekrardan gelecek
            }
           
        }

        private void SaveOrder(Cart cart, ShippingDetails entity) //veritabanı kayıt işlemi 
        {
            var order=new Order();
            order.OrderNumber = (new Random()).Next(111111,999999).ToString(); /*belli aralıklarda random sayı üretip bunu sipariş numarası yapacak*/
            order.Total = cart.Total();
            order.OrderDate=DateTime.Now;
            order.FullName = entity.FullName;
            order.AdresBasligi = entity.AdresBasligi;
            order.Adres=entity.Adres;
            order.Sehir = entity.Sehir;
            order.Semt = entity.Semt;
            order.Mahalle = entity.Mahalle;
            order.PostaKodu= entity.PostaKodu;
            order.Orderlines = new List<OrderLine>();
            foreach (var pr in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = pr.Quantity;
                orderline.Price=pr.Quantity*pr.Product.Price;
                orderline.ProductId=pr.Product.Id;
                order.Orderlines.Add(orderline); //orderları ekle
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}