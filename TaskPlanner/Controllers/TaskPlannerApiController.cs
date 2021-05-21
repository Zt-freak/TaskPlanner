using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<Board>>> GetBoardAsync(/*int? id*/)
        {
            /*if (id == null)
            {
                return NotFound();
            }*/

            var board = _context.Boards
                .Where(m => m.BoardId == 1).Include(b => b.BoardColumns).FirstOrDefault();

            if (board == null)
            {
                return NotFound();
            }

            return new[]
            {
                board
            };
        }
    }
}
