using Forum95.Models;
namespace Forum95.Models.ViewModel
{
    public class IndexViewModel
    {
        public string EmailIdentity { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
