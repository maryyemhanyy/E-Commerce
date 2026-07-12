using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Interfaces
{
    public interface ICacheRepository
    {
        Task<string?> GetCacheValueAsync(string key, CancellationToken ct = default);
        Task SetAsync(string key, string value, TimeSpan expiration , CancellationToken ct = default);
    }
}
