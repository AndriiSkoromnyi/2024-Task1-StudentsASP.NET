using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;
using Students.Services;

namespace Students.Web.Controllers
{
    public class LectureRoomsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDatabaseService _databaseService;

        public LectureRoomsController(
            ILogger<SubjectsController> logger,
            IDatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        // GET: LectureRooms
        public async Task<IActionResult> Index()
        {
            IActionResult result = View();
            var model = await _databaseService.LectureRoomList();
            result = View(model);
            return result;
        }

        // GET: LectureRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureRoom = await _databaseService.LectureRoomDetailsDelete(id);
            if (lectureRoom == null)
            {
                return NotFound();
            }

            return View(lectureRoom);
        }

        // GET: LectureRooms/Create

        public async Task<IActionResult> Create()
        {
            IActionResult result = View();
            try
            {
                var newStudent = await _databaseService.LectureRoomCreateView();
                result = View(newStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception caught: " + ex.Message);
            }

            return result;
        }

        // POST: LectureRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Floor")] LectureRoom lectureRoom, int[] subjectIdDst)
        {
            if (ModelState.IsValid)
            {

                await _databaseService.LectureRoomCreate(lectureRoom, subjectIdDst);
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
            
            var lectureRoom = await _databaseService.LectureRoomEditView(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Floor")] LectureRoom lectureRoom, int[] subjectIdDst)
        {
            if (id != lectureRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _databaseService.LectureRoomEdit(lectureRoom, subjectIdDst);
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

            var lectureRoom = await _databaseService.LectureRoomDetailsDelete(id);
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
            var lectureRoom = await _databaseService.LectureRoomDeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
