using Students.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Students.Common.Models;

public class Book
{

    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    //[Range(1, 10)]
    public string Author { get; set; }

    //public List<Student> Students { get; set; } = new List<Student>();

    //public ICollection<StudentBook> StudentBooks { get; set; } = new List<StudentBook>();

    public Book()
    {
    }

    public Book(string name, string author)
    {
        Name = name;
        Author = author;
    }
}
