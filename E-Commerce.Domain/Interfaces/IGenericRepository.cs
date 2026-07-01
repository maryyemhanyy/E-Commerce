using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Interfaces
{
    public interface IGenericRepository<TEntity , Tkey> where TEntity : BaseEntity<Tkey> 
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity,Tkey> specifications,CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(Tkey id , CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);


    }
}
