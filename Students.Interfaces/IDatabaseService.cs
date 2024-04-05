using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{
    bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst);

    Student? DisplayStudent(int? id);

    public Task<List<Student>> StudentList();

    public Task<Student?> StudentDetails(int? id);

    public Task<bool> StudentCreate(int id, string name, int age, string major, int[] subjectIdDst);

    public Task<Student> StudentCreateView();

    public Task<Student?> DeleteStudentView(int? id);

    public Task<Student?> EditStudentView(int? id);


}
