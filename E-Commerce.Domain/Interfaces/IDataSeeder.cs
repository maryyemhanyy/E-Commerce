using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Interfaces
{
    public interface IDataSeeder
    {
        Task DataSeedAsync(CancellationToken ct = default);
    }
}
