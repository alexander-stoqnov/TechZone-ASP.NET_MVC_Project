using System;

namespace TechZone.Services
{
    using System.Collections.Generic;
    using Models.ViewModels.Customer;
    using System.Linq;
    using AutoMapper;
    using Dropbox.Api;

    public class CustomersService : Service
    {
        public CustomerProfileViewModel GetCurrentUserProfile(string currentUserId, string dropboxKey)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            var customerProfileVm = Mapper.Instance.Map<CustomerProfileViewModel>(customer);
            customerProfileVm.PurchasesHistory = Mapper.Instance.Map<ICollection<CustomerPurchaseHistoryViewModel>>(customer.Purchases);

            customerProfileVm.Credits = customer.Credits;
            customerProfileVm.ImageData = this.GetUserProfilePicture(customer.User.ProfilePictureFileName, customer.User.UserName, dropboxKey);

            return customerProfileVm;
        }

        private string GetUserProfilePicture(string profilePictureFileName, string userUserName, string dropboxKey)
        {
            var imageByteData = this.DownloadAsync(new DropboxClient(dropboxKey), $"Users/{userUserName}/ProfilePicture", profilePictureFileName);            
            string imageBase64Data = Convert.ToBase64String(imageByteData.Result);
            return $"data:image/jpg;base64,{imageBase64Data}";
        }

        public void UploadUserProfilePicture(string currentUserId, string dropboxKey, string fileName, byte[] file)
        {
            var customer = this.Context.Customers.First(c => c.UserId == currentUserId);
            customer.User.ProfilePictureFileName = fileName;
            this.Context.SaveChanges();
            Upload(new DropboxClient(dropboxKey), $"/Users/{customer.User.UserName}/ProfilePicture", fileName, file);
        }


    }
}