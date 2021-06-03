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

        public TaskPlannerApiController(TaskPlannerContext context)
        {
            _context = context;
        }

        //api/board/get?id=1
        [HttpGet("api/board/get")]
        public ActionResult<string> GetBoard(int? id)
        {
            Board board = _context.Boards.Include(b => b.BoardColumns).FirstOrDefault(b => b.BoardId == id);

            if (board == null)
            {
                return NotFound();
            }
            var jsonString = JsonConvert.SerializeObject(board,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            return jsonString;
        }

        //api/board/edit
        [HttpPost("api/board/edit")]
        public ActionResult<string> EditBoard()
        {
            var output = "";
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                output = reader.ReadToEnd();
            }
            RequestData jsonData = JsonConvert.DeserializeObject<RequestData>(output);

            Board board = _context.Boards.FirstOrDefault(t => t.BoardId == jsonData.BoardId);
            if (board == null)
            {
                return NotFound();
            }

            board.Title = jsonData.Title;

            try
            {
                _context.Update(board);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var jsonString = JsonConvert.SerializeObject(board,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
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
            var jsonString = JsonConvert.SerializeObject(column,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            return jsonString;
        }

        //api/column/edit
        [HttpPost("api/column/edit")]
        public ActionResult<string> EditColumn()
        {
            var output = "";
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                output = reader.ReadToEnd();
            }
            RequestData jsonData = JsonConvert.DeserializeObject<RequestData>(output);

            BoardColumn column = _context.BoardColumns.FirstOrDefault(t => t.BoardColumnId == jsonData.BoardColumnId);
            if (column == null)
            {
                return NotFound();
            }

            column.Title = jsonData.Title;

            try
            {
                _context.Update(column);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var jsonString = JsonConvert.SerializeObject(column,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            return jsonString;
        }

        //api/column/delete
        [HttpPost("api/column/delete")]
        public ActionResult<string> DeleteColumn()
        {
            var output = "";
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                output = reader.ReadToEnd();
            }
            RequestData jsonData = JsonConvert.DeserializeObject<RequestData>(output);

            BoardColumn column = _context.BoardColumns.FirstOrDefault(t => t.BoardColumnId == jsonData.BoardColumnId);
            if (column == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(column);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            //var jsonString = JsonConvert.SerializeObject(column);
            return "{}";
        }

        //api/column/add
        [HttpPost("api/column/add")]
        public ActionResult<string> AddColumn()
        {
            var output = "";
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                output = reader.ReadToEnd();
            }
            RequestData jsonData = JsonConvert.DeserializeObject<RequestData>(output);

            BoardColumn column = new BoardColumn();
            if (column == null)
            {
                return NotFound();
            }

            column.Title = jsonData.Title;
            column.BoardId = jsonData.BoardId;

            try
            {
                _context.Add(column);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var jsonString = JsonConvert.SerializeObject(column,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
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
            var jsonString = JsonConvert.SerializeObject(task,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            return jsonString;
        }

        //api/task/edit
        [HttpPost("api/task/edit")]
        public ActionResult<string> EditTask()
        {
            var output = "";
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                output = reader.ReadToEnd();
            }
            RequestData jsonData = JsonConvert.DeserializeObject<RequestData>(output);

            Models.Task task = _context.Tasks.FirstOrDefault(t => t.TaskId == jsonData.TaskId);
            BoardColumn column = _context.BoardColumns.FirstOrDefault(t => t.BoardColumnId == jsonData.BoardColumnId);
            if (task == null)
            {
                return NotFound();
            }
            if (column != null)
            {
                task.BoardColumnId = column.BoardColumnId;
            }

            if (!String.IsNullOrWhiteSpace(jsonData.Title))
            {
                task.Title = jsonData.Title;
            }
            if (!String.IsNullOrWhiteSpace(jsonData.Content))
            {
                task.Content = jsonData.Content;
            }

            try
            {
                _context.Update(task);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var jsonString = JsonConvert.SerializeObject(task,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            return jsonString;
        }

        //api/task/delete
        [HttpPost("api/task/delete")]
        public ActionResult<string> DeleteTask()
        {
            var output = "";
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                output = reader.ReadToEnd();
            }
            RequestData jsonData = JsonConvert.DeserializeObject<RequestData>(output);

            Models.Task task = _context.Tasks.FirstOrDefault(t => t.TaskId == jsonData.TaskId);
            if (task == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(task);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            //var jsonString = JsonConvert.SerializeObject(column);
            return "{}";
        }

        //api/task/add
        [HttpPost("api/task/add")]
        public ActionResult<string> AddTask()
        {
            var output = "";
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                output = reader.ReadToEnd();
            }
            RequestData jsonData = JsonConvert.DeserializeObject<RequestData>(output);

            Models.Task task = new Models.Task();
            if (task == null)
            {
                return NotFound();
            }

            task.Title = jsonData.Title;
            task.Content = jsonData.Content;
            task.BoardColumnId = jsonData.BoardColumnId;

            try
            {
                _context.Add(task);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var jsonString = JsonConvert.SerializeObject(task,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            return jsonString;
        }
    }

    public class RequestData
    {
        public int BoardId { get; set; }
        public int BoardColumnId { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
