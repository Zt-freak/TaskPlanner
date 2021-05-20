using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskPlanner.Models
{
    public class TaskPlannerContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Task> Tasks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bnkho\source\repos\TaskPlannerDb\TaskPlannerDb.mdf;Integrated Security=True;Connect Timeout=30");
    }
}
