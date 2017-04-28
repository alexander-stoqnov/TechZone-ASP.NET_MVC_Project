namespace TechZone.Services
{
    using System.Collections.Generic;
    using Models.ViewModels.Customer;
    using System.Linq;
    using AutoMapper;
    using Dropbox.Api;
    using System;

    public class CustomersService : Service
    {
        public CustomerProfileViewModel GetCurrentUserProfile(string currentUserId, string dropboxKey)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var customerProfileVm = Mapper.Instance.Map<CustomerProfileViewModel>(customer);
            customerProfileVm.PurchasesHistory = Mapper.Instance.Map<ICollection<CustomerPurchaseHistoryViewModel>>(customer.Purchases);

            customerProfileVm.Credits = customer.Credits;
            if (customer.User.ProfilePictureFileName != null)
            {
                customerProfileVm.ImageData = this.GetUserProfilePicture(customer.User.ProfilePictureFileName, customer.User.UserName, dropboxKey);
            }
            return customerProfileVm;
        }

        private string GetUserProfilePicture(string profilePictureFileName, string userUserName, string dropboxKey)
        {
            var imageByteData = this.DownloadAsync(new DropboxClient(dropboxKey), $"Users/{userUserName}/ProfilePicture", profilePictureFileName);
            string imageBase64Data = Convert.ToBase64String(imageByteData.Result);
            return $"data:image/*;base64,{imageBase64Data}";
        }

        public void UploadUserProfilePicture(string currentUserId, string dropboxKey, string fileName, byte[] file)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            customer.User.ProfilePictureFileName = fileName;
            this.Context.SaveChanges();
            Upload(new DropboxClient(dropboxKey), $"/Users/{customer.User.UserName}/ProfilePicture", fileName, file);
        }

        public bool OrderBellongsToCurrentUser(string currentUserId, int id)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var purchase = this.Context.Purchases.Find(id);
            return purchase?.Customer.Id == customer.Id;
        }

        public byte[] DownloadOrderInvoice(string currentUserId, int id, string dropboxKey)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var purchase = this.Context.Purchases.Find(id);
            string orderFileName = $"{customer.User.UserName}_{purchase.PurchaseDate.ToString("yyyyMMdd")}_{purchase.Id.ToString("00000000")}.pdf";
            var imageByteData = this.DownloadAsync(new DropboxClient(dropboxKey), $"Users/{customer.User.UserName}/Orders", orderFileName);
            return imageByteData.Result;
        }
    }
}