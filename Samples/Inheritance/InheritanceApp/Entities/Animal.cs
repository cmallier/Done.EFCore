namespace InheritanceApp.Entities;

public abstract class Animal
{
    public int Id { get; set; }
    public string Species { get; set; } = null!;
    public int? DnaId { get; set; }
    public DnaSequence? Dna { get; set; }
}

public class FarmAnimal : Animal
{
    public decimal Value { get; set; }
}

public class Pet : Animal
{
    public string Name { get; set; } = null!;
}

public class Cat : Pet
{
    public string EducationLevel { get; set; } = null!;
}

public class Dog : Pet
{
    public string FavoriteToy { get; set; } = null!;
}

public class Human
{
    public int Id { get; set; }
    public int? FavoriteAnimalId { get; set; }
    public Animal? FavoriteAnimal { get; set; }
}

public class DnaSequence
{
    public int Id { get; set; }
    public string Sequence { get; set; } = null!;
}