namespace EnumsApp.Entities;

#region Strategy 1: Use a string / Json
//public class Livre
//{
//    public int LivreId { get; set; }

//    public string Titre { get; set; } = default!;

//    public List<Categorie> Categories { get; set; } = [];

//}

//public enum Categorie
//{
//    Aventure,
//    Biographie,
//    Roman,
//}
#endregion

#region Strategy 2: Use many to many
//public class Livre
//{
//    public int LivreId { get; set; }

//    public string Titre { get; set; } = default!;

//    public List<Categorie> Categories { get; set; } = [];

//}

//public class Categorie
//{
//    public int CategorieId { get; set; }

//    public string Code { get; set; } = default!;

//    public List<Livre> Livres { get; set; } = [];
//}
#endregion


#region Strategy 3: Use a intermediate entity
public class Livre
{
    public int LivreId { get; set; }

    public string Titre { get; set; } = default!;

    public List<Categorie> Categories { get; set; } = [];

}

public class LivreCategorie
{
    public int LivreId { get; set; }
    public int CategorieId { get; set; }
}

public class Categorie
{
    public int CategorieId { get; set; }
    public string Code { get; set; } = default!;

    public List<Livre> Livres { get; set; } = [];
}

//public enum Categorie
//{
//    Aventure = 1,
//    Biographie = 2,
//    Roman = 3,
//}
#endregion
