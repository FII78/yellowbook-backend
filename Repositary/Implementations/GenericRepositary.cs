using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.API.Repositary.Implementations
{
    public class GenericRepositary
    {
        //public class Repository<TEntity> : IRepository<TEntity>
        //{
        //    private readonly IMongoCollection<TEntity> _collection;

        //    public Repository(IMongoCollection<TEntity> collection)
        //    {
        //        if (null == collection)
        //            throw new ArgumentNullException("collection");
        //        _collection = collection;

        //        this.CollectionName = collection.CollectionNamespace.CollectionName;
        //    }

        //    public string CollectionName { get; private set; }

        //    public Task<long> CountAsync(FilterDefinition<TEntity> filter)
        //    {
        //        return _collection.CountAsync(filter);
        //    }

        //    public IFindFluent<TEntity, TEntity> Find(FilterDefinition<TEntity> filter)
        //    {
        //        return _collection.Find(filter);
        //    }
        //    public IFindFluent<TEntity, TEntity> Find(Expression<Func<TEntity, bool>> filter)
        //    {
        //        return _collection.Find(filter);
        //    }

        //    public Task<TEntity> FindOneAndReplaceAsync(FilterDefinition<TEntity> filter, TEntity replacement)
        //    {
        //        return _collection.FindOneAndReplaceAsync(filter, replacement);
        //    }
        //    public Task<TEntity> FindOneAndReplaceAsync(Expression<Func<TEntity, bool>> filter, TEntity replacement)
        //    {
        //        return _collection.FindOneAndReplaceAsync(filter, replacement);
        //    }
        //}
    }
}
