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
    public class BoardColumnsController : Controller
    {
        private readonly TaskPlannerContext _context;

        public BoardColumnsController()
        {
            _context = new TaskPlannerContext();
        }

        // GET: BoardColumns
        public async Task<IActionResult> Index()
        {
            var taskPlannerContext = _context.BoardColumns;
            return View(await taskPlannerContext.ToListAsync());
        }

        // GET: BoardColumns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardColumn = await _context.BoardColumns
                .FirstOrDefaultAsync(m => m.BoardColumnId == id);
            if (boardColumn == null)
            {
                return NotFound();
            }

            return View(boardColumn);
        }

        // GET: BoardColumns/Create
        public IActionResult Create()
        {
            ViewData["BoardId"] = new SelectList(_context.Boards, "BoardId", "BoardId");
            return View();
        }

        // POST: BoardColumns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BoardColumnId,Title,BoardId")] BoardColumn boardColumn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boardColumn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardId"] = new SelectList(_context.Boards, "BoardId", "BoardId", boardColumn.BoardId);
            return View(boardColumn);
        }

        // GET: BoardColumns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardColumn = await _context.BoardColumns.FindAsync(id);
            if (boardColumn == null)
            {
                return NotFound();
            }
            ViewData["BoardId"] = new SelectList(_context.Boards, "BoardId", "BoardId", boardColumn.BoardId);
            return View(boardColumn);
        }

        // POST: BoardColumns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BoardColumnId,Title,BoardId")] BoardColumn boardColumn)
        {
            if (id != boardColumn.BoardColumnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boardColumn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardColumnExists(boardColumn.BoardColumnId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardId"] = new SelectList(_context.Boards, "BoardId", "BoardId", boardColumn.BoardId);
            return View(boardColumn);
        }

        // GET: BoardColumns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardColumn = await _context.BoardColumns
                .FirstOrDefaultAsync(m => m.BoardColumnId == id);
            if (boardColumn == null)
            {
                return NotFound();
            }

            return View(boardColumn);
        }

        // POST: BoardColumns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boardColumn = await _context.BoardColumns.FindAsync(id);
            _context.BoardColumns.Remove(boardColumn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardColumnExists(int id)
        {
            return _context.BoardColumns.Any(e => e.BoardColumnId == id);
        }
    }
}
