using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskPlanner.Models
{
    public class TaskPlannerContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<BoardColumn> BoardColumns { get; set; } 

        public TaskPlannerContext(DbContextOptions<TaskPlannerContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardColumn>()
                .HasOne<Board>(bc => bc.Board)
                .WithMany(b => b.BoardColumns)
                .HasForeignKey(bc => bc.BoardId);

            modelBuilder.Entity<Task>()
                .HasOne<BoardColumn>(t => t.BoardColumn)
                .WithMany(bc => bc.Tasks)
                .HasForeignKey(t => t.BoardColumnId);
        }
    }
}
