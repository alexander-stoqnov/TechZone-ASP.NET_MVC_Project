namespace TechZone.Services
{
    using System.Collections.Generic;
    using Models.ViewModels.Customer;
    using System.Linq;
    using AutoMapper;
    using Dropbox.Api;

    public class CustomersService : Service
    {
        public CustomerProfileViewModel GetCurrentUserProfile(string currentUserId)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var customerProfileVm = Mapper.Instance.Map<CustomerProfileViewModel>(customer);
            customerProfileVm.PurchasesHistory = Mapper.Instance.Map<ICollection<CustomerPurchaseHistoryViewModel>>(customer.Purchases);

            customerProfileVm.Credits = customer.Credits;

            return customerProfileVm;
        }

        public void UploadUserProfilePicture(string currentUserId, string dropboxKey, byte[] file)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            Upload(new DropboxClient(dropboxKey), $"/Users/{customer.User.UserName}/ProfilePicture", "pesh.jpg", file);
        }
    }
}