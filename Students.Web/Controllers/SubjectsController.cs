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


namespace Students.Web.Controllers;

public class SubjectsController : Controller
{
    private readonly ILogger _logger;
    private readonly IDatabaseService _databaseService;

    public SubjectsController(
        ILogger<SubjectsController> logger,
        IDatabaseService databaseService)
    {
        _logger = logger;
        _databaseService = databaseService;
    }


    // GET: Subjects
    public async Task<IActionResult> Index()
    {
        IActionResult result = View();
        var model = await _databaseService.SubjectList();
        result = View(model);
        return result;
    }

    // GET: Subjects/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subject = await _databaseService.SubjectDetailsDelete(id);
            
        if (subject == null)
        {
            return NotFound();
        }

        return View(subject);
    }

    // GET: Subjects/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Subjects/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Credits")] Subject subject)
    {
        if (ModelState.IsValid)
        {      
            await _databaseService.SubjectCreate(subject);
            return RedirectToAction(nameof(Index));
        }
        return View(subject);
    }

    // GET: Subjects/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subject = await _databaseService.SubjectEditView(id);
        if (subject == null)
        {
            return NotFound();
        }
        return View(subject);
    }

    // POST: Subjects/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Credits")] Subject subject)
    {
        if (id != subject.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _databaseService.SubjectEdit(subject);
            return RedirectToAction(nameof(Index));
        }
        return View(subject);
    }

    // GET: Subjects/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subject = await _databaseService.SubjectDetailsDelete(id);           
        if (subject == null)
        {
            return NotFound();
        }

        return View(subject);
    }

    // POST: Subjects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var subject = await _databaseService.SubjectDeleteConfirmed(id);
        return RedirectToAction(nameof(Index));
    }


}
