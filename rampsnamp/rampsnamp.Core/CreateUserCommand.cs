using System.ComponentModel.DataAnnotations;

namespace rampsnamp.Core
{
    public class CreateUserCommand
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Firstname { get; set; }
    }
}