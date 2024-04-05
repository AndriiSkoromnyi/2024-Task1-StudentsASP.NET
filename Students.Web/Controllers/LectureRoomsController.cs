using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;

namespace Students.Web.Controllers
{
    public class LectureRoomsController : Controller
    {
        private readonly StudentsContext _context;

        public LectureRoomsController(StudentsContext context)
        {
            _context = context;
        }

        // GET: LectureRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.LectureRoom.ToListAsync());
        }

        // GET: LectureRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureRoom = await _context.LectureRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lectureRoom == null)
            {
                return NotFound();
            }

            return View(lectureRoom);
        }

        // GET: LectureRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LectureRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Floor")] LectureRoom lectureRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lectureRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lectureRoom);
        }

        // GET: LectureRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureRoom = await _context.LectureRoom.FindAsync(id);
            if (lectureRoom == null)
            {
                return NotFound();
            }
            return View(lectureRoom);
        }

        // POST: LectureRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Floor")] LectureRoom lectureRoom)
        {
            if (id != lectureRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lectureRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectureRoomExists(lectureRoom.Id))
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
            return View(lectureRoom);
        }

        // GET: LectureRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureRoom = await _context.LectureRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lectureRoom == null)
            {
                return NotFound();
            }

            return View(lectureRoom);
        }

        // POST: LectureRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lectureRoom = await _context.LectureRoom.FindAsync(id);
            if (lectureRoom != null)
            {
                _context.LectureRoom.Remove(lectureRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LectureRoomExists(int id)
        {
            return _context.LectureRoom.Any(e => e.Id == id);
        }
    }
}
