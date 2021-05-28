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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaskPlannerContext _context;

        public HomeController(ILogger<HomeController> logger, TaskPlannerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            using (var db = _context)
            {
                IOrderedQueryable<Board> boards = db.Boards
                    .OrderBy(b => b.BoardId);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
