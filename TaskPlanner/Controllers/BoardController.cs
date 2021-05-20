using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Models;

namespace TaskPlanner.Controllers
{
    public class BoardController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public BoardController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var db = new TaskPlannerContext())
            {
                IOrderedQueryable<Board> boards = db.Boards
                    .OrderBy(b => b.BoardId);
            }
            return View();
        }

        public IActionResult Add()
        {    
            return View();
        }

        [HttpPost]
        public IActionResult Add(Board board)
        {
            string title = board.Title;
            using (var db = new TaskPlannerContext())
            {
                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Board { Title = title });
                db.SaveChanges();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
