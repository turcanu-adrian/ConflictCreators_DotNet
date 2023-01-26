using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.User
{
    public class UserRegisterDto
    {
        [Required]
        [MaxLength(30)]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(6)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(6)]
        public string DisplayName { get; set; }
    }
}
