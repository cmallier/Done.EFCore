using System;
using System.Collections.Generic;

namespace ScaffoldApp.Models;

public partial class Note
{
    public int NoteId { get; set; }

    public string Contenu { get; set; } = null!;

    public DateTimeOffset DateCreation { get; set; }

    public int UtilisateurCreationId { get; set; }

    public virtual Utilisateur UtilisateurCreation { get; set; } = null!;

    public virtual ICollection<PublicationOeuvre> PublicationOeuvres { get; set; } = new List<PublicationOeuvre>();
}
