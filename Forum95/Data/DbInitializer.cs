using Forum95.Models;
namespace Forum95.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Forum95Context context)
        {
            if (context.Posts.Any()
                && context.Themes.Any()
                && context.Users.Any())
            {
                return;   // DB has been seeded
            }
            //creating users
            User prepared = new User()
            {
                UserName = "prepared",
                Email = "prepared@forum95.com"
            };
            User ScerRial = new User()
            {
                UserName = "ScerRial",
                Email = "ScerRial@gmail.com"
            };
            //creating themes
            Theme coding = new Theme()
            {
                ThemeName = "coding"
            };
            Theme gaming = new Theme()
            {
                ThemeName = "gaming"
            };
            Theme music = new Theme()
            {
                ThemeName = "music"
            };
            //creating posts
            Post LoremIpsum = new Post()
            {
                Title = "Lorem Ipsum post",
                Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                User = prepared,
                Theme = coding
            };
            Post CocktailsforTwo = new Post()
            {
                Title = "Cocktails for Two",
                Text = "In some secluded rendezvous,\nThat overlooks the avenue,\nWith someone sharing a delightful chat,\nOf this and that,\nAnd cocktails for two.",
                User = prepared,
                Theme = music
            };
            Post IllaoiQuotes = new Post()
            {
                Title = "Illaoi Quotes",
                Text= "If I hate something, I destroy it. If I want something, I take it.\nI am a teacher. Bilgewater will learn.\nThere is joy in food... and fighting.\nI value truth - and barbecue.",
                User =ScerRial,
                Theme = gaming
            };
            Post DRY = new Post()
            {
                Title = "Main coding advice",
                Text = "Don't repeat yourself",
                User = ScerRial,
                Theme = coding
            };

            context.Users.Add(ScerRial);
            context.Users.Add(prepared);

            context.Themes.Add(coding);
            context.Themes.Add(gaming);
            context.Themes.Add(music);

            context.Posts.Add(LoremIpsum);
            context.Posts.Add(CocktailsforTwo);
            context.Posts.Add(IllaoiQuotes);
            context.Posts.Add(DRY);

            context.SaveChanges();
        }
    }
}
