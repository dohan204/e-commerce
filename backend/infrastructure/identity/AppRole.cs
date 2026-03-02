using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set;}
    }
}
