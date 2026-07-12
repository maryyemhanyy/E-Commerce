using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string userId, string email, string username, IEnumerable<string> roles);
    }
}
