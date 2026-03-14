using infrastructure.persistence.entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.identity
{
    public class AppUser : IdentityUser<Guid>, IBase
    {
        public string? FullName { get; set; }
        public string? Avatar { get; set; } 
        //public string? Role { get; set; }
        public string? Status { get; set; } = "Banned";
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual WishlistEntity Wishlist { get; set; }
        public ICollection<OrderEntity> Orders{ get; set; } = new List<OrderEntity>();
        public ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();
        public ICollection<CartEntity> Carts { get; set; } = new List<CartEntity>();
        public ICollection<ReviewEntity> Reviews {get; set; } = new List<ReviewEntity>();
    }
}
