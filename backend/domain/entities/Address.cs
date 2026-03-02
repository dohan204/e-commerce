using System.Diagnostics.Contracts;
using domain.exceptions;

namespace domain.entities
{
    public class Address : BaseEntity
    {
        public Guid UserId { get; set; }
        // public string FullName {get; private set;} = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Province { get; private set; } = string.Empty;
        public string District { get; private set; } = string.Empty;
        public string Ward { get; private set; } = string.Empty;
        public string? Details { get; private set; } = string.Empty;

        public bool IsDefault { get; private set; } = false;
        private Address() { }
        public Address(Guid id, string province, string district, string ward, string? details, string phone)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                throw new DomainException("User invalid");

            if (string.IsNullOrEmpty(province))
                throw new DomainException("Province is not empty");

            if (string.IsNullOrEmpty(district))
            {
                throw new DomainException("district is not empty");
            }

            if (string.IsNullOrEmpty(ward))
            {
                throw new DomainException("wards is not empty");
            }
            UserId = id;
            Phone = phone;
            Province = province;
            District = district;
            Ward = ward;
            Details = details;
        }
        public void SetDefault () => IsDefault = true;
        public void UnDefault () => IsDefault = false;
    }
}