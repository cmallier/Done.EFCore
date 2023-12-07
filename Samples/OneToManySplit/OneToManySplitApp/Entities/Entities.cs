namespace OneToManyApp.Entities;

#region Case 1
public class Livre
{
    public int LivreId { get; set; }

    public string Titre { get; set; } = null!;

    public ICollection<Tag> Tags { get; set; } = [];
}

public class Tag
{
    public int TagId { get; set; }

    public string Nom { get; set; } = null!;

    public int LivreId { get; set; }

    public Livre Livre { get; set; } = null!;
}
#endregion


