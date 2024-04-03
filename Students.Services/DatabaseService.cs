using Microsoft.Extensions.Logging;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Students.Services;

public class DatabaseService : IDatabaseService
{
    #region Ctor and Properties

    private readonly StudentsContext _context;
    private readonly ILogger<DatabaseService> _logger;

    public DatabaseService(
        ILogger<DatabaseService> logger,
        StudentsContext context)
    {
        _logger = logger;
        _context = context;
    }

    #endregion // Ctor and Properties

    #region Public Methods

    public bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst)
    {
        var result = false;

        // Find the student
        var student = _context.Student.Find(id);
        if (student != null)
        {
            // Update the student's properties
            student.Name = name;
            student.Age = age;
            student.Major = major;

            // Get the chosen subjects
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();

            // Remove the existing StudentSubject entities for the student
            var studentSubjects = _context.StudentSubject
                .Where(ss => ss.StudentId == id)
                .ToList();
            _context.StudentSubject.RemoveRange(studentSubjects);

            // Add new StudentSubject entities for the chosen subjects
            foreach (var subject in chosenSubjects)
            {
                var studentSubject = new StudentSubject
                {
                    Student = student,
                    Subject = subject
                };
                _context.StudentSubject.Add(studentSubject);
            }

            // Save changes to the database
            var resultInt = _context.SaveChanges();
            result = resultInt > 0;
        }

        return result;
    }

    public Student? DisplayStudent(int? id)
    {
        Student? student = null;
        try
        {
            student = _context.Student
                .FirstOrDefault(m => m.Id == id);
            if (student is not null)
            {
                var studentSubjects = _context.StudentSubject
                    .Where(ss => ss.StudentId == id)
                    .Include(ss => ss.Subject)
                    .ToList();
                student.StudentSubjects = studentSubjects;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught in DisplayStudent: " + ex);
        }

        return student;
    }

    /// <summary>
    /// Get StudentList
    /// </summary>
    /// <returns></returns>
    public async Task<List<Student>> StudentList()
    {
        var model = await _context.Student.ToListAsync();
        return model;
    }

    public async Task<Student?> StudentDetails(int? id)
    {
        var student = await _context.Student
            .FirstOrDefaultAsync(m => m.Id == id);
        if (student is not null)
        {
            var studentSubjects = _context.StudentSubject
                .Where(ss => ss.StudentId == id)
                .Include(ss => ss.Subject)
                .ToList();
            student.StudentSubjects = studentSubjects;
        }
        return student;

    }

    public async Task<bool> StudentCreate(int id, string name, int age, string major, int[] subjectIdDst)
    {
        var result = false;

        var chosenSubjects = _context.Subject
            .Where(s => subjectIdDst.Contains(s.Id))
            .ToList();
        var availableSubjects = _context.Subject
            .Where(s => !subjectIdDst.Contains(s.Id))
            .ToList();
        var student = new Student()
        {
            Id = id,
            Name = name,
            Age = age,
            Major = major,
            AvailableSubjects = availableSubjects
        };
        foreach (var chosenSubject in chosenSubjects)
        {
            student.AddSubject(chosenSubject);
        }
        await _context.Student.AddAsync(student);
        var saveResult = await _context.SaveChangesAsync();
        result = saveResult > 0;

        return result;
    }

    public async Task<Student?> DeleteStudentView(int? id)
    {
        var student = await _context.Student
            .FirstOrDefaultAsync(m => m.Id == id);

        return student;
    }

    public async Task<Student> StudentCreateView()
    {
        var listOfSubjects = await _context.Subject
                .ToListAsync();
        var newStudent = new Student();
        newStudent.AvailableSubjects = listOfSubjects;

        return newStudent;
    }

    public async Task<Student?> EditStudentView(int? id)
    {
        var student = await _context.Student.FindAsync(id);
        var chosenSubjects = _context.StudentSubject
                .Where(ss => ss.StudentId == id)
                .Select(ss => ss.Subject)
                .ToList();
            var availableSubjects = _context.Subject
                .Where(s => !chosenSubjects.Contains(s))
                .ToList();
            student.StudentSubjects = _context.StudentSubject
                .Where(x => x.StudentId == id)
                .ToList();

        return student;      
    }

    #endregion // Public Methods
}
