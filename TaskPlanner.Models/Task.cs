﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TaskPlanner.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
