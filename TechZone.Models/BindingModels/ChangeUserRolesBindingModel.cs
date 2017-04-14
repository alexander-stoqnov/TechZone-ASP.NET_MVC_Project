namespace TechZone.Models.BindingModels
{
    using System.Collections.Generic;

    public class ChangeUserRolesBindingModel
    {
        public string Id { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}