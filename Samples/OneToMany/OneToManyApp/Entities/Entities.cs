namespace OneToManyApp.Entities;

#region Case 1
//public class Livre
//{
//    public int LivreId { get; set; }

//    public string Titre { get; set; } = null!;

//    public ICollection<Tag> Tags { get; set; } = [];
//}

//public class Tag
//{
//    public int TagId { get; set; }

//    public string Nom { get; set; } = null!;

//    public int LivreId { get; set; }

//    public Livre Livre { get; set; } = null!;
//}
#endregion


#region Case 2
//public class Livre
//{
//    public int XLivreId { get; set; }

//    public string Titre { get; set; } = null!;

//    public ICollection<Tag> Tags { get; set; } = [];
//}


//public class Tag
//{
//    public int XTagId { get; set; }

//    public string Nom { get; set; } = null!;


//    // Since there is no CLR property which holds the foreign key for this relationship, a shadow property is created.
//    public int YLivreId { get; set; }
//    public Livre Livre { get; set; } = null!;
//}
#endregion



#region Case 3
//public class Livre
//{
//    public int LivreId { get; set; }

//    public string Titre { get; set; } = null!;

//    public ICollection<Tag> Tags { get; set; } = [];
//}

//public class Tag
//{
//    // No Primary Key in the CLR
//    // public int LivreId { get; set; }
//    public string Nom { get; set; } = null!;


//    // No Foreign Key in the CLR
//    // public int LivreId { get; set; }

//    // No Navigation Property in the CLR
//    // public Livre Livre { get; set; } = null!;
//}
#endregion




// Principal (parent)
public class Livre
{
    public int LivreId { get; set; }                        // Primary or alternate key properties on the principal entity

    public string Titre { get; set; } = null!;

    public Auteur AuteurProp { get; set; }                  // (Optional) Reference navigation to dependent
}

// Dependent (child)
public class Auteur
{
    public int AuteurId { get; set; }

    public string Nom { get; set; } = default!;

    public int LivreId { get; set; }                        // Required foreign key property

    public Livre LivreProp { get; set; } = default!;        // (Optional) Required reference navigation to principal
}