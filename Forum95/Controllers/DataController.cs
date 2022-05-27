using Forum95.Models;
using Forum95.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Forum95.Models.ViewModel;

namespace Forum95.Controllers
{
    public class DataController: Controller
    {
        private readonly ILogger<DataController> _logger;
        private readonly Forum95Context _ctx;
        public DataController(ILogger<DataController> logger, Forum95Context ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [HttpPost]
        public IActionResult Change(Post post)
        {
            var postFromDB = _ctx.Posts.Include(g=>g.Theme).FirstOrDefault(p => p.Id == post.Id);
            if (postFromDB == null) return BadRequest(new { message ="Post doesn't exist" });

            var theme = _ctx.Themes.FirstOrDefault(p => p.Id == post.Theme.Id);
            if (theme == null) return BadRequest(new { message = "Theme doesn't exist" });

            if(theme.ThemeName!=post.Theme.ThemeName)
            {
                Theme newThema = new Theme()
                {
                    ThemeName = post.Theme.ThemeName
                };
                _ctx.Themes.Add(newThema);
                _ctx.SaveChanges();

                postFromDB.Theme = newThema;
            }

            postFromDB.Title = post.Title;
            postFromDB.Text = post.Text;
            
            _ctx.Posts.Update(postFromDB);
            _ctx.SaveChanges();
            return RedirectPermanent("/Home/Index");
        }

        
        public IActionResult DeletePost(int id)
        {
            var postFromDB = _ctx.Posts.FirstOrDefault(p => p.Id == id);
            if (postFromDB == null) return BadRequest(new { message = "Post doesn't exist" });

            _ctx.Posts.Remove(postFromDB);
            _ctx.SaveChanges();
            return RedirectPermanent("/Home/Index");
        }
        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            var theme = _ctx.Themes.FirstOrDefault(p => p.ThemeName == post.Theme.ThemeName);
            if (theme == null)
            {
                var newThema = new Theme() { ThemeName = post.Theme.ThemeName };
                theme=newThema;
            }
            var emailIdentity = HttpContext.User.Identity.Name;
            var user = _ctx.Users.FirstOrDefault(p => p.Email == emailIdentity);

            var newPost = new Post()
            {
                User = user,
                Theme = theme,
                Title = post.Title,
                Text = post.Text,
            };
            _ctx.Posts.Add(newPost);
            _ctx.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
