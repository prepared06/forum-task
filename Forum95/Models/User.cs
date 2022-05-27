
namespace Forum95.Models
{
    public class User
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }

        public string Email { get; set; }
        //User has many posts
        public ICollection<Post> Posts { get; set; }
    }
}
