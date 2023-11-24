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


#region Strategy 3 : Use a intermediate entity
//public class Livre
//{
//    public int LivreId { get; set; }

//    public string Titre { get; set; } = default!;

//    public List<LivreCategorie> LivreCategories { get; set; } = [];

//}

//public class LivreCategorie
//{
//    public int LivresId { get; set; }
//    public Livre Livre { get; set; } = default!;
//    public int CategoriesId { get; set; }
//    public Categorie Categorie { get; set; } = default!;
//}

//public class CategorieEnumTableForTheSakeOfNotUsigSQLDirectlySorryAboutThat
//{
//    public int CategorieId { get; set; }
//    public string Code { get; set; } = default!;

//    public List<LivreCategorie> LivreCategories { get; set; } = [];
//}

//public enum Category
//{
//    Aventure = 1,
//    Biographie = 2,
//    Roman = 3,
//}
#endregion


#region Strategy 4 : Mapping
//public class Livre
//{
//    public int LivreId { get; set; }

//    public string Titre { get; set; } = default!;

//    public List<LivreCategorie> LivreCategories { get; set; } = [];

//}

//public class LivreCategorie
//{
//    //private Livre _livre = default!;   // Backend field
//    public int LivresId { get; set; }
//    public Livre Livre { get; set; } = default!;
//    public int CategoriesId { get; set; }
//}

//public enum Categorie
//{
//    Aventure = 1,
//    Biographie = 2,
//    Roman = 3,
//}
#endregion


#region Strategy 5 : Domain Driven Design
//public class Livre
//{
//    public int LivreId { get; set; }

//    public string Titre { get; set; } = default!;

//    public List<Categorie> Categories { get; set; } = [];

//}

//public enum Categorie
//{
//    Aventure = 1,
//    Biographie = 2,
//    Roman = 3,
//}
#endregion


#region Strategy 6 : OwnsMany Json
//public class Livre
//{
//    public int LivreId { get; set; }

//    public string Titre { get; set; } = default!;

//    public List<Categorie> Categories { get; set; } = [];

//}

//public class Categorie
//{
//    public string Value { get; set; } = default!;

//    public static implicit operator string(Categorie categorie) => categorie.Value;
//    public static implicit operator Categorie(string categorie) => new() { Value = categorie };

//}
#endregion


#region Strategy 7 : Many to many with a join table
public class Livre
{
    public int LivreId { get; set; }

    public string Titre { get; set; } = default!;

    public List<Categorie> Categories { get; set; } = [];

    //public List<int> CategorieIds { get; set; } = [];
}

public record Categorie
{
    private string _code = default!;
    public int CategorieId { get; } = default!;
    public string Code
    {
        get
        {
            return _code;
        }
    }
}

public enum CategoryEnum
{
    Aventure = 1,
    Biographie = 2,
    Roman = 3,
}
#endregion


//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<EntityA>()
//        .HasMany( a => a.EntityBs )
//        .WithMany() // No collection in EntityB, so leave this blank
//        .UsingEntity( join => join.ToTable( "EntityAEntityB" ) ); // Name of the join table
//}