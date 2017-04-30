namespace TechZone.Services.Contracts
{
    using System.Collections.Generic;
    using Models.BindingModels;
    using Models.ViewModels.Admin;

    public interface IAdminService
    {
        UserRolesViewModel GetAllUsersAndRoles();
        void ChangeUserRoles(string id);
        IEnumerable<ManageProductViewModel> GetProductsToManage();
        bool ProductExists(int id);
        bool DisableEnableSelectedProduct(int id);
        EditProductViewModel GetProductToEditDetails(int id);
        void EditProductInfo(EditProductViewModel epbm);
        void AddNewHardDrive(AddHardDriveBindingModel ahdbm);
        void AddNewGraphicCard(AddGraphicCardBindingModel agcbm);
        void AddNewProcessor(AddProcessorBindingModel apbm);
    }
}