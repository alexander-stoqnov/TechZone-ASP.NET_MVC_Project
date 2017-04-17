using System;

namespace TechZone.Services
{
    using System.Linq;
    using Models.EntityModels;
    using System.Collections.Generic;
    using AutoMapper;
    using Models.ViewModels.Purchase;

    public class PurchaseService : Service
    {
        public bool ProductExists(int id)
        {
            return this.Context.Products.Find(id) != null;
        }

        public void AddProductForCurrentSession(int id, string sessionId)
        {
            var product = this.Context.Products.Find(id);
            var shoppingCart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.SessionId == sessionId);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    SessionId = sessionId
                };
                this.Context.ShoppingCarts.Add(shoppingCart);
            }
            shoppingCart.Products.Add(product);
            this.Context.SaveChanges();
        }

        public void AddProductToShoppingCart(int id, string userId)
        {
            var product = this.Context.Products.Find(id);
            var user = this.Context.Users.Find(userId);
            var shoppingCart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == userId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    Customer = user
                };
                this.Context.ShoppingCarts.Add(shoppingCart);
            }

            shoppingCart.Products.Add(product);
            this.Context.SaveChanges();
        }

        public int GetNumberOfItemsInCart(string currentUserId, string sessionId)
        {
            ShoppingCart cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == currentUserId);
            if (cart != null)
            {
                return cart.Products.Count;
            }

            cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.SessionId == sessionId);
            if (cart != null)
            {
                return cart.Products.Count;
            }
            return 0;
        }

        public ShoppingCartViewModel GetCartItems(string userId, string sessionId)
        {
            ShoppingCartViewModel scvm = new ShoppingCartViewModel { Id = 0 };

            ShoppingCart cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == userId);
            if (cart != null)
            {
                scvm = new ShoppingCartViewModel
                {
                    Id = cart.Id,
                    ProductsInCart = GetFinalProductInCartInfo(cart.Products)
                };
                scvm.FinalPriceWithoutDiscount = scvm.ProductsInCart.Sum(pr => pr.Price);
                scvm.FinalPriceWithDiscount = scvm.ProductsInCart.Sum(pr => pr.FinalPrice);
                return scvm;
            }
            cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.SessionId == sessionId);
            if (cart != null)
            {
                scvm = new ShoppingCartViewModel
                {
                    Id = cart.Id,
                    ProductsInCart = GetFinalProductInCartInfo(cart.Products)
                };
            }
            scvm.FinalPriceWithoutDiscount = scvm.ProductsInCart.Sum(pr => pr.Price);
            scvm.FinalPriceWithDiscount = scvm.ProductsInCart.Sum(pr => pr.FinalPrice);
            return scvm;
        }

        private IEnumerable<ProductInCartViewModel> GetFinalProductInCartInfo(IEnumerable<Product> products)
        {
            var productsInCartVms = new HashSet<ProductInCartViewModel>();

            foreach (var product in products)
            {
                var productVm = Mapper.Map<ProductInCartViewModel>(product);
                productVm.FinalPrice = CalculateFinalPrice(product.Discount, product.Price);
                productsInCartVms.Add(productVm);
            }

            return productsInCartVms;
        }

        private decimal CalculateFinalPrice(int discount, decimal price)
        {
            decimal discountFinal = discount / 100.0m;
            return price - price * discountFinal;
        }

        public bool IsShoppingCartForRegisteredUser(string userId)
        {
            return this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == userId) != null;
        }

        public void RemoveProductFromCart(string currentUserId, string sessionId, int productId)
        {
            var cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.SessionId == sessionId);
            if (cart == null)
            {
                cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == currentUserId);
            }
            var productToRemove = cart.Products.FirstOrDefault(pr => pr.Id == productId);
            cart.Products.Remove(productToRemove);
            this.Context.SaveChanges();
        }

        public FinalCheckoutViewModel CalculatePriceWithoutShipment(string currentUserId)
        {
            var customer = this.Context.Customers.First(c => c.User.Id == currentUserId);
            var cart = this.Context.ShoppingCarts.First(c => c.Customer.Id == currentUserId);
            var finalCheckoutVm = new FinalCheckoutViewModel { CurrentCustomerBalance = customer.Credits };
            foreach (var product in cart.Products)
            {
                finalCheckoutVm.FinalPrice += this.CalculateFinalPrice(product.Discount, product.Price);
            }
            return finalCheckoutVm;
        }

        public bool EnoughCredits(string currentUserId, decimal totalPrice)
        {
            var customer = this.Context.Customers.First(c => c.User.Id == currentUserId);
            return customer.Credits > totalPrice;
        }

        public void FinalizePurchase(string currentUserId, decimal finalPrice)
        {
            var customer = this.Context.Customers.First(c => c.User.Id == currentUserId);
            var cart = this.Context.ShoppingCarts.First(c => c.Customer.Id == currentUserId);

            Purchase purchase = new Purchase
            {
                Customer = customer,
                PurchaseDate = DateTime.Now,
            };

            foreach (var product in cart.Products)
            {
                purchase.Products.Add(product);
            }

            customer.Credits -= finalPrice;
            this.Context.Purchases.Add(purchase);
            this.Context.ShoppingCarts.Remove(cart);
            this.Context.SaveChanges();
        }
    }
}