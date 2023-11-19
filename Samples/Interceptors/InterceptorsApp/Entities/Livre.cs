namespace InterceptorsApp.Entities;

public class Livre : Entity, IAuditable
{
    public int LivreId { get; set; }

    public string Titre { get; set; } = null!;

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }
}
