namespace MappingApp.Entities;

// public class Livre
// {
//    public int LivreId { get; set; }
// }

// private int <LivreId>k__BackingField;
// public int LivreId
// {
//    [CompilerGenerated]
//    get
//    {
//        return < LivreId > k__BackingField;
//    }
//    [CompilerGenerated]
//    set
//    {
//            < LivreId > k__BackingField = value;
//    }
// }



public class Livre
{
    public int LivreId { get; set; }

    public string Titre { get; set; } = null!;

    public bool IsActive { get; set; } = false;

    public int Number { get; set; }
}


// Record
//public sealed record Livre
//{
//    public int LivreId { get; set; }

//    public string Titre { get; set; } = null!;

//    public bool IsActive { get; set; } = false;

//    public int Number { get; set; }
//}


// Record init
//public sealed record Livre
//{
//    //private string _url = default!;

//    //public string Url
//    //{
//    //    get { return _url; }
//    //    set { _url = value; }
//    //}

//    public int LivreId { get; init; }

//    public required string Titre { get; init; } = null!;

//    public required bool IsActive { get; init; } = false;

//    public required int Number { get; set; }
//}

//public sealed record Livre(string Titre, bool IsActive, int Number)
//{
//    public int LivreId { get; init; } // Id property that will be auto-incremented by ef core and we don’t need to assign it, that’s why it’s
//}


// EF accesses the collection through its backing field
//public class Livre
//{
//    private readonly List<Tag> _tags = new();

//    public int Id { get; set; }

//    public IEnumerable<Tag> Tags => _tags;

//    public void AddTag( Tag tag ) => _tags.Add( tag );
//}

//public class Tag
//{
//    public int TagId { get; set; }

//    public string Nom { get; init; } = default!;
//}



//public class Livre2
//{
//    public Livre2( string titre )
//    {
//        Titre = titre;
//    }

//    public int LivreId { get; set; }

//    public string Titre { get; private set; }

//    public bool IsActive { get; set; } = false;

//    public int Number { get; set; }
//}


//public class Livre
//{
//    public Livre(string titre)
//    {
//        Titre = titre + "suffix";
//    }

//    public int LivreId { get; set; }

//    public string Titre { get; set; } = null!;

//    public bool IsActive { get; set; } = false;

//    public int Number { get; set; }
//}