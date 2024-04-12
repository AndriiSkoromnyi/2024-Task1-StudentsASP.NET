using Students.Common.Attributes;
using Students.Common.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Students.Common.Models;

public class Subject
{

    public int Id { get; set; }

    [Required]
    [CapitalLettersOnly]
    [NameValidation(ErrorMessage = "Nazwa przedmiotu nie mo?e zawiera? cyfr oraz nie mo?e zaczyna? si? z ma?ej litery.")]
    public string Name { get; set; } = string.Empty;

    [Range(1, 10)]
    public int Credits { get; set; }

    public List<Student> Students { get; set; } = new List<Student>();

    public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();



    public Subject()
    {
    }

    public Subject(string name, int credits)
    {
        Name = name;
        Credits = credits;
    }
}
