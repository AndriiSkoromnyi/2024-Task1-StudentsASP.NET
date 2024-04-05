using Students.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Students.Common.Models;

public class Lecturer
{

    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;  
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
