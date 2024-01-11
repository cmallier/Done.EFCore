namespace ProjectionApp.Entities;

public class Auteur
{
    public int AuteurId { get; set; }

    public string Nom { get; set; } = default!;

    public Biographie? Biographie { get; set; } = default!;
}


public class Biographie
{
    public int BiographieId { get; set; }

    public string Texte { get; set; } = default!;
}


public class AuteurReadModel
{
    public int AuteurId { get; set; }

    public string AuteurNom { get; set; } = default!;

    public string? BiographieTexte { get; set; }
}