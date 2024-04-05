using Students.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Students.Common.Models;

public class LectureRoom
{

    public int Id { get; set; }

    [Required]
    public string Number { get; set; } = string.Empty;
    public int Floor { get; set; }

    public LectureRoom()
    {
    }

    public LectureRoom(string number, int floor)
    {
        Number = number;
        Floor = floor;
    }
}
