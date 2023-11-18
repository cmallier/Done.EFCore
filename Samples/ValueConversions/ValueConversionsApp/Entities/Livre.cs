namespace ValueConversionsApp.Entities;

public class Livre
{
    public int LivreId { get; set; }

    public string Titre { get; set; } = null!;

    public Genre Genre { get; set; } = Genre.Autre;

    public Dollars Price { get; set; }

    public bool IsActive { get; set; }

    public bool IsAvailable { get; set; }

    public Currency Currency { get; set; }
}


public enum Genre
{
    Roman,
    Policier,
    ScienceFiction,
    Histoire,
    Biographie,
    Autre
}

public enum Currency
{
    UsDollars,
    PoundsSterling,
    Euros
}

public readonly struct Dollars
{
    public Dollars( decimal amount )
        => Amount = amount;

    public decimal Amount { get; }

    public override string ToString()
        => $"${Amount}";
}