using System;
using System.Collections.Generic;

namespace NEU25G_Databases_Labb2;

public partial class Format
{
    public int FormatId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
