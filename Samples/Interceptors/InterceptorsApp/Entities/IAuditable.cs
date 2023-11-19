namespace InterceptorsApp.Entities;

public interface IAuditable
{
    DateTime CreatedOnUtc { get; set; }
    DateTime? ModifiedOnUtc { get; set; }
}

