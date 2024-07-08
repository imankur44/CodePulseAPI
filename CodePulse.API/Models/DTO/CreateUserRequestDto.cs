using System.ComponentModel.DataAnnotations;

namespace CodePulse.API.Models.DTO
{
    public class CreateUserRequestDto
    {
        public Guid id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        public DateTime dateOfJoining { get; set; }
    }
}
