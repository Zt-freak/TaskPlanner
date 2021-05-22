using System;
using System.Collections.Generic;
using System.IO;
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

        //api/board/get?id=1
        [HttpGet("api/board/get")]
        public ActionResult<string> GetBoard( int? id )
        {
            Board board = _context.Boards.Include(b => b.BoardColumns).FirstOrDefault(b => b.BoardId == id);

            if (board == null)
            {
                return NotFound();
            }
            var jsonString = JsonConvert.SerializeObject(board);
            return jsonString;
        }

        //api/column/get?id=1
        [HttpGet("api/column/get")]
        public ActionResult<string> GetColumn(int? id)
        {
            BoardColumn column = _context.BoardColumns.Include(c => c.Tasks).FirstOrDefault(b => b.BoardColumnId == id);

            if (column == null)
            {
                return NotFound();
            }
            var jsonString = JsonConvert.SerializeObject(column);
            return jsonString;
        }

        //api/task/get?id=1
        [HttpGet("api/task/get")]
        public ActionResult<string> GetTask(int? id)
        {
            Models.Task task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);

            if (task == null)
            {
                return NotFound();
            }
            var jsonString = JsonConvert.SerializeObject(task);
            return jsonString;
        }

        //api/task/move
        [HttpPost("api/task/move")]
        public ActionResult<string> MoveTask(string stringData)
        {
            int taskId = 1;
            int columnId = 1;
            Models.Task task = _context.Tasks.FirstOrDefault(t => t.TaskId == taskId);
            BoardColumn column = _context.BoardColumns.FirstOrDefault(b => b.BoardColumnId == columnId);
            if (task == null)
            {
                return "task not found";
                //return NotFound();
            }
            if (column == null)
            {
                return "column not found";
                //return NotFound();
            }

            task.BoardColumnId = column.BoardColumnId;

            try
            {
                _context.Update(task);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var jsonString = JsonConvert.SerializeObject(task);
            return jsonString;
        }
    }
}
