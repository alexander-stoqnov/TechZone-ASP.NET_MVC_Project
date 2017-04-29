namespace TechZone.Services.Contracts
{
    using Models.ViewModels.Customer;

    public interface ICustomersService
    {
        CustomerProfileViewModel GetCurrentUserProfile(string currentUserId, string dropboxKey);
        void UploadUserProfilePicture(string currentUserId, string dropboxKey, string fileName, byte[] file);
        bool OrderBellongsToCurrentUser(string currentUserId, int id);
        byte[] DownloadOrderInvoice(string currentUserId, int id, string dropboxKey);
    }
}