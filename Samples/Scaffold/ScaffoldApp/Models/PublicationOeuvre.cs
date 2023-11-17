using System;
using System.Collections.Generic;

namespace ScaffoldApp.Models;

public partial class PublicationOeuvre
{
    public int PublicationOeuvreId { get; set; }

    public string Titre { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
