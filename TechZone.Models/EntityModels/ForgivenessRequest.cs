namespace TechZone.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class ForgivenessRequest
    {
        public int Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Your message should be at least 10 characters")]
        [MaxLength(300, ErrorMessage = "Your message shouldn't be longer than 300 characters")]
        public string Message { get; set; }

        public bool IsAnswered { get; set; }

        [Required]
        public string RoomId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}