namespace InheritanceApp.Entities;

public class Note
{
    public int NoteId { get; set; }

    public string Contenu { get; set; } = default!;

    public DateTime DateCreation { get; set; }

    public int UtilisateurCreationId { get; set; }

    public ICollection<PublicationOeuvre> PublicationOeuvres { get; set; } = [];
}


public class PublicationOeuvre
{
    public int PublicationOeuvreId { get; set; }

    public string Titre { get; set; } = default!;

    public List<Note> Notes { get; set; } = [];
}