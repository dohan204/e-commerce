namespace infrastructure.persistence.entities
{
    public abstract class BaseEntity
    {
        public bool IsDeleted { get; set; }
    }
}