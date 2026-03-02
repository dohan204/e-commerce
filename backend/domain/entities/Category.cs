using domain.exceptions;

namespace domain.entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public string? Images { get; private set; }
        public string Slug { get; private set; }
        protected Category() { }
        public Category(string name, string? images, string slug)
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainException("Name is required");
            Name = name.Trim();
            Images = images;
            Slug = GenerateSlug(slug);
        }
        public void Rename(string newName)
        {
            if(string.IsNullOrEmpty(newName))
                throw new DomainException("Name isvalid");
            Name = newName;
        }
        public string GenerateSlug(string slug)
        {
            if(string.IsNullOrEmpty(slug)) 
                throw new DomainException("Slug is required");
            
            var newSlug = slug.ToLowerInvariant();
            return newSlug.Replace(" ", "-");
        }
    }
}