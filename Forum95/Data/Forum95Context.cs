using Microsoft.EntityFrameworkCore;
using Forum95.Models;
namespace Forum95.Data
{
    public class Forum95Context:DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<User> Users { get; set; }
        public Forum95Context(DbContextOptions<Forum95Context> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne<User>(s => s.User)
                .WithMany(g => g.Posts)
                .HasForeignKey(s => s.CurrentUserId);

            modelBuilder.Entity<Post>()
                .HasOne<Theme>(s => s.Theme)
                .WithMany(g => g.Posts)
                .HasForeignKey(s => s.CurrentThemeId);
        }

    }
}
