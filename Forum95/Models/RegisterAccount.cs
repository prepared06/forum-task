using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum95.Models
{
    [NotMapped]
    public class RegisterAccount
    {
        [Required(ErrorMessage = "Enter please your nuckname")]
        [Display(Name ="Your nickname")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter please your email")]
        public string Email { get; set; }
    }
}
