using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{

    Student? DisplayStudent(int? id);

    public Task<List<Student>> StudentList();

    public Task<Student?> StudentDetails(int? id);

    public Task<Student> StudentCreate(Student student, int[] subjectIdDst);

    public Task<Student> StudentCreateView();

    public Task<Student?> DeleteStudentView(int? id);

    public Task<Student?> EditStudentView(int? id);

    public Task<Student> StudentEdit(Student student, int[] subjectIdDst);

    public Task<bool> StudentDeleteConfirmed(int id);

    //Sybject
    public Task<List<Subject>> SubjectList();

    public Task<Subject?> SubjectDetailsDelete(int? id);

    public Task<Subject?> SubjectCreate(Subject subject);

    public Task<Subject?> SubjectEditView(int? id);

    public Task<Subject?> SubjectEdit(Subject subject);

    public Task<bool> SubjectDeleteConfirmed(int id);

    //Books
    public Task<List<Book>> BookList();

    public Task<Book?> BookDetailsDelete(int? id);

    public Task<Book?> BookCreate(Book book);

    public Task<Book?> BookEditView(int? id);

    public Task<Book?> BookEdit(Book book);

    public Task<bool> BookDeleteConfirmed(int id);

    //Lecturers
    public Task<List<Lecturer>> LecturerList();

    public Task<Lecturer?> LecturerDetailsDelete(int? id);

    public Task<Lecturer?> LecturerCreate(Lecturer lecturer);

    public Task<Lecturer?> LecturerEditView(int? id);

    public Task<Lecturer?> LecturerEdit(Lecturer lecturer);

    public Task<bool> LecturerDeleteConfirmed(int id);

    //LectureRooms
    public Task<List<LectureRoom>> LectureRoomList();

    public Task<LectureRoom?> LectureRoomDetailsDelete(int? id);

    public Task<LectureRoom?> LectureRoomCreate(LectureRoom lectureRoom, int[] subjectIdDst);

    public Task<LectureRoom?> LectureRoomCreateView();

    public Task<LectureRoom?> LectureRoomEditView(int? id);

    public Task<LectureRoom?> LectureRoomEdit(LectureRoom lectureRoom, int[] subjectIdDst);

    public Task<bool> LectureRoomDeleteConfirmed(int id);
}
