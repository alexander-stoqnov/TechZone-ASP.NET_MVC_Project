using TechZone.Services.Contracts;

namespace TechZone.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using Models.ViewModels.Admin;
    using Models.BindingModels;

    public class AdminService : Service, IAdminService
    {
        public UserRolesViewModel GetAllUsersAndRoles()
        {
            UserRolesViewModel urvm = new UserRolesViewModel();
            var users = this.Context.Users.ToList();
            var roles = this.Context.Roles.ToList();

            foreach (var user in users)
            {
                var userVm = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.FullName,
                    Roles = roles.Where(role => user.Roles.Any(r => r.RoleId == role.Id)).Select(r => r.Name)
                };
                urvm.Users.Add(userVm);
            }

            urvm.Roles = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(roles);

            return urvm;
        }

        public void ChangeUserRoles(string id)
        {
            ApplicationUser user = this.Context.Users.Find(id);
            user.Roles.Clear();
            this.Context.SaveChanges();
        }

        public IEnumerable<ManageProductViewModel> GetProductsToManage()
        {
            var products = this.Context.Products;
            return Mapper.Instance.Map<IEnumerable<ManageProductViewModel>>(products);
        }

        public bool ProductExists(int id)
        {
            return this.Context.Products.Find(id) != null;
        }

        public void DisableEnableSelectedProduct(int id)
        {
            var productToDisable = this.Context.Products.Find(id);
            productToDisable.IsAvailable = !productToDisable.IsAvailable;         
            this.Context.SaveChanges();
        }

        public EditProductViewModel GetProductToEditDetails(int id)
        {
            var product = this.Context.Products.Find(id);
            return Mapper.Instance.Map<EditProductViewModel>(product);
        }

        public void EditProductInfo(EditProductViewModel epbm)
        {
            var product = this.Context.Products.Find(epbm.Id);
            product.Description = epbm.Description;
            product.Discount = epbm.Discount;
            product.ImageUrl = epbm.ImageUrl;
            product.Name = epbm.Name;
            product.Price = epbm.Price;
            product.Quantity = epbm.Quantity;

            this.Context.SaveChanges();
        }

        public void AddNewHardDrive(AddHardDriveBindingModel ahdbm)
        {
            HardDrive hardDrive = Mapper.Instance.Map<HardDrive>(ahdbm);
            this.Context.HardDrives.Add(hardDrive);
            this.Context.SaveChanges();
        }

        public void AddNewGraphicCard(AddGraphicCardBindingModel agcbm)
        {
            GraphicCard graphicCard = Mapper.Instance.Map<GraphicCard>(agcbm);
            this.Context.GraphicCards.Add(graphicCard);
            this.Context.SaveChanges();
        }

        public void AddNewProcessor(AddProcessorBindingModel apbm)
        {
            Processor processor = Mapper.Instance.Map<Processor>(apbm);
            this.Context.Processors.Add(processor);
            this.Context.SaveChanges();
        }
    }
}