using infrastructure.identity;

namespace infrastructure.persistence.entities
{
    public class AddressEntity : IBase
    {
        public int Id { get; set; }
        public Guid UserId {get; set;}
        public AppUser AppUser { get; set;}
        public string FullName {get; set; } = string.Empty; 
        public string Phone {get; set;} = string.Empty;
        public string Province {get; set; } = string.Empty;
        public string District {get; set; } = string.Empty;
        public string Ward {get; set; } = string.Empty;
        public string Details {get; set; } = string.Empty;
        public bool IsDefault {get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}