namespace TechZone.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;

    [Table("HardDrives")]
    public class HardDrive : Product
    {
        public int Capacity { get; set; }

        public HardDriveBrandType DriveBrand { get; set; }

        public HardDriveType DriveType { get; set; }
    }
}