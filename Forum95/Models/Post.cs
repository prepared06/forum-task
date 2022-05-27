using System.ComponentModel.DataAnnotations;

namespace Forum95.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Your post")]
        public string Text { get; set; }
        //post has one user
        public int CurrentUserId { get; set; }
        public User User { get; set; }
        //post has one theme
        public int CurrentThemeId { get; set; }
        public Theme Theme { get; set; }
    }
}
