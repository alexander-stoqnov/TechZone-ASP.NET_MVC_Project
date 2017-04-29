namespace TechZone.Services.Contracts
{
    using Models.EntityModels;
    using Models.ViewModels.Purchase;

    public interface IPurchaseService
    {
        bool ProductExists(int id);
        void AddProductForCurrentSession(int id, string sessionId);
        void AddProductToShoppingCart(int id, string userId);
        int GetNumberOfItemsInCart(string currentUserId, string sessionId);
        ShoppingCartViewModel GetCartItems(string userId, string sessionId);
        bool IsShoppingCartForRegisteredUser(string userId);
        void RemoveProductFromCart(string currentUserId, string sessionId, int productId);
        FinalCheckoutViewModel CalculatePriceWithoutShipment(string currentUserId);
        bool EnoughCredits(string currentUserId, decimal totalPrice);
        void FinalizePurchase(string currentUserId, decimal finalPrice, string dropboxKey);
        bool ContainsItemsNotInStock(string currentUserId);

        /// <summary>
        /// Helper Method to Generate Pdfs
        /// </summary>
        byte[] CreatePdf(Purchase purchase);
    }
}