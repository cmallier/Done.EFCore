namespace InheritanceCopibecApp.Entities;

public abstract class PublicationOeuvre
{
    public int PublicationOeuvreId { get; set; }
    public string Titre { get; set; } = null!;
    public string Statut { get; set; } = null!;
}

public class Partition : PublicationOeuvre
{
    public int NombrePages { get; set; }
}
