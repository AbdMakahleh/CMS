
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Interfaces
{
    /// <summary>
    /// The IDBManger used for have one refernce to deal with it when any need functions related to
    /// database with this entity
    /// </summary>
    /// <seealso cref="IEntity" />
    /// <createdby>
    /// Jebril mohammad
    /// </createdby>

    //public interface IDBManger<Entity> where Entity : class, IEntity, new()
    public interface IDBManger<Entity> where Entity : class, new()
    {
        IDbContextTransaction BeginTransaction();
    }
}