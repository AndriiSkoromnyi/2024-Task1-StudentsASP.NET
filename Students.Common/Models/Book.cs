using Students.Common.Attributes;
using Students.Common.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Students.Common.Models;

public class Book
{
    
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [NameValidation]
    public string Author { get; set; } = string.Empty;

    public Book()
    {
    }

    public Book(string name, string author)
    {
        Name = name;
        Author = author;
    }
}
