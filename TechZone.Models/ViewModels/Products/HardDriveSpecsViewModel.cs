namespace TechZone.Models.ViewModels.Products
{
    using Enums;

    public class HardDriveSpecsViewModel
    {
        public int Capacity { get; set; }

        public HardDriveBrandType DriveBrand { get; set; }

        public HardDriveType DriveType { get; set; }
    }
}