using System.ComponentModel.DataAnnotations;

namespace LoginFormAspCore6.Models
{
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Gender { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = null!;
    }
}
