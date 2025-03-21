using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuthUi.Models
{
    public class RegisterModel
    {
        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Email")]
        [Required]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
    }
}
