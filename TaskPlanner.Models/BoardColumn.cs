using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TaskPlanner.Models
{
    public class BoardColumn
    {
        public int BoardColumnId { get; set; }
        public string Title { get; set; }
        public int BoardId { get; set; }
        //public Board Board { get; set; }
        public List<Task> Tasks { get; } = new List<Task>();
    }
}
