using System.IO;

namespace TechZone.Services
{
    using System.Collections.Generic;
    using Models.ViewModels.Customer;
    using System.Linq;
    using AutoMapper;
    using Dropbox.Api;

    public class CustomersService : Service
    {
        public IEnumerable<CustomerPurchaseHistoryViewModel> GetCurrentUserPurchases(string currentUserId)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var customerPurchases = customer.Purchases;
            return Mapper.Instance.Map<IEnumerable<CustomerPurchaseHistoryViewModel>>(customerPurchases);
        }

        public void UploadUserProfilePicture(string currentUserId, string dropboxKey, byte[] file)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            Upload(new DropboxClient(dropboxKey), $"/Users/{customer.User.UserName}/ProfilePicture", "pesh.jpg", file);
        }
    }
}