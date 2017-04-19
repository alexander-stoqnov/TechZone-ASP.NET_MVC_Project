namespace TechZone.Services
{
    using System.Collections.Generic;
    using Models.ViewModels.Customer;
    using System.Linq;
    using AutoMapper;

    public class CustomersService : Service
    {
        public IEnumerable<CustomerPurchaseHistoryViewModel> GetCurrentUserPurchases(string currentUserId)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var customerPurchases = customer.Purchases;
            return Mapper.Instance.Map<IEnumerable<CustomerPurchaseHistoryViewModel>>(customerPurchases);
        }
    }
}