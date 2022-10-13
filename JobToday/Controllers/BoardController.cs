using JobToday.Data;
using Microsoft.AspNetCore.Mvc;

namespace JobToday.Controllers
{
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _context;
        //constructor to connect the _ context
        //to the DB
        public BoardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tags = _context.Tags.OrderBy(c => c.TagName).ToList();
            return View(tags);
        }
    }
}
