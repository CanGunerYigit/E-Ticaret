using E_Ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class Cart
    {
        private List<CartLine> _cardLines = new List<CartLine>();
        public List<CartLine> CartLines
        {
            get 
            { 
                return _cardLines; 
            }
             
        }
        public void AddProduct(Product product,int quantity)
        {
            var line = _cardLines.FirstOrDefault(i => i.Product.Id == product.Id);
            if(line==null) //eklenmek istenen eleman yok demek
            {
                _cardLines.Add(new CartLine() { Product=product,Quantity=quantity}); //adet miktarı kadar eleman ekle
            }
            else
            {
                line.Quantity += quantity; //sepette bu ürün varsa ekstradan üzerine eklensin
            }
        }
        public void DeleteProduct(Product product)
        {
            _cardLines.RemoveAll(i => i.Product.Id == product.Id); //ürün sil
        }
        public double Total()
        {
            return _cardLines.Sum(i=>i.Product.Price*i.Quantity);
        }
        public void Clear()//hepsini silecek
        { 
            _cardLines.Clear();
        }

    }
    public class CartLine //her bir ürünü temsil eden objeler
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}