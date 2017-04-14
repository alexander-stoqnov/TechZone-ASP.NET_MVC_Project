namespace TechZone.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using Models.ViewModels.Admin;

    public class AdminService : Service
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
    }
}