namespace infrastructure.persistence.entities
{
    public interface IBase
    {
        // public int? Id { get; set; }
        DateTime CreatedAt {get; set;}
        DateTime? UpdatedAt {get; set;}
    }
}