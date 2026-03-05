namespace application.interfaces
{
    public interface ISoftDelete
    {
        bool IsDeleted {get; set;}
        DateTimeOffset? DeleteAt { get; set; }
    }
}