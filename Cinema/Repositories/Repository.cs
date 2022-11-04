using Cinema.Domain;
using Cinema.Interfaces;
using Cinema.Models;
using FluentResults;

namespace Cinema.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CinemaDbContext _dbContext;

        public Repository(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public Result<TEntity> Get(Guid id)
        {
            var response = _dbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);

            return response is null 
                ? Result.Fail($"Entity with id: {id}, not found") 
                : Result.Ok(response);
        }

        public Result<Guid> Create(TEntity entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            
            _dbContext.Set<TEntity>().Add(entity);
            var response = SaveChanges();
            
            return response.IsSuccess ? Result.Ok(entity.Id) : response;
        }

        public Result<Guid> Update(TEntity entity)
        {
            if (entity.Id == Guid.Empty) 
                return Result.Fail($"The id: {entity.Id} has an incorrect format");

            _dbContext.Set<TEntity>().Update(entity);
            var response = SaveChanges();

            return response.IsSuccess ? Result.Ok(entity.Id) : response;
        }
        
        public Result Delete(Guid id)
        {
            var entity = Get(id);

            if (entity.IsFailed)
                return Result.Fail("Entity not found");

            _dbContext.Set<TEntity>().Remove(entity.Value);

            return SaveChanges();
        }

        public Result SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges() != 0 ? Result.Ok() : Result.Fail("Error on save");
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }
    }
}
