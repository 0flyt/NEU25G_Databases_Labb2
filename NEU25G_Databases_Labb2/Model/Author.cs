using System;
using System.Collections.Generic;

namespace NEU25G_Databases_Labb2;

public partial class Author
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? Birth { get; set; }

    public virtual ICollection<Book> Isbn13s { get; set; } = new List<Book>();
}
