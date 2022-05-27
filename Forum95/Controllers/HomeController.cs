using Forum95.Models;
using Forum95.Models.ViewModel;
using Forum95.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Forum95.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Forum95Context _ctx;
        public HomeController(ILogger<HomeController> logger, Forum95Context ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }
        [HttpGet]
        public IActionResult Index()
        {           
            var emailIdentity = HttpContext.User.Identity.Name;
            var posts = _ctx.Posts.Include(g=>g.User).Include(g=>g.Theme).ToList();
            var viewModel = new IndexViewModel()
            {
                EmailIdentity = emailIdentity,
                Posts = posts
            };
            return View(viewModel);
        }
        
        public IActionResult Edit(int id)//post id
        {
            var post =_ctx.Posts.Include(g=>g.User).Include(g => g.Theme).FirstOrDefault(g=>g.Id == id);
            if (post == null) return View("Error");
            return View(post);
        }
        //user colection theme
        public IActionResult AddPost()
        {
            var emailIdentity = HttpContext.User.Identity.Name;
            User user = _ctx.Users.FirstOrDefault(g => g.Email == emailIdentity);
            var addPostModel = new AddPostViewModel()
            {
                User = user,
                Post = new Post()
            };
            return View(addPostModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}