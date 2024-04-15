using Students.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Common.Models;

public class LectureRoom
{

    public int Id { get; set; }

    [Required]
    public string Number { get; set; } = string.Empty;
    public int Floor { get; set; }

    [NotMapped]
    public ICollection<Subject> AvailableSubjects { get; set; } = new List<Subject>();
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    public LectureRoom()
    {
    }

    public LectureRoom(string number, int floor)
    {
        Number = number;
        Floor = floor;
    }
}
