using Infrastructure.Entity;

namespace Infrastructure.Interfaces
{
    /// <summary>
    /// Use ICommand to locate http requset data from the APIS as command all command should have
    /// Execute function
    /// </summary>
    /// <createdby>
    /// Jebril mohammad
    /// </createdby>
    internal interface ICommand<T, Entity> where Entity : class, IEntity, new()
    {
        public  T Execute(ICommandParam<Entity> param );
    }

    internal interface ICommandExt<T, Entity> where Entity : class, IEntity, new()
    {
        public T Execute(ICommandParam<Entity> param, dynamic paramExt);
    }
}