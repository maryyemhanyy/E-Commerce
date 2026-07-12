using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public class IdentityUserResult
    {
        public IdentityUserResult(string id, string? email, string? username, string displayName)
        {
            Id = id;
            Email = email;
            Username = username;
            DisplayName = displayName;
        }

        public string Id { get; set; } = default!;
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string DisplayName { get; set; } = default!;


    }
}
