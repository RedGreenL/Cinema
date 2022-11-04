using Cinema.Domain;
using FluentResults;

namespace Cinema.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    IList<TEntity> GetAll();

    Result<TEntity> Get(Guid id);

    Result<Guid> Create(TEntity entity);

    Result<Guid> Update(TEntity entity);

    Result Delete(Guid id);

    Result SaveChanges();
}