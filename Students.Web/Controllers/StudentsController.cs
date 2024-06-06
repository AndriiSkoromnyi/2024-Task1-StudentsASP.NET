using Microsoft.AspNetCore.Mvc;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;

namespace Students.Web.Controllers;

public class StudentsController : Controller
{
    #region Ctor And Properties

    private readonly StudentsContext _context;
    private readonly ILogger _logger;
    private readonly ISharedResourcesService _sharedResourcesService;
    private readonly IDatabaseService _databaseService;

    public StudentsController(
        StudentsContext context, 
        ILogger<StudentsController> logger, 
        ISharedResourcesService sharedResourcesService,
        IDatabaseService databaseService)
    {
        _context = context;
        _logger = logger;
        _sharedResourcesService = sharedResourcesService;
        _databaseService = databaseService;
    }

    #endregion // Ctor And Properties

    #region Public Methods

    // GET: Students
    public async Task<IActionResult> Index()
    {
        IActionResult result = View();
        try
        {
            var model = await _databaseService.StudentList();
            result = View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return result;
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        IActionResult result = NotFound();

        try
        {
            if (id != null)
            {
                var student = await _databaseService.StudentDetails(id);
                if (student is null)
                {
                    result = NotFound();
                }
                else
                {

                    result = View(student);

                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }
        return result;
    }


    // GET: Students/Create
    public async Task <IActionResult> Create()
    {
        IActionResult result = View();
        try
        {
             var newStudent = await _databaseService.StudentCreateView();          
             result = View(newStudent);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return result;
    }

    // POST: Students/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id, Name, Age, Major, PostalCode ")] Student student, int[] subjectIdDst)
    {
        IActionResult result = await Create();
        if (ModelState.IsValid)
        {
            student = await _databaseService.StudentCreate(student, subjectIdDst);
            result = RedirectToAction(nameof(Index));
        }
        return result;
    }
    
    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        IActionResult result = NotFound();

        try
        {
            if (id != null)
            {
                var student = await _databaseService.EditStudentView(id);
                if (student != null)
                {              
                    result = View(student);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return result;
    }


    // POST: Students/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit([Bind("Id, Name, Age, Major, PostalCode")] Student student, int[] subjectIdDst)
{
    IActionResult result;

    try
    {
        if (ModelState.IsValid)
        {
            student = await _databaseService.StudentEdit(student, subjectIdDst);

            // Set the result to redirect to the Index action
            result = RedirectToAction(nameof(Index));
        }
        else
        {
            // If model state is not valid, return the view with the current student
            result = View(student);
        }
    }
    catch (Exception ex)
    {
        // Log the exception and set the result to return the view with the current student
        _logger.LogError("Exception caught: " + ex.Message);
        result = View(student);
    }

    return result;
}

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        IActionResult result = View();
        try
        {
            if (id == null)
            {
                result = NotFound();
            }
            else
            {

                var student = await _databaseService.DeleteStudentView(id);
                if (student == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = View(student);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught: " + ex.Message);
        }

        return result;
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        IActionResult result = View();
        
        {
            var student = await _databaseService.StudentDeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

    }

    #endregion // Public Methods

        #region Private Methods

    private bool StudentExists(int id)
    {
        var result = _context.Student.Any(e => e.Id == id);
        return result;
    }

    #endregion // Private Methods
}
