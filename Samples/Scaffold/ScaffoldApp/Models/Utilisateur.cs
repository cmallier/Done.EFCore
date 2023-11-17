using System;
using System.Collections.Generic;

namespace ScaffoldApp.Models;

public partial class Utilisateur
{
    public int UtilisateurId { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
