namespace TechZone.Models.ViewModels.Moderator
{
    using System.ComponentModel.DataAnnotations;

    public class UserWarningsViewModel
    {
        [Display(Name = "Room")]
        public string RoomId { get; set; }

        [Display(Name = "Username")]
        public string WarnedUserUsername { get; set; }

        [Display(Name = "Warnings")]
        public int CountOfWarnings { get; set; }
    }
}