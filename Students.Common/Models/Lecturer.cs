using Students.Common.Attributes;
using Students.Common.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Students.Common.Models;

public class Lecturer
{

    public int Id { get; set; }

    [Required]
    [NameValidation]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Subject { get; set; }
    public Lecturer()
    {
    }

    public Lecturer(string name, string subject)
    {
        Name = name;
        Subject = subject;
    }
}
