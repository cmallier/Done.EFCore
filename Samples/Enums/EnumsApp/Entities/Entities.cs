namespace EnumsApp.Entities;

//public class Livre
//{
//    public int LivreId { get; set; }

//    public string Titre { get; set; } = default!;

//    public List<Categorie> Categories { get; set; } = [];

//}

//public class Categorie
//{
//    private readonly string _code = default!;

//    public int CategorieId { get; set; }

//    //public string Code { get; set; } = default!;
//    public string Code
//    {
//        get { return _code; }
//    }

//    public ICollection<Livre> Livres { get; set; } = [];
//}

//public enum Category
//{
//    Aventure = 1,
//    Biographie = 2,
//    Roman = 3,
//}


//public enum Categorie
//{
//    Aventure,
//    Biographie,
//    Roman,
//}

/*
dbo.Livres
Titre : Titre1
Categories : [0,1]
*/


// Strategy 2: Use many to many
public class Livre
{
    public int LivreId { get; set; }

    public string Titre { get; set; } = default!;

    public List<Categorie> Categories { get; set; } = [];

}

public class Categorie
{
    public int CategorieId { get; set; }

    public string Code { get; set; } = default!;

    public List<Livre> Livres { get; set; } = [];
}

public enum Category
{
    Aventure = 1,
    Biographie = 2,
    Roman = 3,
}