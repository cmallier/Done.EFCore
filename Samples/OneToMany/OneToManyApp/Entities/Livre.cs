namespace OneToManyApp.Entities;

public class Livre
{
    public int XLivreId { get; set; }

    public string Titre { get; set; } = null!;

    public ICollection<Tag> Tags { get; set; } = [];
}


public class Tag
{
    public int XTagId { get; set; }

    public string Nom { get; set; } = null!;


    // Since there is no CLR property which holds the foreign key for this relationship, a shadow property is created.
    public int YLivreId { get; set; }
    public Livre Livre { get; set; } = null!;
}
