namespace TechZone.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddCommentBindingModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Please write at least 10 characters.")]
        [MaxLength(400, ErrorMessage = "Your comment cannot contain more than 400 characters.")]
        public string Comment { get; set; }
    }
}