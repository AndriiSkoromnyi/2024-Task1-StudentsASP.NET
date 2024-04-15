using Students.Common.Attributes;
using Students.Common.ValidationAttributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Common.Models;

public class Subject
{

    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(1, 10)]
    public int Credits { get; set; }

    public List<Student> Students { get; set; } = new List<Student>();

    public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    
  
    public LectureRoom? LectureRoom { get; set; }
    public Subject()
    {
    }

    public Subject(string name, int credits)
    {
        Name = name;
        Credits = credits;
    }
}
