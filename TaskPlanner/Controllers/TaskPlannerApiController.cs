using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskPlanner.Models;

namespace TaskPlanner.Controllers
{
    [ApiController]
    public class TaskPlannerApiController : Controller
    {
        private readonly TaskPlannerContext _context;

        // Add dependency injection later
        public TaskPlannerApiController()
        {
            _context = new TaskPlannerContext();
        }

        [HttpGet("api/board")]
        public ActionResult<string> GetBoard()
        {
            Board board = _context.Boards.Include(b => b.BoardColumns).FirstOrDefault(b => b.BoardId == 1);

            if (board == null)
            {
                return NotFound();
            }
            var jsonString = JsonConvert.SerializeObject(board);
            return jsonString;
        }

        /*[HttpGet("api/board")]
        public ActionResult<string> GetBoardAsync()
        {​​​​​​​
            Board board = _context.Boards.Include(b => b.BoardColumns).FirstOrDefault(b => b.BoardId == 1);


            if (board == null)
            {​​​​​​​
                return NotFound();
            }​​​​​​​
            var jsonString = JsonConvert.SerializeObject(board);
            return jsonString;
        }​​​​​​​*/
    }
}
