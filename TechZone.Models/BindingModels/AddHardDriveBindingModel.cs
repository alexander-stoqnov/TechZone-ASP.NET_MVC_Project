namespace TechZone.Models.BindingModels
{
    using Enums;
    using System.ComponentModel.DataAnnotations;

    public class AddHardDriveBindingModel : AddProductBindingModel
    {
        [Range(80, 10000, ErrorMessage = "Hard drive capacity must be between 80 and 10000 Mbs")]
        public int Capacity { get; set; }

        [Display(Name = "Brand")]
        public HardDriveBrandType DriveBrand { get; set; }

        [Display(Name = "Storage Type")]
        public HardDriveType DriveType { get; set; }
    }
}
