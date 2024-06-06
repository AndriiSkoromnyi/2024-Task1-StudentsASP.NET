using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;

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

    #region Students

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
        student.AvailableSubjects = availableSubjects;
        return student;
    }

    public async Task<Student> StudentEdit(Student student, int[] subjectIdDst)
    {

        var existingStudent = await _context.Student.FindAsync(student.Id);
        try
        {
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Age = student.Age;
                existingStudent.Major = student.Major;

                var studentSubjects = await _context.StudentSubject
                    .Where(ss => ss.StudentId == student.Id)
                    .ToListAsync();
                _context.StudentSubject.RemoveRange(studentSubjects);

                var chosenSubjects = await _context.Subject
                    .Where(s => subjectIdDst.Contains(s.Id))
                    .ToListAsync();
                foreach (var subject in chosenSubjects)
                {
                    var studentSubject = new StudentSubject
                    {
                        Student = existingStudent,
                        Subject = subject
                    };
                    _context.StudentSubject.Add(studentSubject);
                }
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return existingStudent;
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

    public async Task<Student> StudentCreate(Student student, int[] subjectIdDst)
    {

        var chosenSubjects = _context.Subject
            .Where(s => subjectIdDst.Contains(s.Id))
            .ToList();
        var availableSubjects = _context.Subject
            .Where(s => !subjectIdDst.Contains(s.Id))
            .ToList();
        student.AvailableSubjects = availableSubjects;

        foreach (var chosenSubject in chosenSubjects)
        {
            student.AddSubject(chosenSubject);
        }
        _context.Add(student);
        await _context.SaveChangesAsync();
        return student;
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

    public async Task<bool> StudentDeleteConfirmed(int id)
    {
        var student = await _context.Student.FindAsync(id);
        if (student != null)
        {
            _context.Student.Remove(student);
        }

        var resultInt = await _context.SaveChangesAsync();
        var result = resultInt > 0;
        return result;
    }

    #endregion //Students

    #region Subject

    public async Task<List<Subject>> SubjectList()
    {
        var model = await _context.Subject.ToListAsync();
        return model;
    }

    public async Task<Subject?> SubjectDetailsDelete(int? id)
    {
        var subject = await _context.Subject.FirstOrDefaultAsync(m => m.Id == id);
        return subject;
    }

    public async Task<Subject?> SubjectCreate(Subject subject)
    {
        _context.Add(subject);
        await _context.SaveChangesAsync();
        return subject;
    }

    public async Task<Subject?> SubjectEditView(int? id)
    {
        var subject = await _context.Subject.FindAsync(id);
        return subject;
    }

    public async Task<Subject?> SubjectEdit(Subject subject)
    {
        _context.Update(subject);
        await _context.SaveChangesAsync();
        return subject;
    }

    public async Task<bool> SubjectDeleteConfirmed(int id)
    {
        var subject = await _context.Subject.FindAsync(id);
        if (subject != null)
        {
            _context.Subject.Remove(subject);
        }

        var resultInt = await _context.SaveChangesAsync();
        var result = resultInt > 0;
        return result;
    }

    #endregion Subject

    #region Book
    public async Task<List<Book>> BookList()
    {
        var model = await _context.Book.ToListAsync();
        return model;
    }

    public async Task<Book?> BookDetailsDelete(int? id)
    {
        var book = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);
        return book;
    }

    public async Task<Book?> BookCreate(Book book)
    {
        _context.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> BookEditView(int? id)
    {
        var book = await _context.Book.FindAsync(id);
        return book;
    }

    public async Task<Book?> BookEdit(Book book)
    {
        _context.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> BookDeleteConfirmed(int id)
    {
        var book = await _context.Book.FindAsync(id);
        if (book != null)
        {
            _context.Book.Remove(book);
        }
        var resultInt = await _context.SaveChangesAsync();
        var result = resultInt > 0;
        return result;
    }
    #endregion

    #region Lecturer
    public async Task<List<Lecturer>> LecturerList()
    {
        var model = await _context.Lecturer.ToListAsync();
        return model;
    }

    public async Task<Lecturer?> LecturerDetailsDelete(int? id)
    {
        var lecturer = await _context.Lecturer.FirstOrDefaultAsync(m => m.Id == id);
        return lecturer;
    }

    public async Task<Lecturer?> LecturerCreate(Lecturer lecturer)
    {
        _context.Add(lecturer);
        await _context.SaveChangesAsync();
        return lecturer;
    }

    public async Task<Lecturer?> LecturerEditView(int? id)
    {
        var lecturer = await _context.Lecturer.FindAsync(id);
        return lecturer;
    }

    public async Task<Lecturer?> LecturerEdit(Lecturer lecturer)
    {
        _context.Update(lecturer);
        await _context.SaveChangesAsync();
        return lecturer;
    }

    public async Task<bool> LecturerDeleteConfirmed(int id)
    {
        var lecturer = await _context.Lecturer.FindAsync(id);
        if (lecturer != null)
        {
            _context.Lecturer.Remove(lecturer);
        }
        var resultInt = await _context.SaveChangesAsync();
        var result = resultInt > 0;
        return result;
    }
    #endregion Lecturer

    #region LectureRooms
    public async Task<List<LectureRoom>> LectureRoomList()
    {
        var model = await _context.LectureRoom.ToListAsync();
        return model;
    }

    public async Task<LectureRoom?> LectureRoomDetailsDelete(int? id)
    {
        var lectureRoom = await _context.LectureRoom
                .FirstOrDefaultAsync(m => m.Id == id);
        if (lectureRoom is not null)
        {
            var studentSubjects = _context.Subject
                .Where(ss => ss.LectureRoom.Id == id)
                .Include(ss => ss.LectureRoom)
                .ToList();
            lectureRoom.Subjects = studentSubjects;
        }
        return lectureRoom;
    }

    public async Task<LectureRoom?> LectureRoomCreate(LectureRoom lectureRoom, int[] subjectIdDst)
    {
        var chosenSubjects = _context.Subject
            .Where(s => subjectIdDst.Contains(s.Id))
            .ToList();

        var availableSubjects = _context.Subject
            .Where(s => !subjectIdDst.Contains(s.Id))
            .ToList();
        lectureRoom.AvailableSubjects = availableSubjects;

        foreach (var chosenSubject in chosenSubjects)
        {
            lectureRoom.Subjects.Add(chosenSubject);
        }

        _context.Add(lectureRoom);
        await _context.SaveChangesAsync();
        return lectureRoom;
    }

    public async Task<LectureRoom> LectureRoomEditView(int? id)
    {
        try
        {
            var lecturer = await _context.LectureRoom
               .Include(l => l.Subjects)
               .FirstOrDefaultAsync(l => l.Id == id);

            if (lecturer != null)
            {
                var chosenSubjects = lecturer.Subjects;
                var subjectsWithLecturers = await _context.LectureRoom
                    .Where(l => l.Id != id)
                    .SelectMany(l => l.Subjects)
                    .Distinct()
                    .ToListAsync();

                var availableSubjects = await _context.Subject
                    .Where(s => !chosenSubjects.Contains(s) && !subjectsWithLecturers.Contains(s))
                    .ToListAsync();

                lecturer.AvailableSubjects = availableSubjects;

                return lecturer;
            }
            else
            {
                throw new Exception("LectureRoom not found.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
            throw new Exception("An error occurred while retrieving lecturer details.");
        }
    }

    public async Task<LectureRoom?> LectureRoomEdit(LectureRoom lectureRoom, int[] subjectIdDst)
    {
        var existingLectureRoom = await _context.LectureRoom.FindAsync(lectureRoom.Id);
        try
        {
            if (existingLectureRoom != null)
            {
                existingLectureRoom.Number = lectureRoom.Number;
                existingLectureRoom.Floor = lectureRoom.Floor;




                var chosenSubjects = await _context.Subject
                    .Where(s => subjectIdDst.Contains(s.Id))
                    .ToListAsync();
                var availableSubjects = await _context.Subject
                    .Where(s => !subjectIdDst.Contains(s.Id))
                    .ToListAsync();
                existingLectureRoom.Subjects = chosenSubjects;
                existingLectureRoom.AvailableSubjects = availableSubjects;
                 await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return existingLectureRoom;
    }

    public async Task<bool> LectureRoomDeleteConfirmed(int id)
    {
        var lectureRoom = await _context.LectureRoom.FindAsync(id);
        if (lectureRoom != null)
        {
            _context.LectureRoom.Remove(lectureRoom);
        }
        var resultInt = await _context.SaveChangesAsync();
        var result = resultInt > 0;
        return result;
    }

    public async Task<LectureRoom?> LectureRoomCreateView()
    {
        var listOfSubjects = await _context.Subject
                .ToListAsync();
        var newStudent = new LectureRoom();
        newStudent.AvailableSubjects = listOfSubjects;

        return newStudent;
    }

    #endregion LectureRooms
}
