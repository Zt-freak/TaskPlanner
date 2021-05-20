using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TaskPlanner.Models
{
    public class Board
    {
        public int BoardId { get; set; }
        public string Title { get; set; }
        public List<Task> Tasks { get; } = new List<Task>();
    }
}
