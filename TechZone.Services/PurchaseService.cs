using System.Linq;

namespace TechZone.Services
{
    using Models.EntityModels;

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

        public int ProductsInCartForLoggedInUser(string currentUserId)
        {
            ShoppingCart cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.Customer.Id == currentUserId);
            if (cart == null)
            {
                return 0;
            }
            return cart.Products.Count;
        }

        public int ProductsInCartForGuest(string sessionId)
        {
            ShoppingCart cart = this.Context.ShoppingCarts.FirstOrDefault(sc => sc.SessionId == sessionId);
            if (cart == null)
            {
                return 0;
            }
            return cart.Products.Count;
        }
    }
}