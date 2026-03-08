using System.Diagnostics.Contracts;
using domain.exceptions;
using domain.valuesObject;

namespace domain.entities
{
    public class Address : ValueObject
    {
        // public string FullName {get; private set;} = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Province { get; private set; } = string.Empty;
        public string District { get; private set; } = string.Empty;
        public string Ward { get; private set; } = string.Empty;
        public string? Details { get; private set; } = string.Empty;

        public bool IsDefault { get; private set; } = false;
        private Address() { }
        public Address(string province, string district, string ward, string? details, string phone)
        {
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
            Phone = phone;
            Province = province;
            District = district;
            Ward = ward;
            Details = details;
        }
        public void SetDefault () => IsDefault = true;
        public void UnDefault () => IsDefault = false;
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Phone;
            yield return Province;
            yield return District;
            yield return Ward;
            yield return Details;
        }
    }
}