using System.ComponentModel.DataAnnotations;
namespace Forum95.Models
{
    public class Theme
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Theme")]
        public string ThemeName { get; set; }
        //Theme has many posts
        public ICollection<Post> Posts { get; set; }
    }
}
